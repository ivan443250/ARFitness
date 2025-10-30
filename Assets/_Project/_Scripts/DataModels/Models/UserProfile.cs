using Firebase.Firestore;
using System;
using System.Collections.Generic;

namespace DataModels
{
    [Serializable]
    [FirestoreData]
    public class UserProfile
    {
        [FirestoreProperty] public string Id { get; set; }
        [FirestoreProperty] public string Email { get; set; }
        [FirestoreProperty] public string DisplayName { get; set; }
        [FirestoreProperty] public string AvatarUrl { get; set; }
        [FirestoreProperty] public int? Age { get; set; }
        [FirestoreProperty] public Gender Gender { get; set; }
        [FirestoreProperty] public FitnessLevel FitnessLevel { get; set; }
        [FirestoreProperty] public List<RoleType> Roles { get; set; } = new List<RoleType>();
        [FirestoreProperty] public List<string> GroupIds { get; set; } = new List<string>();
        [FirestoreProperty] public bool ConsentAccepted { get; set; }
        [FirestoreProperty] public DateTime CreatedAt { get; set; }
    }
}
