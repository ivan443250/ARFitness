using Firebase.Firestore;
using System;
using System.Collections.Generic;

namespace DataModels
{
    [Serializable]
    [FirestoreData]
    public class Route
    {
        [FirestoreProperty] public string Id { get; set; }
        [FirestoreProperty] public string OwnerId { get; set; }
        [FirestoreProperty] public string City { get; set; }
        [FirestoreProperty] public string Region { get; set; }
        [FirestoreProperty] public Visibility Visibility { get; set; }
        [FirestoreProperty] public float DefaultRadiusMeters { get; set; } = 20f;
        [FirestoreProperty] public int Version { get; set; } = 1;
        [FirestoreProperty] public DateTime CreatedAt { get; set; }
        [FirestoreProperty] public RouteStatus Status { get; set; } = RouteStatus.Draft;
        [FirestoreProperty] public List<Checkpoint> Checkpoints { get; set; } = new List<Checkpoint>();
        [FirestoreProperty] public List<string> Tags { get; set; } = new List<string>();
    }
}
