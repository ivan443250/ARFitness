using Firebase.Firestore;

[FirestoreData]
public class CloudEvenLogModel
{
    [FirestoreProperty]
    public string EventType { get; set; } // "checkpointEnter", "qrScan", "exercise", "prompt"

    [FirestoreProperty]
    public DocumentReference CheckpointRef { get; set; }

    [FirestoreProperty]
    public Timestamp Timestamp { get; set; }

    [FirestoreProperty]
    public GeoPoint Location { get; set; }

    [FirestoreProperty]
    public double Accuracy { get; set; }

    [FirestoreProperty]
    public double Hdop { get; set; }

    [FirestoreProperty]
    public string Data { get; set; } // JSON с дополнительными данными

    [FirestoreProperty]
    public bool OfflineMode { get; set; }

    [FirestoreProperty]
    public bool Synced { get; set; }
}
