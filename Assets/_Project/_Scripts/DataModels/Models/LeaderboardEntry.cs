using Firebase.Firestore;
using System;

namespace DataModels
{
    [Serializable]
    [FirestoreData]
    public class LeaderboardEntry
    {
        [FirestoreProperty] public string Id { get; set; }
        [FirestoreProperty] public string UserId { get; set; }
        [FirestoreProperty] public float TotalPoints { get; set; }
        [FirestoreProperty] public float? TotalTimeSec { get; set; }
        [FirestoreProperty] public int Rank { get; set; }
        [FirestoreProperty] public DateTime CalculatedAt { get; set; }
    }
}
