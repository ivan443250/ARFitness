using Firebase.Firestore;
using System;
using System.Collections.Generic;

namespace DataModels
{
    [Serializable]
    [FirestoreData]
    public class Attempt
    {
        [FirestoreProperty] public string Id { get; set; }
        [FirestoreProperty] public string UserId { get; set; }
        [FirestoreProperty] public string RouteId { get; set; }
        [FirestoreProperty] public int RouteVersion { get; set; } = 1;
        [FirestoreProperty] public string EventId { get; set; }
        [FirestoreProperty] public AttemptState State { get; set; } = AttemptState.NotStarted;
        [FirestoreProperty] public DateTime? StartedAt { get; set; }
        [FirestoreProperty] public DateTime? FinishedAt { get; set; }
        [FirestoreProperty] public float? TotalDistanceMeters { get; set; }
        [FirestoreProperty] public ScoreBreakdown Score { get; set; } = new ScoreBreakdown();
        [FirestoreProperty] public List<CheckpointAttempt> Checkpoints { get; set; } = new List<CheckpointAttempt>();
        [FirestoreProperty] public List<AttemptLog> Logs { get; set; } = new List<AttemptLog>();
        [FirestoreProperty] public List<AnomalyLog> Anomalies { get; set; } = new List<AnomalyLog>();
    }
}
