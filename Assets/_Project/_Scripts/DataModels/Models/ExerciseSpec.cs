using Firebase.Firestore;
using System;

namespace DataModels
{
    [Serializable]
    [FirestoreData]
    public class ExerciseSpec
    {
        [FirestoreProperty] public ExerciseType Type { get; set; } = ExerciseType.Unknown;
        [FirestoreProperty] public int Reps { get; set; } = 0;
        [FirestoreProperty] public ExerciseVerification Verification { get; set; } = ExerciseVerification.Accelerometer;
    }
}
