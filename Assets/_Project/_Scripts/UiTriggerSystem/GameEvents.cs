using System;

public static class GameEvents
{
    public static event Action<Type, string> UIStateChanged;
    public static event Action<string, bool> UIVisibilityChanged;
    public static event Action<string, object> UIDataUpdated;

    public static void TriggerUIStateChanged<T>(T newState) where T : Enum
    {
        var stateType = typeof(T);
        UIStateChanged?.Invoke(stateType, newState.ToString());
    }

    public static void TriggerUIVisibilityChanged(string elementId, bool isVisible)
    {
        UIVisibilityChanged?.Invoke(elementId, isVisible);
    }

    public static void TriggerUIDataUpdated(string dataKey, object dataValue)
    {
        UIDataUpdated?.Invoke(dataKey, dataValue);
    }
}
