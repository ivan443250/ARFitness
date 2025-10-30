using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Activities))]
public class ActivitiesManager : MonoBehaviour
{
    [SerializeField] private Transform _activitiesContent;
    [SerializeField] private GameObject _activityCardPrefab;
    
    private Activities _activities;

    private void Start()
    {
        _activities = GetComponent<Activities>();
        
        if (_activities != null)
        {
            _activities.OnDataUpdated += RefreshActivitiesList;
            
            if (_activities.ActivitiesData != null && _activities.ActivitiesData.Count > 0)
            {
                RefreshActivitiesList();
            }
        }
        else
        {
            Debug.LogError("Activities component not found!");
        }
    }

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

        if (_activities.ActivitiesData != null && _activities.ActivitiesData.Count > 0)
        {
            foreach (var activity in _activities.ActivitiesData)
            {
                if (activity != null)
                {
                    CreateActivityCard(activity);
                }
            }
        }

        if (_activities.ChallengesData != null && _activities.ChallengesData.Count > 0)
        {
            foreach (var challenge in _activities.ChallengesData)
            {
                if (challenge != null)
                {
                    CreateChallengeCard(challenge);
                }
            }
        }
    }

    private void CreateActivityCard(ActivityData activityData)
    {
        if (activityData == null) return;

        var cardGO = Instantiate(_activityCardPrefab, _activitiesContent);
        var card = cardGO.GetComponent<CardActivity>();

        if (card != null)
        {
            card.Initialize(activityData);
        }
        else
        {
            Debug.LogError("CardActivity component not found on prefab!");
            Destroy(cardGO);
        }
    }

    private void CreateChallengeCard(ChallengeData challengeData)
    {
        if (challengeData == null) return;

        var cardGO = Instantiate(_activityCardPrefab, _activitiesContent);
        var card = cardGO.GetComponent<CardActivity>();

        if (card != null)
        {
            card.Initialize(challengeData);
        }
        else
        {
            Debug.LogError("CardActivity component not found on prefab!");
            Destroy(cardGO);
        }
    }

    [ContextMenu("Refresh Activities UI")]
    public void ForceRefreshUI()
    {
        RefreshActivitiesList();
    }

    private void OnDestroy()
    {
        if (_activities != null)
        {
            _activities.OnDataUpdated -= RefreshActivitiesList;
        }
    }
}