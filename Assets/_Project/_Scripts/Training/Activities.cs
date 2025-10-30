using Firebase.Firestore;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Activities : MonoBehaviour
{
    [SerializeField] private List<ActivityData> _activitiesData;
    [SerializeField] private List<ChallengeData> _challengesData;

    public List<ActivityData> ActivitiesData { get => _activitiesData; set => _activitiesData = value; }
    public List<ChallengeData> ChallengesData { get => _challengesData; set => _challengesData = value; }

    [Header("Firebase Reference")]
    [SerializeField] private FirebaseLoadData _firebaseLoadData;

    public event Action OnDataUpdated;

    private void Start()
    {
        if (_firebaseLoadData == null)
        {
            _firebaseLoadData = FindObjectOfType<FirebaseLoadData>();
        }

        if (_firebaseLoadData != null)
        {
            _firebaseLoadData.onDataLoaded.AddListener(OnFirebaseDataLoaded);
            _firebaseLoadData.onDataLoadFailed.AddListener(OnFirebaseDataLoadFailed);
        }
        else
        {
            Debug.LogError("FirebaseLoadData not found!");
            LoadTestData();
            NotifyDataUpdated();
        }
    }

    private void OnFirebaseDataLoaded()
    {
        _activitiesData = _firebaseLoadData.Activities;
        _challengesData = _firebaseLoadData.Challenges;
        NotifyDataUpdated();
    }

    private void OnFirebaseDataLoadFailed()
    {
        LoadTestData();
        NotifyDataUpdated();
    }

    private void LoadTestData()
    {
        _activitiesData = new List<ActivityData>
        {
            new ActivityData {
                Name = "Тестовая активность",
                Description = "Загрузка из Firebase не удалась",
                Type = "running"
            }
        };

        _challengesData = new List<ChallengeData>
        {
            new ChallengeData {
                Name = "Тестовый челлендж",
                Description = "Загрузка из Firebase не удалась"
            }
        };
    }

    public void UpdateActivitiesData(List<ActivityData> firebaseData)
    {
        if (firebaseData == null || firebaseData.Count == 0)
        {
            return;
        }

        if (_activitiesData == null)
        {
            _activitiesData = new List<ActivityData>();
        }
        else
        {
            _activitiesData.Clear();
        }

        _activitiesData.AddRange(firebaseData);
        NotifyDataUpdated();
    }

    public void RegisterForActivity(ActivityData activityData, DateTime selectedDate)
    {
    }

    private void NotifyDataUpdated()
    {
        OnDataUpdated?.Invoke();
    }

    private void OnDestroy()
    {
        if (_firebaseLoadData != null)
        {
            _firebaseLoadData.onDataLoaded.RemoveListener(OnFirebaseDataLoaded);
            _firebaseLoadData.onDataLoadFailed.RemoveListener(OnFirebaseDataLoadFailed);
        }
    }
}