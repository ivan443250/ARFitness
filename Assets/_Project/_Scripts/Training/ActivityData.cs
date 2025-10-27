using Firebase.Firestore;
using System;
using System.Collections.Generic;

[Serializable]
[FirestoreData]
public class ActivityData
{
    [FirestoreProperty("id")]
    public string Id { get; set; }

    [FirestoreProperty("name ")]
    public string Name { get; set; }

    [FirestoreProperty("description ")]
    public string Description { get; set; }

    [FirestoreProperty("date")]
    public Timestamp Date { get; set; }

    [FirestoreProperty("type ")]
    public string Type { get; set; }

    [FirestoreProperty("distance ")]
    public int Distance { get; set; }

    [FirestoreProperty("duration ")]
    public List<int> Duration { get; set; }

    [FirestoreProperty("challengeId")]
    public string ChallengeId { get; set; }

    [FirestoreProperty("isCompleted ")]
    public bool IsCompleted { get; set; }


    public ActivityData()
    {
        Duration = new List<int> { 0, 0, 0, 0 }; // 4 �������� ��� � ����
        ChallengeId = "";
    }

    public override string ToString()
    {
        return $"Activity: {Name}, Description: {Description}, Type: {Type}, Distance: {Distance}m, Duration: [{string.Join(", ", Duration)}], Date: {Date.ToDateTime()}, Completed: {IsCompleted}";
    }
}

[Serializable]
[FirestoreData]
public class ChallengeData
{
    [FirestoreProperty("id")]
    public string Id { get; set; }

    [FirestoreProperty("name")]
    public string Name { get; set; }

    [FirestoreProperty("description")]
    public string Description { get; set; }

    [FirestoreProperty("type")]
    public string Type { get; set; }

    [FirestoreProperty("distance")]
    public int Distance { get; set; }

    [FirestoreProperty("duration")]
    public List<int> Duration { get; set; }

    [FirestoreProperty("isCompleted")]
    public bool IsCompleted { get; set; }

    [FirestoreProperty("isRegistered")]
    public bool IsRegistered { get; set; }

    public ChallengeData()
    {
        Duration = new List<int> { 0, 0, 0, 0 };
    }

    public override string ToString()
    {
        return $"Challenge: {Name}, Description: {Description}, Type: {Type}, Distance: {Distance}km, Duration: [{string.Join(", ", Duration)}], Completed: {IsCompleted}, Registered: {IsRegistered}";
    }
}