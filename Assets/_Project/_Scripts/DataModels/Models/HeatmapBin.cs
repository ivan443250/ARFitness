using Firebase.Firestore;
using System;

namespace DataModels
{
    [Serializable]
    [FirestoreData]
    public class HeatmapBin
    {
        [FirestoreProperty] public string Id { get; set; }
        [FirestoreProperty] public GeoPoint Center { get; set; }
        [FirestoreProperty] public int AttemptsCount { get; set; }
        [FirestoreProperty] public int FailedConfirmations { get; set; }
    }
}
