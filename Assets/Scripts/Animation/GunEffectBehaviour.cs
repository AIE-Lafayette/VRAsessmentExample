using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEffectBehaviour : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _screenRenderer;

    [SerializeField]
    private MeshRenderer[] _emissiveParts;
    [SerializeField]
    private Color _defaultEmissionColor;
    [SerializeField]
    private float _emissionStrength = 1;
    private float _lastChargeVal;

    private void SetEmission(bool enabled)
    {
        Color emissionColor = _defaultEmissionColor;

        if (enabled)
            emissionColor = _defaultEmissionColor * _emissionStrength;

        foreach (MeshRenderer renderer in _emissiveParts)
        {
            renderer.material.SetColor("_EmissionColor", emissionColor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_lastChargeVal != InputBehaviour.CurrentChargeValue)
            SetEmission(InputBehaviour.CurrentChargeValue > 0);

        _screenRenderer.material.SetFloat("_BarFillAmount", InputBehaviour.CurrentChargeValue);
        _lastChargeVal = InputBehaviour.CurrentChargeValue;
    }
}
