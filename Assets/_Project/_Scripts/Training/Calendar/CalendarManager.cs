using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Firebase.Firestore;

public class CalendarManager : MonoBehaviour
{
    public static CalendarManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private ActivityFilter _currentFilter = ActivityFilter.All;

    public enum ActivityFilter
    {
        All,
        MyTrainings,
        Competitions
    }

    private List<ActivityData> _allActivities = new List<ActivityData>();
    private List<ChallengeData> _allChallenges = new List<ChallengeData>();
    private DateTime _currentMonth;
    private DateTime _selectedDate;

    public static event Action<DateTime> OnMonthChanged;
    public static event Action<DateTime> OnDateSelected;
    public static event Action<ActivityFilter> OnFilterChanged;
    public static event Action<List<ActivityData>> OnActivitiesUpdated;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _currentMonth = DateTime.Today;
            _selectedDate = DateTime.Today;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #region Public Methods

    public void SetFilter(ActivityFilter filter)
    {
        _currentFilter = filter;
        OnFilterChanged?.Invoke(filter);
        UpdateCalendarDisplay();
    }

    public void SelectDate(DateTime date)
    {
        _selectedDate = date;
        OnDateSelected?.Invoke(date);
    }

    public void NavigateMonth(int direction)
    {
        _currentMonth = _currentMonth.AddMonths(direction);
        OnMonthChanged?.Invoke(_currentMonth);
        UpdateCalendarDisplay();
    }

    public void GoToToday()
    {
        _currentMonth = DateTime.Today;
        _selectedDate = DateTime.Today;
        OnMonthChanged?.Invoke(_currentMonth);
        OnDateSelected?.Invoke(_selectedDate);
    }

    public void AddActivity(ActivityData activity)
    {
        _allActivities.Add(activity);
        OnActivitiesUpdated?.Invoke(GetFilteredActivities());
        UpdateCalendarDisplay();
    }

    public void RegisterForChallenge(string challengeId)
    {
        var challenge = _allChallenges.Find(c => c.Id == challengeId);
        if (challenge != null)
        {
            challenge.IsRegistered = true;
        }
    }

   
    #endregion

    #region Data Getters

    public List<ActivityData> GetActivitiesForDate(DateTime date)
    {
        return _allActivities.FindAll(a =>
        {
            if (a.Date == null) return false;
            DateTime activityDate = a.Date.ToDateTime();
            return activityDate.Date == date.Date;
        });
    }

    public List<ChallengeData> GetChallengesForDate(DateTime date)
    {
        return _allChallenges.FindAll(c => !c.IsRegistered);
    }

    public List<ActivityData> GetFilteredActivities()
    {
        switch (_currentFilter)
        {
            case ActivityFilter.MyTrainings:
                return _allActivities.FindAll(a => string.IsNullOrEmpty(a.ChallengeId));
            case ActivityFilter.Competitions:
                return _allActivities.FindAll(a => !string.IsNullOrEmpty(a.ChallengeId));
            default:
                return _allActivities;
        }
    }

    public bool HasActivitiesOnDate(DateTime date)
    {
        return GetActivitiesForDate(date).Count > 0;
    }

    public CalendarDayData GetDayData(DateTime date)
    {
        var activities = GetActivitiesForDate(date);
        var challenges = GetChallengesForDate(date);

        return new CalendarDayData
        {
            date = date,
            activities = activities,
            challenges = challenges,
            hasActivities = activities.Count > 0,
            hasChallenges = challenges.Count > 0,
            isToday = date.Date == DateTime.Today,
            isSelected = date.Date == _selectedDate.Date
        };
    }

    #endregion

    public void UpdateCalendarDisplay()
    {
        OnActivitiesUpdated?.Invoke(GetFilteredActivities());
    }

    

    [ContextMenu("Clear All Data")]
    public void ClearAllData()
    {
        _allActivities.Clear();
        _allChallenges.Clear();
        UpdateCalendarDisplay();
            // all data cleared
    }

}

[System.Serializable]
public class CalendarDayData
{
    public DateTime date;
    public List<ActivityData> activities;
    public List<ChallengeData> challenges;
    public bool hasActivities;
    public bool hasChallenges;
    public bool isToday;
    public bool isSelected;
}