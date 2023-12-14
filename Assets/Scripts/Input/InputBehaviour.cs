using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputBehaviour : MonoBehaviour
{
    private PlayerActions _actions;


    [SerializeField]
    private ProjectileSpawnerBehaviour _leftGun;
    [SerializeField]
    private ProjectileSpawnerBehaviour _rightGun;
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private float _sensitivity = 1;
    [SerializeField]
    private float _rotationSpeed = 1;
    private Vector2 _lastRotation;
    private Vector2 _currentRotation;

    public ProjectileSpawnerBehaviour LeftGun { get => _leftGun; }
    public ProjectileSpawnerBehaviour RightGun { get => _rightGun;}


    // Start is called before the first frame update
    protected virtual void Awake()
    {
        _actions = new PlayerActions();

        _actions.Combat.ShootLeft.performed += context => LeftGun.Fire();
        _actions.Combat.ShootRight.performed += context => RightGun.Fire();

        _actions.Combat.RotateCamera.started += RotateCamera;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        _actions?.Enable();
    }

    private void RotateCamera(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        _currentRotation = _camera.transform.rotation.eulerAngles;

        Vector2 rotation = _currentRotation + direction * Time.deltaTime * (_rotationSpeed * 10);

        if (Mathf.Abs(rotation.x - _lastRotation.x) >= _sensitivity / 10)
            _currentRotation.x = rotation.x;
        if (Mathf.Abs(rotation.y - _lastRotation.y) >= _sensitivity / 10)
            _currentRotation.y = rotation.y;


        //rotation.x = Mathf.Clamp(rotation.x, -180, 180);
        //rotation.y = Mathf.Clamp(rotation.y, -180, 180);

        _camera.transform.rotation = Quaternion.Euler(_currentRotation);
        _lastRotation = _currentRotation;
    }

}
