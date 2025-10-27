using Firebase.Firestore;

[FirestoreData]
public class CloudAttemptModel
{
    [FirestoreProperty]
    public DocumentReference UserRef { get; set; }

    [FirestoreProperty]
    public DocumentReference RouteRef { get; set; }

    [FirestoreProperty]
    public DocumentReference EventRef { get; set; }

    [FirestoreProperty]
    public string AttemptType { get; set; }

    [FirestoreProperty]
    public Timestamp StartTime { get; set; }

    [FirestoreProperty]
    public Timestamp EndTime { get; set; }

    [FirestoreProperty]
    public string Status { get; set; }

    // ����������������� ������ ��� ��������
    [FirestoreProperty]
    public string UserId { get; set; }

    [FirestoreProperty]
    public string RouteName { get; set; }

    [FirestoreProperty]
    public int CheckpointsCompleted { get; set; }

    [FirestoreProperty]
    public int TotalPoints { get; set; }

    [FirestoreProperty]
    public double TotalDistance { get; set; }

    // ������������:
    // /attempts/{attemptId}/checkpointProgress/{checkpointId}
    // /attempts/{attemptId}/eventLogs/{logId}
}
