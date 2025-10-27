using Firebase.Firestore;

[FirestoreData]
public class CloudCheckpointModel
{
    [FirestoreProperty]
    public int OrderIndex { get; set; }

    [FirestoreProperty]
    public string Name { get; set; }

    [FirestoreProperty]
    public GeoPoint Location { get; set; }

    [FirestoreProperty]
    public double Radius { get; set; }

    [FirestoreProperty]
    public string ExerciseType { get; set; }

    [FirestoreProperty]
    public int TargetRepetitions { get; set; }

    [FirestoreProperty]
    public string PromptKeyword { get; set; }

    [FirestoreProperty]
    public string QrControlHash { get; set; }

    // Референс на родительский маршрут
    [FirestoreProperty]
    public DocumentReference RouteRef { get; set; }
}
