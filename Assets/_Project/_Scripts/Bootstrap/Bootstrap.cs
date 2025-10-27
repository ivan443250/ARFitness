using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    [Inject] private IUIStateManager<RegistrationUIState> _UIStateManager;

    [SerializeField] private FirebaseAuthManager _firebaseAuthManager;

    public void Initialize()
    {
        _UIStateManager.ChangeUIState(RegistrationUIState.ChoseWayToAuth);
        _firebaseAuthManager.SetupFirebaseServiceListeners();
    }
}
