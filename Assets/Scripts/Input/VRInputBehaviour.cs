using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRInputBehaviour : InputBehaviour
{
    [SerializeField]  
    private InputActionReference _leftHandActivate;
    [SerializeField]
    private InputActionReference _rightHandActivate;


    // Start is called before the first frame update
    protected override void Awake()
    {
        _rightHandActivate.action.started += context => CanCharge = true;
        _rightHandActivate.action.performed += context => FireWeapon();
    }
}
