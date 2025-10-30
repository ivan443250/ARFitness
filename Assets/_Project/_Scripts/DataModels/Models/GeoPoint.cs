using Firebase.Firestore;
using System;

namespace DataModels
{
    [Serializable]
    [FirestoreData]
    public class GeoPoint
    {
        [FirestoreProperty] public float Latitude { get; set; }
        [FirestoreProperty] public float Longitude { get; set; }
    }
}
