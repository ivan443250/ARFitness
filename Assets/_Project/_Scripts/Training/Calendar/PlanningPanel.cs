using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using Firebase.Firestore;
public class PlanningPanel : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _dateText;
    [SerializeField] private TMP_Dropdown _activityTypeDropdown;
    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private TMP_InputField _distanceInput;
    [SerializeField] private TMP_InputField _durationInput;
    [SerializeField] private Toggle _notificationToggle;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _cancelButton;

    private DateTime _selectedDate;

    private void Start()
    {
        if (_cancelButton != null)
        {
            _cancelButton.onClick.RemoveAllListeners();
            _cancelButton.onClick.AddListener(OnCancelClicked);
        }

        if (_saveButton != null)
        {
            _saveButton.onClick.RemoveAllListeners();
            _saveButton.onClick.AddListener(OnSaveClicked);
        }

        if (_activityTypeDropdown != null)
        {
            _activityTypeDropdown.ClearOptions();
            _activityTypeDropdown.AddOptions(new System.Collections.Generic.List<string>
            {
                "Running",
                "Cycling",
                "Workout"
            });
        }

        if (_panel != null) _panel.SetActive(false);
    }

    public void ShowForDate(DateTime date)
    {
        _selectedDate = date;
        _dateText?.SetText($"Дата: {date:dd.MM.yyyy}");
        if (_panel != null) _panel.SetActive(true);

        // ����� �����
        _nameInput.text = "";
        _distanceInput.text = "";
        _durationInput.text = "";
        _notificationToggle.isOn = true;
    }

    private void OnSaveClicked()
    {
        // Basic validation
        if (_nameInput == null || string.IsNullOrWhiteSpace(_nameInput.text))
        {
            // Optionally show a modal or toast
            return;
        }

        int distanceVal = 0;
        int.TryParse(_distanceInput?.text ?? "0", out distanceVal);

        var newActivity = new ActivityData
        {
            Id = Guid.NewGuid().ToString(),
            Name = _nameInput.text,
            Description = string.Empty,
            Date = Firebase.Firestore.Timestamp.FromDateTime(_selectedDate.ToUniversalTime()),
            Type = _activityTypeDropdown != null && _activityTypeDropdown.options.Count > _activityTypeDropdown.value ? _activityTypeDropdown.options[_activityTypeDropdown.value].text : "",
            Distance = distanceVal,
            IsCompleted = false
        };

        ScheduleNotification(newActivity);
        _panel.SetActive(false);
    }

    private void OnCancelClicked()
    {
        _panel.SetActive(false);
    }

    private void ScheduleNotification(ActivityData activity)
    {
        // ������ ������������ �����������
        // schedule notification (implementation placeholder)
    }
}