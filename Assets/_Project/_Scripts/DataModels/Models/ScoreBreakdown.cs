using Firebase.Firestore;
using System;

namespace DataModels
{
    [Serializable]
    [FirestoreData]
    public class ScoreBreakdown
    {
        [FirestoreProperty] public float PointsSpeed { get; set; }
        [FirestoreProperty] public float PointsPrompt { get; set; }
        [FirestoreProperty] public float PointsHonesty { get; set; }
        [FirestoreProperty] public float Bonus { get; set; }
    }
}
