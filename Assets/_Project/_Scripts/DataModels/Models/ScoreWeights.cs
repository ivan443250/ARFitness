using Firebase.Firestore;
using System;

namespace DataModels
{
    [Serializable]
    [FirestoreData]
    public class ScoreWeights
    {
        [FirestoreProperty] public float Speed { get; set; } = 1f;
        [FirestoreProperty] public float PromptQuality { get; set; } = 1f;
        [FirestoreProperty] public float HonestyLog { get; set; } = 1f;
    }
}
