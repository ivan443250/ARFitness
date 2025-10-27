using Firebase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FirebaseLoadData : MonoBehaviour
{
    [Header("Loaded Data")]
    private List<ActivityData> _activities = new List<ActivityData>();
    private List<ChallengeData> _challenges = new List<ChallengeData>();
    public List<ActivityData> Activities => _activities;
    public List<ChallengeData> Challenges => _challenges;

    [Header("Events")]
    public UnityEvent onDataLoaded;
    public UnityEvent onDataLoadFailed;

    private void Start()
    {
        StartCoroutine(InitializeFirebaseCoroutine());
    }

    private IEnumerator InitializeFirebaseCoroutine()
    {
    var task = FirebaseApp.CheckAndFixDependenciesAsync();
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Exception != null)
        {
            Debug.LogError($"Failed to initialize Firebase: {task.Exception}");
            onDataLoadFailed?.Invoke();
            yield break;
        }

        var dependencyStatus = task.Result;
        if (dependencyStatus == DependencyStatus.Available)
        {
            yield return StartCoroutine(LoadAllDataCoroutine());
        }
        else
        {
            Debug.LogError($"Could not resolve Firebase dependencies: {dependencyStatus}");
            onDataLoadFailed?.Invoke();
        }
    }
    private IEnumerator LoadAllDataCoroutine()
    {
        var activitiesTask = _FirestoreORM.GetAllActivitiesAsync();
        yield return new WaitUntil(() => activitiesTask.IsCompleted);

        if (activitiesTask.Exception != null)
        {
            Debug.LogError($"Failed to load activities: {activitiesTask.Exception}");
        }
        else
        {
            _activities = activitiesTask.Result;
        }
    

        var challengesTask = _FirestoreORM.GetAllChallengesAsync();
        yield return new WaitUntil(() => challengesTask.IsCompleted);

        if (challengesTask.Exception != null)
        {
            Debug.LogError($"Failed to load challenges: {challengesTask.Exception}");
        }
        else
        {
            _challenges = challengesTask.Result;
        }


        if ((_activities != null && _activities.Count > 0) || (_challenges != null && _challenges.Count > 0))
        {
            onDataLoaded?.Invoke();
        }
        else
        {
            onDataLoadFailed?.Invoke();
        }
    }

    public void PrintLoadedData()
    {
    }
        
    public void RefreshData()
    {
        StartCoroutine(LoadAllDataCoroutine());
    }
}