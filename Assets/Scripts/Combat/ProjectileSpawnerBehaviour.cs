using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawnerBehaviour : MonoBehaviour
{
    [SerializeField]
    private string _ownerTag;
    [SerializeField]
    private ProjectileBehaviour _projectile;
    [SerializeField]
    private float _projectileForce;

    public ProjectileBehaviour Projectile { get => _projectile; set => _projectile = value; }

    public void Fire()
    {
        ProjectileBehaviour projectileInstance = Instantiate(Projectile, transform.position, transform.rotation);

        projectileInstance.RB.AddForce(transform.forward * _projectileForce, ForceMode.Impulse);
        projectileInstance.OwnerTag = _ownerTag;
    }
}
