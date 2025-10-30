using Firebase.Firestore;
using System;
using System.Collections.Generic;

namespace DataModels
{
    [Serializable]
    [FirestoreData]
    public class Group
    {
        [FirestoreProperty] public string Id { get; set; }
        [FirestoreProperty] public string Name { get; set; }
        [FirestoreProperty] public string OwnerTrainerId { get; set; }
        [FirestoreProperty] public List<string> MemberIds { get; set; } = new List<string>();
        [FirestoreProperty] public string LevelTag { get; set; }
        [FirestoreProperty] public DateTime CreatedAt { get; set; }
    }
}
