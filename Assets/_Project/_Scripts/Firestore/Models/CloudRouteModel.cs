using Firebase.Firestore;

[FirestoreData]
public class CloudRouteModel
{
    [FirestoreProperty]
    public string Name { get; set; }

    [FirestoreProperty]
    public string OwnerId { get; set; }

    [FirestoreProperty]
    public string City { get; set; }

    [FirestoreProperty]
    public string Visibility { get; set; }

    [FirestoreProperty]
    public double DefaultRadius { get; set; }

    [FirestoreProperty]
    public string RouteType { get; set; }

    // ��������� ��� �������� �� ��������������
    [FirestoreProperty]
    public GeoPoint CenterLocation { get; set; }

    [FirestoreProperty]
    public double RadiusMeters { get; set; }

    [FirestoreProperty]
    public Timestamp CreatedAt { get; set; }

    // ����������������� ����������
    [FirestoreProperty]
    public int TotalCompletions { get; set; }

    [FirestoreProperty]
    public double AverageRating { get; set; }

    // ������������ ����� ��������� ��������
    // /routes/{routeId}/checkpoints/{checkpointId}
}
