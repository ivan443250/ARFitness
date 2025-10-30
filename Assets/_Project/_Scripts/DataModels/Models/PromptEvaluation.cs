using Firebase.Firestore;
using System;

namespace DataModels
{
    [Serializable]
    [FirestoreData]
    public class PromptEvaluation
    {
        [FirestoreProperty] public bool Passed { get; set; }
        [FirestoreProperty] public float Structure { get; set; }
        [FirestoreProperty] public float Meaning { get; set; }
        [FirestoreProperty] public float Uniqueness { get; set; }
        [FirestoreProperty] public float TotalScore { get; set; }
        [FirestoreProperty] public string Feedback { get; set; }
    }
}
