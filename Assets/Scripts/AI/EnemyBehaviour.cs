using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Timeline;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private bool _canMove = true;
    [SerializeField]
    private bool _updateFacing = true;
    private Rigidbody _rb;
    private HealthBehaviour _healthBehaviour;
    [SerializeField]
    private float _explosionRadius;
    [SerializeField]
    private float _explosionForce;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _healthBehaviour = GetComponent<HealthBehaviour>();
        _healthBehaviour.AddOnDeathAction(Explode);
    }

    /// <summary>
    /// Temporarily prevents the object from moving towards its target.
    /// </summary>
    /// <param name="time">The amount of time to wait before moving again.
    /// Will disable permanently if -1.</param>
    public void DisableMovement(float time = -1)
    {
        if (time == -1)
        {
            _canMove = false;
            return;
        }
        StartCoroutine(PauseMovement(time));
    }

    /// <summary>
    /// Allows the object to begin moving towards its target again.
    /// Will stop any movement timer that is currently running.
    /// </summary>
    public void EnableMovement()
    {
        StopAllCoroutines();
        _canMove = true;
    }

    private IEnumerator PauseMovement(float time)
    {
        _canMove = false;
        yield return new WaitForSeconds(time);
        _canMove = true;
    }

    private void Explode()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit[] raycastHits = Physics.SphereCastAll(ray, _explosionRadius);

        for (int i = 0; i < raycastHits.Length; i++)
        {
            Rigidbody rigidbody = raycastHits[i].collider.attachedRigidbody;
            if (rigidbody && rigidbody.CompareTag("Enemy"))
            {
                Vector2 position = new Vector2(transform.position.x, transform.position.z);
                Vector2 targetPosition = new Vector2(rigidbody.transform.position.x, rigidbody.transform.position.z);

                Vector2 positionToTarget = (targetPosition - position).normalized;

                float dot = Vector2.Dot(positionToTarget, Vector2.down);

                if (dot < 0)
                    rigidbody.AddForce(positionToTarget * _explosionForce, ForceMode.Impulse);
            }
        }

        _healthBehaviour.SpawnDeathParticles();

        gameObject.SetActive(false);
        Destroy(gameObject, 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody == null)
            return;

        if (collision.gameObject.CompareTag("Shield"))
        {
            _healthBehaviour.TakeDamage(2, -transform.forward * _explosionForce);
            return;
        }

        if (!collision.rigidbody.CompareTag("Player") && !collision.rigidbody.CompareTag("Enemy"))
            return;

        _healthBehaviour.TakeDamage((int)collision.rigidbody.velocity.magnitude / 2);

        HealthBehaviour health = collision.rigidbody.GetComponent<HealthBehaviour>();

        health.TakeDamage(1);

        Destroy(gameObject);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_canMove)
            return;

        Vector3 targetPosition = GameManagerBehaviour.Instance.Player.transform.position;
        Vector3 velocity = (targetPosition - transform.position).normalized * _moveSpeed;

        transform.position += velocity * Time.deltaTime;

        if (_updateFacing && velocity.magnitude > 0)
            transform.forward = velocity.normalized;

    }
}
