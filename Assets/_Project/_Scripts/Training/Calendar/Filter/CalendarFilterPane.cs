using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CalendarFilterPanel : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _filterDropdown;
    [SerializeField] private Toggle _showNotificationsToggle;

    private void Start()
    {
        // ����������� dropdown
        if (_filterDropdown != null)
        {
            _filterDropdown.ClearOptions();
            _filterDropdown.AddOptions(new System.Collections.Generic.List<string>
            {
                "Все активности",
                "Мои тренировки",
                "Челленджи"
            });

            _filterDropdown.onValueChanged.AddListener(OnFilterChanged);
        }

        if (_showNotificationsToggle != null)
        {
            _showNotificationsToggle.onValueChanged.AddListener(OnNotificationsToggled);
        }
    }

    private void OnFilterChanged(int index)
    {
        var filter = (CalendarManager.ActivityFilter)index;
        CalendarManager.Instance.SetFilter(filter);
    }

    private void OnNotificationsToggled(bool isOn)
    {
    // notifications toggle changed
    }
}