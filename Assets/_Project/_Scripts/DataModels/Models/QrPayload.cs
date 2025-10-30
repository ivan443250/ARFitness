using Firebase.Firestore;
using System;

namespace DataModels
{
    [Serializable]
    [FirestoreData]
    public class QrPayload
    {
        [FirestoreProperty] public string CheckpointId { get; set; }
        [FirestoreProperty] public string RouteId { get; set; }
        [FirestoreProperty] public string ChecksumHmac { get; set; }
        [FirestoreProperty] public DateTime? Exp { get; set; }
    }
}
