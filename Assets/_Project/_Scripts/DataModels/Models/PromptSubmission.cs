using Firebase.Firestore;
using System;

namespace DataModels
{
    [Serializable]
    [FirestoreData]
    public class PromptSubmission
    {
        [FirestoreProperty] public string Id { get; set; }
        [FirestoreProperty] public string AttemptId { get; set; }
        [FirestoreProperty] public string CheckpointId { get; set; }
        [FirestoreProperty] public string Text { get; set; }
        [FirestoreProperty] public PromptEvaluation Evaluation { get; set; } = new PromptEvaluation();
        [FirestoreProperty] public DateTime CreatedAt { get; set; }
    }
}
