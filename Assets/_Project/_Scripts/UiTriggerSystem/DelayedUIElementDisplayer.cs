using System;
using System.Collections;
using TMPro;
using UnityEngine;


public class DelayedUIElementDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject _element;

    public void DisplayElementForSeconds(float seconds)
    {
        StartCoroutine(DisplayElementCoroutine(seconds));
    }

    private IEnumerator DisplayElementCoroutine(float seconds)
    {
        _element.SetActive(true);

        yield return new WaitForSeconds(seconds);

        _element.SetActive(false);
    }
}