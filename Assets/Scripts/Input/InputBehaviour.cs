using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputBehaviour : MonoBehaviour
{
    [SerializeField]
    private InputActionReference _leftHandActivate;
    [SerializeField]
    private InputActionReference _rightHandActivate;

    [SerializeField]
    private ProjectileSpawnerBehaviour _leftGun;
    [SerializeField]
    private ProjectileSpawnerBehaviour _rightGun;

    // Start is called before the first frame update
    void Start()
    {
        _leftHandActivate.action.performed += context => _leftGun.Fire();
        _rightHandActivate.action.performed += context => _rightGun.Fire();
    }
}
