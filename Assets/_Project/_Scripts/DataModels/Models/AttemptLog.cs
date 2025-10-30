using Firebase.Firestore;
using System;
using System.Collections.Generic;

namespace DataModels
{
    [Serializable]
    [FirestoreData]
    public class AttemptLog
    {
        [FirestoreProperty] public DateTime Timestamp { get; set; }
        [FirestoreProperty] public GeoPoint Position { get; set; }
        [FirestoreProperty] public float? AccuracyMeters { get; set; }
        [FirestoreProperty] public float? Hdop { get; set; }
        [FirestoreProperty] public LogAction Action { get; set; }
        [FirestoreProperty] public Dictionary<string, string> Extras { get; set; } = new Dictionary<string, string>();
    }
}
