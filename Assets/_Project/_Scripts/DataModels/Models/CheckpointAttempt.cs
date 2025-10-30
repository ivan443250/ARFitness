using Firebase.Firestore;
using System;

namespace DataModels
{
    [Serializable]
    [FirestoreData]
    public class CheckpointAttempt
    {
        [FirestoreProperty] public string CheckpointId { get; set; }
        [FirestoreProperty] public int Index { get; set; }
        [FirestoreProperty] public CheckpointProgressState State { get; set; } = CheckpointProgressState.OutOfRadius;
        [FirestoreProperty] public DateTime? EnteredRadiusAt { get; set; }
        [FirestoreProperty] public DateTime? QrScannedAt { get; set; }
        [FirestoreProperty] public DateTime? ExerciseDoneAt { get; set; }
        [FirestoreProperty] public string PromptSubmissionId { get; set; }
        [FirestoreProperty] public float SegmentDistanceMeters { get; set; }
        [FirestoreProperty] public float SegmentTimeSec { get; set; }
        [FirestoreProperty] public float SegmentScore { get; set; }
    }
}
