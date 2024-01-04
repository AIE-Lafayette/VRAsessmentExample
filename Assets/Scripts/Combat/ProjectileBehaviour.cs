using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _damage;
    [SerializeField]
    private float _knockbackScale = 1;
    [SerializeField]
    private float _despawnDelay = 1;
    [SerializeField]
    private bool _despawnOnCollision = true;
    [SerializeField]
    private AudioClip _spawnSound;
    private string _ownerTag;
    private Rigidbody _rb;

    public Rigidbody RB { get => _rb; private set => _rb = value; }
    public string OwnerTag { get => _ownerTag; set => _ownerTag = value; }
    public float Damage { get => _damage; set => _damage = value; }

    // Start is called before the first frame update
    void Awake()
    {
        RB = GetComponent<Rigidbody>();    
    }

    private void Start()
    {
        Destroy(gameObject, _despawnDelay);
        SoundManagerBehaviour.Instance.PlaySound(_spawnSound);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(OwnerTag))
            return;

        HealthBehaviour health = other.attachedRigidbody?.GetComponent<HealthBehaviour>();

        if (!health)
            return;

        health.TakeDamage(Damage, RB.velocity.normalized * _knockbackScale);

        if (_despawnOnCollision)
            Destroy(gameObject);
    }
}
