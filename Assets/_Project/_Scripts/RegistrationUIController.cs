using UnityEngine;
using Zenject;

public class RegistrationUIController : MonoBehaviour
{
    [Inject] private UIStateManager<RegistrationUIState> _UIStateManager;

    public void ShowEmailRegistration()
    {
        _UIStateManager.ChangeUIState(RegistrationUIState.EmailRegistration);
    }

    public void ShowEmailLogIn()
    {
        _UIStateManager.ChangeUIState(RegistrationUIState.EmailLogIn);
    }

    public void ShowProfile()
    {
        _UIStateManager.ChangeUIState(RegistrationUIState.Profile);
    }

    public void ShowParentAcception()
    {
        _UIStateManager.ChangeUIState(RegistrationUIState.ParentsAcception);
    }
}
