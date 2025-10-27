using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using Firebase.Firestore;

public class CardActivity : MonoBehaviour, IDataCard<ActivityData>, IInitializable<ChallengeData>
{
    private ActivityData _activityData;
    private ChallengeData _challengeData;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _distance;
    [SerializeField] private TMP_Text _duration;
    [SerializeField] private TMP_Text _type;
    [SerializeField] private TMP_Text _date; 
    [SerializeField] private Button _actionButton;

    private bool _isChallenge = false;

    private void Awake()
    {
        if (_actionButton != null)
        {
            _actionButton.onClick.AddListener(OnActionButtonClick);
        }
    }
    public void Initialize(ActivityData activityData)
    {
        _activityData = activityData;
        _challengeData = null;
        _isChallenge = false;
        SetData();
    }

    public void Initialize(ChallengeData challengeData)
    {
        _challengeData = challengeData;
        _activityData = null;
        _isChallenge = true;
        SetData();
    }

    public void SetData()
    {
        if (_isChallenge)
        {
            SetChallengeData();
        }
        else
        {
            SetActivityData();
        }

        UpdateActionButton();
    }

    private void SetActivityData()
    {
        if (_activityData == null)
        {
            Debug.LogError("Activity data is null");
            return;
        }

        _name?.SetText(_activityData.Name ?? string.Empty);
        _description?.SetText(_activityData.Description ?? string.Empty);
    _type?.SetText(_activityData.Type ?? string.Empty);
        if (_distance != null)
        {
            // Distance stored as meters — display in km with one decimal if > 1000m
            float meters = _activityData.Distance;
            if (meters >= 1000f)
                _distance.text = $"{meters / 1000f:F1} км";
            else
                _distance.text = $"{meters} м";
        }

        if (_date != null)
        {
            try
            {
                var dt = _activityData.Date.ToDateTime();
                _date.SetText(dt.ToString("dd.MM.yyyy"));
            }
            catch
            {
                _date.SetText(string.Empty);
            }
        }

        if (_duration != null)
        {
            if (_activityData.Duration != null && _activityData.Duration.Count >= 3)
            {
                _duration.SetText($"{_activityData.Duration[0]}:{_activityData.Duration[1]:D2}:{_activityData.Duration[2]:D2}");
            }
            else
            {
                _duration.SetText(string.Empty);
            }
        }
    }

    private void SetChallengeData()
    {
        if (_challengeData == null)
        {
            Debug.LogError("Challenge data is null");
            return;
        }

    _name?.SetText(_challengeData.Name ?? string.Empty);
    _description?.SetText(_challengeData.Description ?? string.Empty);
    _type?.SetText(_challengeData.Type ?? string.Empty);
        if (_distance != null) _distance.text = "Челлендж";
        if (_duration != null)
        {
            // Duration stored as List<int> — try to format as H:M:S if possible
            if (_challengeData.Duration != null && _challengeData.Duration.Count >= 3)
            {
                _duration.SetText($"{_challengeData.Duration[0]}:{_challengeData.Duration[1]:D2}:{_challengeData.Duration[2]:D2}");
            }
            else
            {
                _duration.SetText(string.Empty);
            }
        }
        _date?.SetText(string.Empty);
    }

    private void UpdateActionButton()
    {
        if (_actionButton != null)
        {
            var buttonText = _actionButton.GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                buttonText.text = _isChallenge ? "Участвовать" : "Записаться";
            }
        }
    }

    private void OnActionButtonClick()
    {
        if (_isChallenge)
        {
            RegisterForChallenge();
        }
        else
        {
            RegisterForRegularActivity();
        }
    }

    private void RegisterForChallenge()
    {
        if (ModalManager.Instance == null)
        {
            Debug.LogError("ModalManager is not available");
            return;
        }

        var request = new ModalRequest
        {
            title = "Участие в челлендже",
            message = $"Хотите участвовать в челлендже \"{_challengeData.Name}\"?",
            approveText = "Участвовать",
            cancelText = "Отмена",
            onApprove = CompleteChallengeRegistration,
            onCancel = OnRegistrationCancelled
        };

        ModalManager.Instance.ShowModal(request);
    }

    private void RegisterForRegularActivity()
    {
        if (ModalManager.Instance == null)
        {
            Debug.LogError("ModalManager is not available");
            return;
        }

        var request = new ModalRequest
        {
            title = "Запись на тренировку",
            message = $"Выберите дату для \"{_activityData.Name}\":",
            approveText = "Записаться",
            cancelText = "Отмена",
            showDatePicker = true,
            initialDate = DateTime.Today,
            onApprove = () => {
                OnRegularActivityApproved(ModalManager.Instance.GetSelectedDate());
                FindAnyObjectByType<CalendarGrid>().RefreshDayCellsAppearance();
            },
            onCancel = OnRegistrationCancelled
        };

        ModalManager.Instance.ShowModalWithDatePicker(request);
    }

    private void OnRegularActivityApproved(DateTime selectedDate)
    {
        if (selectedDate < DateTime.Today)
        {
            ModalManager.Instance?.ShowAlertModal("Нельзя записаться на прошедшую дату!");
            return;
        }

        CompleteRegularRegistration(selectedDate);
    }

    private void CompleteChallengeRegistration()
    {
        try
        {
            if (ActivityDataManager.Instance != null && _challengeData != null)
            {
                ActivityDataManager.Instance.JoinChallenge(_challengeData.Id);
            }

            ModalManager.Instance?.ShowAlertModal(
                $"🎉 Вы участвуете в челлендже!\n\"{_challengeData.Name}\"",
                closeText: "Отлично!"
            );
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Ошибка: {e.Message}");
            ModalManager.Instance?.ShowAlertModal("Ошибка при записи на челлендж");
        }
    }

    private void CompleteRegularRegistration(DateTime selectedDate)
    {
       
    }

    private void OnRegistrationCancelled()
    {
        // registration cancelled
    }
}