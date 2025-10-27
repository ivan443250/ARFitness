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

    [Header("UI Reference")]
    [SerializeField] private Transform _activitiesContent;
    [SerializeField] private GameObject _activityCardPrefab;

    [Header("Firebase Reference")]
    [SerializeField] private FirebaseLoadData _firebaseLoadData;

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
            RefreshActivitiesList();
        }
    }

    private void OnFirebaseDataLoaded()
    {
        _activitiesData = _firebaseLoadData.Activities;
        _challengesData = _firebaseLoadData.Challenges;

        RefreshActivitiesList();
    }

    private void OnFirebaseDataLoadFailed()
    {
        LoadTestData();
        RefreshActivitiesList();
    }

    private void LoadTestData()
    {
        _activitiesData = new List<ActivityData>
        {
            new ActivityData {
                Name = "��������� ����������",
                Description = "������ �� Firebase �� �����������",
                Type = "running"
            }
        };

        _challengesData = new List<ChallengeData>
        {
            new ChallengeData {
                Name = "��������� ��������",
                Description = "������ �� Firebase �� �����������"
            }
        };

    // test data loaded
    }

    [ContextMenu("Refresh Activities List")]
    public void RefreshActivitiesList()
    {
        if (_activitiesContent == null || _activityCardPrefab == null)
        {
            Debug.LogError("UI references not assigned!");
            return;
        }

        foreach (Transform child in _activitiesContent)
        {
            Destroy(child.gameObject);
        }

        if (_activitiesData != null && _activitiesData.Count > 0)
        {
            foreach (var activity in _activitiesData)
            {
                if (activity != null)
                {
                    CreateCard(activity);
                }
            }
        }

        if (_challengesData != null && _challengesData.Count > 0)
        {
            foreach (var challenge in _challengesData)
            {
                if (challenge != null)
                {
                    CreateCard(challenge);
                }
            }
        }
    }

    private void CreateCard(ActivityData activityData)
    {
        if (activityData == null)
        {
            return;
        }

        InstantiateCard(card => card.Initialize(activityData));
    }

    private void CreateCard(ChallengeData challengeData)
    {
        if (challengeData == null)
        {
            return;
        }

        InstantiateCard(card => card.Initialize(challengeData));
    }

    private void InstantiateCard(Action<CardActivity> initAction)
    {
        if (_activityCardPrefab == null || _activitiesContent == null)
        {
            Debug.LogError("UI references not assigned!");
            return;
        }

        var cardGO = Instantiate(_activityCardPrefab, _activitiesContent);
        var card = cardGO.GetComponent<CardActivity>();

        if (card != null)
        {
            initAction?.Invoke(card);
        }
        else
        {
            Debug.LogError("CardActivity component not found on prefab!");
            Destroy(cardGO);
        }
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

        RefreshActivitiesList();
    }

    public void RegisterForActivity(ActivityData activityData, DateTime selectedDate)
    {
        // registration handled elsewhere
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