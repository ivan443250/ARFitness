using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class GuestProfileMaker : MonoBehaviour
{
    [SerializeField] private Button _enterButton;

    [SerializeField] private TMP_InputField _age;
    [SerializeField] private TMP_Dropdown _genderDropdown;
    [SerializeField] private TMP_Dropdown _startLevelDropdown;

    [SerializeField] private DelayedUIElementDisplayer _delayedDisplayer;

    private UserProfileConstants.Sex _gender;
    private UserProfileConstants.StartLevel _startLevel;

    private void Start()
    {
        _enterButton.onClick.AddListener(SaveGuestLocal);

        _genderDropdown.onValueChanged.AddListener((int index) => 
        { 
            _gender = UserProfileConstants.ProfileSexOptions[_genderDropdown.options[index].text]; 
        });
        _genderDropdown.onValueChanged.AddListener((int index) =>
        {
            _startLevel = UserProfileConstants.ProfileStartLevelOptions[_startLevelDropdown.options[index].text];
        });
    }

    private void SaveGuestLocal()
    {
        bool ageIsNumber = int.TryParse(_age.text, out _);

        bool canContinue =  ageIsNumber == true &&
                            int.Parse(_age.text) > 0 && int.Parse(_age.text) <= 150;

        if (canContinue)
        {
            PlayerPrefs.SetString(PlayerPrefsKeys.Gender, _gender.ToString());
            PlayerPrefs.SetInt(PlayerPrefsKeys.UserAge, int.Parse(_age.text));
            PlayerPrefs.SetInt(PlayerPrefsKeys.UserAge, int.Parse(_age.text));

            SceneManager.LoadScene("Training");
        }
        else
        {
            _delayedDisplayer.DisplayElementForSeconds("ƒанные указаны не верно или не указаны!", 1);
        }
    }
}
