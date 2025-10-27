using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActivityDataManager : MonoBehaviour
{
    public static ActivityDataManager Instance { get; private set; }

    private List<ActivityData> _activities = new List<ActivityData>();
    private List<ChallengeData> _challenges = new List<ChallengeData>();

    public static event Action<ActivityData> OnActivityAdded;
    public static event Action<ChallengeData> OnChallengeJoined;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void JoinChallenge(string challengeId)
    {
        var challenge = _challenges.FirstOrDefault(c => c.Id == challengeId);
        if (challenge != null)
        {
            challenge.IsRegistered = true;
            OnChallengeJoined?.Invoke(challenge);
        }
    }

    public void AddActivity(ActivityData activity)
    {
        _activities.Add(activity);
        OnActivityAdded?.Invoke(activity);
    }

    public ChallengeData GetChallengeById(string challengeId)
    {
        return _challenges.FirstOrDefault(c => c.Id == challengeId);
    }
   
}