using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _damage;
    [SerializeField]
    private float _despawnDelay = 1;
    [SerializeField]
    private bool _despawnOnCollision = true;
    private string _ownerTag;
    private Rigidbody _rb;

    public Rigidbody RB { get => _rb; private set => _rb = value; }
    public string OwnerTag { get => _ownerTag; set => _ownerTag = value; }

    // Start is called before the first frame update
    void Awake()
    {
        RB = GetComponent<Rigidbody>();    
    }

    private void Start()
    {
        Destroy(gameObject, _despawnDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(OwnerTag))
            return;

        HealthBehaviour health = other.attachedRigidbody?.GetComponent<HealthBehaviour>();

        if (!health)
            return;

        health.TakeDamage(_damage);

        if (_despawnOnCollision)
            Destroy(gameObject);
    }
}
