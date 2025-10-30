using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class ProfileMaker : MonoBehaviour
{
    [Inject] private UIStateManager<RegistrationUIState> _UIStateManager;
    [Inject] private IFirebaseService _firebaseService;

    [SerializeField] private Button[] _avatarsButtons;

    [SerializeField] private TMP_InputField _displayingName;
    [SerializeField] private TMP_InputField _realName;
    [SerializeField] private TMP_InputField _age;

    [SerializeField] private TMP_Dropdown _sexDropdown;
    [SerializeField] private TMP_Dropdown _startLevelDropdown;
    [SerializeField] private TMP_Dropdown _sportExperienceDropdown;
    [SerializeField] private TMP_Dropdown _goalDropdown;

    [SerializeField] private Toggle _GDPR;
    [SerializeField] private Toggle _COPPA;

    [SerializeField] private DelayedUIElementDisplayer _displayer;

    private UserProfileConstants.Sex _sex;
    private UserProfileConstants.StartLevel _startLevel;
    private UserProfileConstants.SportExperience _sportExperience;
    private UserProfileConstants.Goal _goal;
    private string _avatarPath;

    private void Start()
    {
        foreach (Button button in _avatarsButtons)
            button.onClick.AddListener(() => SetAvatarPath(button));
        
        _sexDropdown.onValueChanged.AddListener(SetSex);
        _startLevelDropdown.onValueChanged.AddListener(SetStartLevel);
        _sportExperienceDropdown.onValueChanged.AddListener(SetSportExperience);
        _goalDropdown.onValueChanged.AddListener(SetGoal);
    }

    private void SetAvatarPath(Button button)
    {
        CloudUserModel currentUserOnAuth = _firebaseService.GetUserOnAuth();
        Image buttonImage = button.GetComponent<Image>();

        if (buttonImage.sprite != null)
        {
            _avatarPath = "Images/Avatars" + $"/{buttonImage.sprite.name}";

            currentUserOnAuth.BasicInfo.AvatarPath = _avatarPath;
            _firebaseService.SetUserOnAuth(currentUserOnAuth);

            Debug.Log(currentUserOnAuth.BasicInfo.AvatarPath);
        }
    }

    private void SetSex(int sexIndex)
    {
        _sex = UserProfileConstants.ProfileSexOptions[_sexDropdown.options[sexIndex].text];
        Debug.Log(_sex);
    }

    private void SetStartLevel(int startLevelIndex)
    {
        _startLevel = UserProfileConstants.ProfileStartLevelOptions[_startLevelDropdown.options[startLevelIndex].text];
        Debug.Log(_startLevel);
    }

    private void SetSportExperience(int sportExperienceIndex)
    {
        _sportExperience = UserProfileConstants.ProfileSportExperienceOptions[_sportExperienceDropdown.options[sportExperienceIndex].text];
        Debug.Log(_sportExperience);
    }

    private void SetGoal(int goalIndex)
    {
        _goal = UserProfileConstants.ProfileGoalOptions[_goalDropdown.options[goalIndex].text];
        Debug.Log(_goal);
    }

    public async void MakeProfile()
    {
        bool ageIsNumber = int.TryParse(_age.text, out _);
        
        bool canContinue = string.IsNullOrEmpty(_avatarPath) == false &&
                            string.IsNullOrEmpty(_displayingName.text) == false &&
                            ageIsNumber == true &&
                            int.Parse(_age.text) > 0 && int.Parse(_age.text) <= 150 &&
                            _GDPR.isOn && _COPPA.isOn;

        if (canContinue)
        {
            int age = int.Parse(_age.text);

            CloudUserModel currentUserOnAuth = _firebaseService.GetUserOnAuth();
            BasicInfo basicInfo = currentUserOnAuth.BasicInfo;
            Profile profile = currentUserOnAuth.Profile;

            Debug.Log(currentUserOnAuth.ToString());
            Debug.Log(basicInfo.ToString());
            Debug.Log(profile.ToString());

            basicInfo.DisplayName = _displayingName.text;
            basicInfo.RealName = _realName.text;
            profile.Age = age;
            profile.Gender = _sex.ToString();
            profile.FitnessLevel = _startLevel.ToString();

            _firebaseService.SetUserOnAuth(currentUserOnAuth);

            if (age < 14)
            {
                _UIStateManager.ChangeUIState(RegistrationUIState.ParentsAcception);
            }
            else
            {
                await FirestoreORM.AddToFirestoreAsync(currentUserOnAuth, "Users");
                SceneManager.LoadScene("Training");
            }
        }
        else
        {
            _displayer.DisplayElementForSeconds(1);
            return;
        }
    }
}
