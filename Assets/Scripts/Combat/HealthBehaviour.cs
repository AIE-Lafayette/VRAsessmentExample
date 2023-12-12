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

    public float CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        private set
        {
            _currentHealth -= value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _startHealth;    
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        _onTakeDamage?.Invoke();

        if (CurrentHealth <= 0)
            PerformDeathAction();
    }

    private void PerformDeathAction()
    {
        _onDeath?.Invoke();

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