using Firebase.Firestore;
using System;

namespace DataModels
{
    [Serializable]
    [FirestoreData]
    public class AnomalyLog
    {
        [FirestoreProperty] public DateTime Timestamp { get; set; }
        [FirestoreProperty] public AnomalyFlagType FlagType { get; set; }
        [FirestoreProperty] public float? SpeedMps { get; set; }
        [FirestoreProperty] public string FromCheckpointId { get; set; }
        [FirestoreProperty] public string ToCheckpointId { get; set; }
        [FirestoreProperty] public string Note { get; set; }
    }
}
