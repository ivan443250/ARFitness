using System;
using UnityEngine;
using Zenject;

public class UIPanel : MonoBehaviour
{
    [Serializable]
    private class StateCondition
    {
        public string stateTypeName;
        public string stateValueName;
    }

    [Header("Panel Configuration")]
    [SerializeField] private StateCondition[] _visibleInStates; // В каких состояниях видна эта панел
    [SerializeField] private string _panelId; // Уникальный идентификатор панели
    [SerializeField] private bool _hideOnStart;

    private void Start()
    {
        GameEvents.UIStateChanged += OnUIStateChanged;
        GameEvents.UIVisibilityChanged += OnUIVisibilityChanged;

        if (isActiveAndEnabled && _hideOnStart)
            gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameEvents.UIStateChanged -= OnUIStateChanged;
        GameEvents.UIVisibilityChanged -= OnUIVisibilityChanged;
    }

    private void OnUIStateChanged(Type stateType, string newState)
    {
        if (_visibleInStates == null) return;

        foreach (StateCondition condition in _visibleInStates)
        {
            if (condition.stateTypeName == stateType.Name &&
                condition.stateValueName == newState)
            {
                gameObject.SetActive(true);
                Debug.Log(isActiveAndEnabled);
                return;
            }
        }

        gameObject.SetActive(false);
    }

    private void OnUIVisibilityChanged(string elementId, bool isVisible)
    {
        if (elementId == _panelId)
        {
            gameObject.SetActive(isVisible);
        }
    }
}
