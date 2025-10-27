using System;
using UnityEngine;

public class UIStateManager<T> : IUIStateManager<T> where T : Enum
{
    public T CurrentUIState { get; set; }

    public void ChangeUIState(T newState)
    {
        if (CurrentUIState.Equals(newState)) return;

        Debug.Log($"Changing UI state from {CurrentUIState} to {newState}");
        CurrentUIState = newState;

        GameEvents.TriggerUIStateChanged(newState);
    }
}