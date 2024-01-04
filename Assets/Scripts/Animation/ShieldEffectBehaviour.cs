using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ShieldEffectBehaviour : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _shieldRenderer;
    private Material _shieldMat;

    private Color _defaultEmissionColor;
    private Color _defaultEdgeColor;
    private float _defaultEdgeIntensity;
    private float _defaultCenterFade;
    private Vector2 _defaultPulseScale;
    private float _defaultCenterPulseSpeed;

    [SerializeField]
    private Color _targetEmissionColor;
    [SerializeField]
    private Color _targetEdgeColor;
    [SerializeField]
    private float _targetEdgeIntensity;
    [SerializeField]
    private float _targetCenterFade;
    [SerializeField]
    private Vector2 _targetPulseScale;
    [SerializeField]
    private float _targetCenterPulseSpeed;
    private float _time;
    [SerializeField]
    private float _lerpSpeed = 1;
    private bool _canLerp;

    // Start is called before the first frame update
    void Start()
    {
        _shieldMat = _shieldRenderer.material;

        _defaultEmissionColor = _shieldMat.GetColor("_EmissionColor");
        _defaultEdgeColor = _shieldMat.GetColor("_EdgeColor");
        _defaultEdgeIntensity = _shieldMat.GetFloat("_EdgeIntensity");
        _defaultCenterFade = _shieldMat.GetFloat("_CenterFadeAmount");
        _defaultPulseScale = _shieldMat.GetVector("_CenterPulseScale");
        _defaultCenterPulseSpeed = _shieldMat.GetFloat("_CenterPulseSpeed");
    }

    private void OnCollisionEnter(Collision collision)
    {
        _canLerp = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canLerp)
            return;

        float lerpVal = Mathf.Sin(_time += Time.deltaTime * _lerpSpeed);

        _shieldMat.SetColor("_EmissionColor", Color.Lerp(_defaultEmissionColor, _targetEmissionColor, lerpVal));
        _shieldMat.SetColor("_EdgeColor", Color.Lerp(_defaultEdgeColor, _targetEdgeColor, lerpVal));

        _shieldMat.SetFloat("_EdgeIntensity", Mathf.Lerp(_defaultEdgeIntensity, _targetEdgeIntensity, lerpVal));
        _shieldMat.SetFloat("_CenterFadeAmount", Mathf.Lerp(_defaultCenterFade, _targetCenterFade, lerpVal));
        _shieldMat.SetFloat("_CenterPulseSpeed", Mathf.Lerp(_defaultCenterPulseSpeed, _targetCenterPulseSpeed, lerpVal));

        _shieldMat.SetVector("_CenterPulseScale", Vector2.Lerp(_defaultPulseScale, _targetPulseScale, lerpVal));

        if (lerpVal > 0)
            return;
            
        _canLerp = false;
        _time = 0;
    }
}
