using Firebase.Firestore;
using System;

namespace DataModels
{
    [Serializable]
    [FirestoreData]
    public class RadiusAdjustmentLog
    {
        [FirestoreProperty] public DateTime Timestamp { get; set; }
        [FirestoreProperty] public string CheckpointId { get; set; }
        [FirestoreProperty] public float OldRadiusMeters { get; set; }
        [FirestoreProperty] public float NewRadiusMeters { get; set; }
        [FirestoreProperty] public float? Hdop { get; set; }
        [FirestoreProperty] public string Reason { get; set; }
    }
}
