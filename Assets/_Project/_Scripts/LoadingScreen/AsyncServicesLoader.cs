using UnityEngine;
using Zenject;

public class AsyncServicesLoader : MonoBehaviour
{
    [Inject] private UIStateManager<RegistrationUIState> _uiStateManger;
    [Inject] private IFirebaseService _firebaseService;
    [Inject] private Bootstrap _bootstrap;

    private async void Start()
    {
        _uiStateManger.ChangeUIState(RegistrationUIState.LoadServisesAsync);
        await _firebaseService.Initialize();
        FirestoreORM.SetFirebaseService(_firebaseService);
        _uiStateManger.ChangeUIState(RegistrationUIState.None);
        _bootstrap.Initialize();
    }
}
