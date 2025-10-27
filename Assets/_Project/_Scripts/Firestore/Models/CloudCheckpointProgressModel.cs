using Firebase.Firestore;

[FirestoreData]
public class CloudCheckpointProgressModel
{
    [FirestoreProperty]
    public DocumentReference CheckpointRef { get; set; }

    [FirestoreProperty]
    public int OrderIndex { get; set; }

    [FirestoreProperty]
    public bool Completed { get; set; }

    [FirestoreProperty]
    public Timestamp EnterRadiusTime { get; set; }

    [FirestoreProperty]
    public Timestamp QrScanTime { get; set; }

    [FirestoreProperty]
    public Timestamp ExerciseEndTime { get; set; }

    [FirestoreProperty]
    public Timestamp PromptSubmitTime { get; set; }

    [FirestoreProperty]
    public int ExerciseRepetitions { get; set; }

    [FirestoreProperty]
    public string PromptGoal { get; set; }

    [FirestoreProperty]
    public string PromptData { get; set; }

    [FirestoreProperty]
    public int PointsEarned { get; set; }

    // Для антифрода
    [FirestoreProperty]
    public GeoPoint VerificationLocation { get; set; }

    [FirestoreProperty]
    public double Hdop { get; set; }
}
