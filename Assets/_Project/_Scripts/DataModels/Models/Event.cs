using Firebase.Firestore;
using System;
using System.Collections.Generic;

namespace DataModels
{
    [Serializable]
    [FirestoreData]
    public class Event
    {
        [FirestoreProperty] public string Id { get; set; }
        [FirestoreProperty] public string RouteId { get; set; }
        [FirestoreProperty] public ActivityType Type { get; set; }
        [FirestoreProperty] public Visibility Visibility { get; set; }
        [FirestoreProperty] public EventStatus Status { get; set; } = EventStatus.Draft;
        [FirestoreProperty] public TimeWindow StartWindow { get; set; }
        [FirestoreProperty] public List<string> GroupIds { get; set; } = new List<string>();
        [FirestoreProperty] public List<string> AllowedUserIds { get; set; } = new List<string>();
        [FirestoreProperty] public bool SynchronousStart { get; set; } = false;
        [FirestoreProperty] public ScoreWeights Score { get; set; } = new ScoreWeights();
        [FirestoreProperty] public DateTime CreatedAt { get; set; }
    }
}
