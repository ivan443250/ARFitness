using Firebase.Firestore;
using System;

namespace DataModels
{
    [Serializable]
    [FirestoreData]
    public class Checkpoint
    {
        [FirestoreProperty] public string Id { get; set; }
        [FirestoreProperty] public int Index { get; set; }
        [FirestoreProperty] public string Name { get; set; }
        [FirestoreProperty] public string Hint { get; set; }
        [FirestoreProperty] public GeoPoint Location { get; set; }
        [FirestoreProperty] public float RadiusMeters { get; set; }
        [FirestoreProperty] public int? SegmentTimeLimitSec { get; set; }
        [FirestoreProperty] public ConfirmationMode Confirmation { get; set; } = ConfirmationMode.GpsAndQr;
        [FirestoreProperty] public ExerciseSpec Exercise { get; set; }
        [FirestoreProperty] public string PromptRulesetId { get; set; }
        [FirestoreProperty] public string QrControlHash { get; set; }
    }
}
