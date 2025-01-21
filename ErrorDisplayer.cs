using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorDisplayer : MonoBehaviour
{
    [SerializeField]
    private GameObject _errorWindow;

    private static Image _image;
    private static TMP_Text _textBox;
    private static Color _imageColor;
    private static Color _textColor;
    private void Awake()
    {
        _image = _errorWindow.GetComponent<Image>();
        _textBox = _errorWindow.GetComponentInChildren<TMP_Text>();
        _imageColor = _image.color;
        _textColor = _textBox.color;
    }
    public void DisplayError(Exception e) 
    {
        Debug.LogException(e);
        _textBox.text = e.Message;
        _imageColor.a = 1f;
        _textColor.a = 1f;
        _image.color = _imageColor;
        _textBox.color = _textColor;
        StartCoroutine(Fade());
    }
    private IEnumerator Fade() 
    {
        yield return new WaitForSeconds(5f);
        for (int alpha = 10; alpha >= 0; alpha -= 1)
        {
            _imageColor.a = (alpha)/10f;
            _textColor.a = alpha/10f;
            _image.color = _imageColor;
            _textBox.color = _textColor;
            
            yield return new WaitForSeconds(0.1f);
        }
    }
}
