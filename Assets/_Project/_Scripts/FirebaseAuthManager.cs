using UnityEngine;
using Firebase;
using Firebase.Auth;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using Zenject;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class FirebaseAuthManager : MonoBehaviour
{
    [Inject]
    private IFirebaseService _firebaseService;

    [Inject]
    private UIStateManager<RegistrationUIState> _UIStateManager;

    private FirebaseUser _user;
    private FirebaseAuth _auth;

    [Space]
    [Header("Login")]
    [SerializeField] private TMP_InputField emailLoginField;
    [SerializeField] private TMP_InputField passwordLoginField;

    [Space]
    [Header("Registration")]
    [SerializeField] private TMP_InputField emailRegisterField;
    [SerializeField] private TMP_InputField passwordRegisterField;
    [SerializeField] private TMP_InputField confirmPasswordRegisterField;

    [SerializeField] private DelayedUIElementDisplayer _delayedDisplayer;

    public void SetupFirebaseServiceListeners()
    {
        _auth = _firebaseService.Auth;
        _auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    public void ChangeSceneOnTraining()
    {
        SceneManager.LoadScene("Training");
    }

    // Track state changes of the auth object.
    private void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (_auth.CurrentUser != _user)
        {
            bool signedIn = _user != _auth.CurrentUser && _auth.CurrentUser != null;

            if (!signedIn && _user != null)
            {
                Debug.Log("Signed out " + _user.UserId);
            }

            _user = _auth.CurrentUser;

            if (signedIn)
            {
                Debug.Log("Signed in " + _user.UserId);
            }
        }
    }

    public void Login()
    {
        StartCoroutine(LoginWithEmailAsync(emailLoginField.text, passwordLoginField.text));
    }

    private IEnumerator LoginWithEmailAsync(string email, string password)
    {
        var loginTask = _auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            Debug.LogError(loginTask.Exception);

            FirebaseException firebaseException = loginTask.Exception.GetBaseException() as FirebaseException;
            AuthError authError = (AuthError)firebaseException.ErrorCode;

            string failedMessage = "Login Failed! Because ";

            switch (authError)
            {
                case AuthError.InvalidEmail:
                    failedMessage += "Email is invalid";
                    break;
                case AuthError.WrongPassword:
                    failedMessage += "Wrong Password";
                    break;
                case AuthError.MissingEmail:
                    failedMessage += "Email is missing";
                    break;
                case AuthError.MissingPassword:
                    failedMessage += "Password is missing";
                    break;
                default:
                    failedMessage = "Login Failed";
                    break;
            }

            Debug.Log(failedMessage);
        }
        else
        {
            _user = loginTask.Result.User;

            Debug.LogFormat("{0} You Are Successfully Logged In", _user.DisplayName);

            SceneManager.LoadScene("Training");
        }
    }

    public async void Register()
    {
        await RegisterAsync(emailRegisterField.text, passwordRegisterField.text, confirmPasswordRegisterField.text);
    }

    private async UniTask RegisterAsync(string email, string password, string confirmPassword)
    {
        if (string.IsNullOrEmpty(email))
        {
            Debug.LogError("email field is empty");
        }
        else if (passwordRegisterField.text != confirmPasswordRegisterField.text)
        {
            Debug.LogError("Password does not match");
        }
        else
        {
            Task<AuthResult> registrationTask = _auth.CreateUserWithEmailAndPasswordAsync(email, password);
            AuthResult authResult = await registrationTask.AsUniTask();

            if (registrationTask.Exception != null)
            {
                Debug.LogError(registrationTask.Exception);

                FirebaseException firebaseException = registrationTask.Exception.GetBaseException() as FirebaseException;
                AuthError authError = (AuthError)firebaseException.ErrorCode;

                string failedMessage = "Registration Failed! Becuase ";
                switch (authError)
                {
                    case AuthError.InvalidEmail:
                        failedMessage += "Email is invalid";
                        break;
                    case AuthError.WrongPassword:
                        failedMessage += "Wrong Password";
                        break;
                    case AuthError.MissingEmail:
                        failedMessage += "Email is missing";
                        break;
                    case AuthError.MissingPassword:
                        failedMessage += "Password is missing";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        _delayedDisplayer.DisplayElementForSeconds(1);
                        break;
                    default:
                        failedMessage = "Registration Failed";
                        break;
                }

                Debug.Log(failedMessage);
            }
            else
            {
                _user = authResult.User;

                var newUser = new CloudUserModel(
                uid: _user.UserId,
                email: _user.Email
                );

                newUser.Profile.Auth.AuthMethod = "email";
                newUser.Profile.Auth.IsGuest = false;

                Debug.Log(newUser.BasicInfo.Uid);
                Debug.Log(newUser.BasicInfo.CreatedAt);
                Debug.Log(newUser.Profile.Auth.AuthMethod);
                Debug.Log(newUser.BasicInfo.Email);

                _firebaseService.SetUserOnAuth(newUser);

                _UIStateManager.ChangeUIState(RegistrationUIState.Profile);
            }
        }
    }
}
