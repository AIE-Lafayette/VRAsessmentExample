using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private bool _canMove = true;
    [SerializeField]
    private bool _updateFacing = true;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.rigidbody.CompareTag("Player"))
            return;

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
