using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ModalWindow : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _messageText;
    [SerializeField] private Button _approveButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private TMP_Text _approveButtonText;
    [SerializeField] private TMP_Text _cancelButtonText;

    [Header("Date Picker Components")]
    [SerializeField] private GameObject _datePickerPanel;
    [SerializeField] private TMP_Text _selectedDateText;
    [SerializeField] private Button _prevDayButton;
    [SerializeField] private Button _nextDayButton;

    private Action _onApprove;
    private Action _onCancel;
    private Action _onClose;
    private DateTime _currentDate;
    private Action<DateTime> _onDateSelected;
    private bool _hasDatePicker = false;

    private void Awake()
    {
        if (_approveButton != null)
        {
            _approveButton.onClick.RemoveAllListeners();
            _approveButton.onClick.AddListener(OnApprove);
        }

        if (_cancelButton != null)
        {
            _cancelButton.onClick.RemoveAllListeners();
            _cancelButton.onClick.AddListener(OnCancel);
        }

        if (_closeButton != null)
        {
            _closeButton.onClick.RemoveAllListeners();
            _closeButton.onClick.AddListener(OnClose);
        }
    }

    public void Initialize(ModalRequest request)
    {
        if (_datePickerPanel != null)
            _datePickerPanel.SetActive(false);

        _hasDatePicker = false;
        SetTextImmediately(request);
        SetupCallbacks(request);
        SetupButtonsVisibility(request);
    }

    public void InitializeWithDatePicker(ModalRequest request, DateTime initialDate, Action<DateTime> onDateSelected)
    {
        _currentDate = initialDate;
        _onDateSelected = onDateSelected;
        _hasDatePicker = true;

        if (_datePickerPanel != null)
        {
            _datePickerPanel.SetActive(true);
            SetupDatePicker();
        }
        else
        {
            Debug.LogError("Date Picker Panel is not assigned in inspector!");
        }

        SetTextImmediately(request);
        SetupCallbacks(request);
        SetupButtonsVisibility(request);
    }

    private void SetTextImmediately(ModalRequest request)
    {
        _titleText?.SetText(string.IsNullOrWhiteSpace(request.title) ? "Сообщение" : request.title);
        _messageText?.SetText(request.message ?? string.Empty);
        _approveButtonText?.SetText(request.approveText ?? string.Empty);
        _cancelButtonText?.SetText(request.cancelText ?? string.Empty);

        if (_hasDatePicker && _selectedDateText != null)
        {
            _selectedDateText.SetText(_currentDate.ToString("dd.MM.yyyy"));
        }
    }

    private void SetupCallbacks(ModalRequest request)
    {
        _onApprove = request.onApprove;
        _onCancel = request.onCancel;
        _onClose = request.onClose;
    }

    private void SetupButtonsVisibility(ModalRequest request)
    {
        if (_approveButton != null)
        {
            _approveButton.gameObject.SetActive(_onApprove != null && !string.IsNullOrWhiteSpace(request.approveText));
        }

        if (_cancelButton != null)
        {
            _cancelButton.gameObject.SetActive(_onCancel != null && !string.IsNullOrWhiteSpace(request.cancelText));
        }
    }

    private void SetupDatePicker()
    {
        if (_selectedDateText == null || _prevDayButton == null || _nextDayButton == null)
        {
            Debug.LogError("Date picker components are not properly assigned!");
            return;
        }

        UpdateDateDisplay();

        _prevDayButton.onClick.RemoveAllListeners();
        _nextDayButton.onClick.RemoveAllListeners();

        _prevDayButton.onClick.AddListener(OnPrevDayClicked);
        _nextDayButton.onClick.AddListener(OnNextDayClicked);
    }

    private void OnPrevDayClicked()
    {
        ChangeDate(-1);
    }

    private void OnNextDayClicked()
    {
        ChangeDate(1);
    }

    private void ChangeDate(int days)
    {
        _currentDate = _currentDate.AddDays(days);
        UpdateDateDisplay();
        _onDateSelected?.Invoke(_currentDate);
    }

    private void UpdateDateDisplay()
    {
        if (_selectedDateText != null)
        {
            _selectedDateText.SetText(_currentDate.ToString("dd.MM.yyyy"));

            if (_prevDayButton != null)
            {
                _prevDayButton.interactable = _currentDate > DateTime.Today;
            }
        }
        else
        {
            Debug.LogError("Selected Date Text is not assigned!");
        }
    }

    private void OnDestroy()
    {
        if (_prevDayButton != null) _prevDayButton.onClick.RemoveListener(OnPrevDayClicked);
        if (_nextDayButton != null) _nextDayButton.onClick.RemoveListener(OnNextDayClicked);
        if (_approveButton != null) _approveButton.onClick.RemoveListener(OnApprove);
        if (_cancelButton != null) _cancelButton.onClick.RemoveListener(OnCancel);
        if (_closeButton != null) _closeButton.onClick.RemoveListener(OnClose);
    }

    private void OnApprove()
    {
        if (_hasDatePicker)
        {
            _onDateSelected?.Invoke(_currentDate);
        }

        ExecuteAction(_onApprove);
    }

    private void OnCancel()
    {
        ExecuteAction(_onCancel);
    }

    private void OnClose()
    {
        ExecuteAction(_onClose);
    }

    private void ExecuteAction(Action action)
    {
        action?.Invoke();
        Close();
    }

    private void Close()
    {
        Destroy(gameObject);
    }

    [ContextMenu("Debug Date Picker State")]
    private void DebugDatePickerState()
    {
        // debug info available in editor when needed
    }
}