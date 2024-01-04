using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputBehaviour : MonoBehaviour
{
    private PlayerActions _actions;

    [SerializeField]
    private ProjectileSpawnerBehaviour _gun;
    [SerializeField]
    private GameObject _chargeEffect;
    [SerializeField]
    private ProjectileBehaviour _defaultBullet;
    [SerializeField]
    private ProjectileBehaviour _chargeBullet;

    [SerializeField]
    private float _chargeSpeed;
    [SerializeField]
    private float _minChargeAmount;
    [SerializeField]
    private AudioClip _chargeSound;
    private static float _currentChargeValue;
    private bool _canCharge;

    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private float _sensitivity = 1;
    [SerializeField]
    private float _rotationSpeed = 1;
    private Vector2 _lastRotation;
    private Vector2 _currentRotation;
    private bool _canPlayChargeFX = true;

    public ProjectileSpawnerBehaviour Gun { get => _gun;}
    public float ChargeSpeed { get => _chargeSpeed; set => _chargeSpeed = value; }
    public float MinChargeAmount { get => _minChargeAmount; set => _minChargeAmount = value; }
    public bool CanCharge { get => _canCharge; set => _canCharge = value; }
    public static float CurrentChargeValue { get => _currentChargeValue; set => _currentChargeValue = value; }


    // Start is called before the first frame update
    protected virtual void Awake()
    {
        _actions = new PlayerActions();

        _actions.Combat.ShootLeft.started += context => _canCharge = true;
        _actions.Combat.ShootLeft.performed += context => FireWeapon();

        _actions.Combat.RotateCamera.started += RotateCamera;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        _actions?.Enable();
    }

    public void FireWeapon()
    {
        if (CurrentChargeValue >= _minChargeAmount)
            _gun.Projectile = _chargeBullet;
        else
            _gun.Projectile = _defaultBullet;

        _gun.Fire();

        CurrentChargeValue = 0;
        _canCharge = false;
        _canPlayChargeFX = true;
        _chargeEffect?.SetActive(false);
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

    private void Update()
    {
        if (!CanCharge)
            return;

        CurrentChargeValue += Time.deltaTime * _chargeSpeed;

        if (CurrentChargeValue >= MinChargeAmount && _canPlayChargeFX)
        {
            SoundManagerBehaviour.Instance.PlaySound(_chargeSound);
            _canPlayChargeFX = false;
            _chargeEffect?.SetActive(true);
        }

    }
}
