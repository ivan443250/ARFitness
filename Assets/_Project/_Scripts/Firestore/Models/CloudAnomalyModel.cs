using Firebase.Firestore;

[FirestoreData]
public class CloudAnomalyModel
{
    [FirestoreProperty]
    public DocumentReference AttemptRef { get; set; }

    [FirestoreProperty]
    public DocumentReference UserRef { get; set; }

    [FirestoreProperty]
    public string AnomalyType { get; set; }

    [FirestoreProperty]
    public string Description { get; set; }

    [FirestoreProperty]
    public double Speed { get; set; }

    [FirestoreProperty]
    public double Distance { get; set; }

    [FirestoreProperty]
    public double TimeDifference { get; set; }

    [FirestoreProperty]
    public Timestamp Timestamp { get; set; }

    [FirestoreProperty]
    public bool Reviewed { get; set; }

    [FirestoreProperty]
    public string ReviewDecision { get; set; }

    [FirestoreProperty]
    public string ReviewerId { get; set; }

    // Денормализованные для фильтрации
    [FirestoreProperty]
    public string UserName { get; set; }

    [FirestoreProperty]
    public string RouteName { get; set; }
}
