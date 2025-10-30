using UnityEngine;
using Firebase;
using Firebase.Auth;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using Zenject;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

public class FirebaseAuthManager : MonoBehaviour
{
    [Inject]
    private IFirebaseService _firebaseService;

    [Inject]
    private UIStateManager<RegistrationUIState> _UIStateManager;

    private FirebaseUser _user;
    private FirebaseAuth _auth;

    [Space]
    [Header("LoginWithEmail")]
    [SerializeField] private TMP_InputField _emailLoginField;
    [SerializeField] private TMP_InputField _passwordLoginField;

    [Space]
    [Header("Registration")]
    [SerializeField] private TMP_InputField _emailRegisterField;
    [SerializeField] private TMP_InputField _passwordRegisterField;
    [SerializeField] private TMP_InputField _confirmPasswordRegisterField;

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

    public void LoginWithEmail()
    {
        StartCoroutine(LoginWithEmailAsync(_emailLoginField.text, _passwordLoginField.text));
    }

    private IEnumerator LoginWithEmailAsync(string email, string password)
    {
        var loginTask = _auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            _delayedDisplayer.DisplayElementForSeconds("Данные введены неверно или не введены!", 1);

            Debug.LogError(loginTask.Exception);

            FirebaseException firebaseException = loginTask.Exception.GetBaseException() as FirebaseException;
            AuthError authError = (AuthError)firebaseException.ErrorCode;

            string failedMessage = "LoginWithEmail Failed! Because ";

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
                    failedMessage = "LoginWithEmail Failed";
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

    public async void RegisterWithEmail()
    {
        await RegisterAsync(_emailRegisterField.text, _passwordRegisterField.text, _confirmPasswordRegisterField.text);
    }

    private async UniTask RegisterAsync(string email, string password, string confirmPassword)
    {
        if (string.IsNullOrEmpty(email))
        {
            _delayedDisplayer.DisplayElementForSeconds("Почта не введена!", 1);
            Debug.LogError("email field is empty");
        }
        else if (_passwordRegisterField.text != _confirmPasswordRegisterField.text)
        {
            _delayedDisplayer.DisplayElementForSeconds("Пароли не совпадают!", 1);
            Debug.LogError("Password does not match");
        }
        else
        {
            _delayedDisplayer.DisplayElementForSeconds("Данные введены неверно или не введены!", 1);
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
                        _delayedDisplayer.DisplayElementForSeconds("Почта не валидна!", 1);
                        break;
                    case AuthError.WrongPassword:
                        failedMessage += "Wrong Password";
                        _delayedDisplayer.DisplayElementForSeconds("Неправильный пароль!", 1);
                        break;
                    case AuthError.MissingEmail:
                        failedMessage += "Email is missing";
                        _delayedDisplayer.DisplayElementForSeconds("Почта не указана!", 1);
                        break;
                    case AuthError.MissingPassword:
                        failedMessage += "Password is missing";
                        _delayedDisplayer.DisplayElementForSeconds("Пароль не указан!", 1);
                        break;
                    case AuthError.EmailAlreadyInUse:
                        _delayedDisplayer.DisplayElementForSeconds("Такая почта уже используется!", 1);
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

    public async void LoginWithSocialNetworks()
    {
        var googleProvider = new FederatedOAuthProvider();

        googleProvider.SetProviderData(new FederatedOAuthProviderData()
        {
            ProviderId = "google.com",
        });

        try
        {
            AuthResult authResult = await _auth.SignInWithProviderAsync(googleProvider);
            _user = authResult.User;

            var newUser = new CloudUserModel(
                uid: _user.UserId,
                email: _user.Email
            );

            newUser.Profile.Auth.AuthMethod = "google";
            newUser.Profile.Auth.IsGuest = false;

            _firebaseService.SetUserOnAuth(newUser);

            _UIStateManager.ChangeUIState(RegistrationUIState.Profile);
        }
        catch (Exception ex)
        {
            Debug.Log($"{ex.Message}");
        }
    }
}
