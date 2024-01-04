using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TextTypeBehaviour : MonoBehaviour
{
    [SerializeField]
    private string _finishedText;
    [SerializeField]
    private float _typeDelay;
    [SerializeField]
    private bool _typeOnEnable;
    [SerializeField]
    private UnityEvent _onTypeComplete;
    private TextMeshProUGUI _text;

    // Start is called before the first frame update
    void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        BeginTyping(0);
    }

    private IEnumerator TypeText(float initalDelay)
    {
        string currentText = "";

        for (int i = 0; i < _finishedText.Length; i++)
        {
            currentText.Append(_finishedText[i]);
            _text.text = currentText;
            yield return new WaitForSeconds(_typeDelay);
        }

        _onTypeComplete?.Invoke();
    }

    public void BeginTyping(float delay)
    {
        StartCoroutine(TypeText(delay));
    }
}
