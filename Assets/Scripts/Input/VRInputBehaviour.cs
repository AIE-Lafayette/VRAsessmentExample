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
        _leftHandActivate.action.performed += context => LeftGun.Fire();
        _rightHandActivate.action.performed += context => RightGun.Fire();
    }
}
