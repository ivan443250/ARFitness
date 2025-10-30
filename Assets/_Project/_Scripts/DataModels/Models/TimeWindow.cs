using Firebase.Firestore;
using System;

namespace DataModels
{
    [Serializable]
    [FirestoreData]
    public class TimeWindow
    {
        [FirestoreProperty] public DateTime Start { get; set; }
        [FirestoreProperty] public DateTime End { get; set; }
    }
}
