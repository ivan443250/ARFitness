using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class KidProfileMaker : MonoBehaviour
{
    [Inject] private FirebaseService _firebaseService;

    [SerializeField] private TMP_Text _parentEmail;
    [SerializeField] private Toggle _parentAcception;

    [SerializeField] private DelayedUIElementDisplayer _displayer;

    private const string _matchEmailPattern =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
            + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
              + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
            + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

    public async void MakeKidProfile()
    {
        CloudUserModel userOnAuth = _firebaseService.GetUserOnAuth();    
        Auth auth = userOnAuth.Profile.Auth;

        bool canContinue = _parentEmail.text != null &&
                            Regex.IsMatch(_parentEmail.text, _matchEmailPattern) &&
                            _parentAcception.isOn;

        if (canContinue)
        {
            auth.ParentConsent = true;
            auth.ParentEmail = _parentEmail.text;

            await FirestoreORM.AddToFirestoreAsync(userOnAuth, "Users");
            SceneManager.LoadScene("Training");
        }
        else
        {
            _displayer.DisplayElementForSeconds("Данные не введены или введены неверно!", 1);
        }
    }
}
