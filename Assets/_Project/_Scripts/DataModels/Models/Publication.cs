using Firebase.Firestore;
using System;

namespace DataModels
{
    [Serializable]
    [FirestoreData]
    public class Publication
    {
        [FirestoreProperty] public string Id { get; set; }
        [FirestoreProperty] public string RouteId { get; set; }
        [FirestoreProperty] public Visibility Visibility { get; set; }
        [FirestoreProperty] public string LinkedEventId { get; set; }
        [FirestoreProperty] public DateTime PublishedAt { get; set; }
        [FirestoreProperty] public string AccessCode { get; set; }
    }
}
