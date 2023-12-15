using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthBehaviour : MonoBehaviour
{
    private enum DeathAction
    {
        NONE,
        DISABLE,
        DESTROY
    }

    [SerializeField]
    private float _startHealth;
    [SerializeField]
    private float _currentHealth;
    [SerializeField]
    private DeathAction _deathAction;
    [SerializeField]
    private UnityEvent _onTakeDamage;
    [SerializeField]
    private UnityEvent _onDeath;
    [SerializeField]
    private GameObject _damageEffect;
    [SerializeField]
    private GameObject _deathEffect;
    [SerializeField]
    private AudioClip _hitSound;
    [SerializeField]
    private AudioClip _deathSound;
    private Rigidbody _rb;


    public float CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        private set
        {
            _currentHealth = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _startHealth;
        _rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(float damage, Vector3 knockbackVelocity = default(Vector3))
    {
        CurrentHealth -= damage;

        if (_damageEffect)
            Instantiate(_damageEffect, transform.position, transform.rotation);

        _onTakeDamage?.Invoke();
        SoundManagerBehaviour.Instance.PlaySound(_hitSound);

        if (knockbackVelocity.magnitude > 0)
            _rb.AddForce(knockbackVelocity, ForceMode.Impulse);

        if (CurrentHealth <= 0)
            PerformDeathAction();
    }

    private void PerformDeathAction()
    {
        _onDeath?.Invoke();

        if (_deathEffect)
            Instantiate(_deathEffect, transform.position, transform.rotation);


        SoundManagerBehaviour.Instance.PlaySound(_deathSound);


        switch (_deathAction)
        {
            case DeathAction.NONE:
                break;
            case DeathAction.DISABLE:
                gameObject.SetActive(false);
                break;
            case DeathAction.DESTROY:
                Destroy(gameObject);
                break;
        }
    }
}