using System;
using System.Collections;
using TMPro;
using UnityEngine;


public class DelayedUIElementDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject _element;
    [SerializeField] private TMP_Text _text;

    public void DisplayElementForSeconds(string text, float seconds)
    {
        StartCoroutine(DisplayElementCoroutine(text, seconds));
    }

    private IEnumerator DisplayElementCoroutine(string text, float seconds)
    {
        _element.SetActive(true);
        _text.text = text;

        yield return new WaitForSeconds(seconds);

        _element.SetActive(false);
    }
}