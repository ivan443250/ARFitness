using System;

public interface IUIStateManager<T> where T : Enum
{
    T CurrentUIState { get; }
    void ChangeUIState(T newState);
}
