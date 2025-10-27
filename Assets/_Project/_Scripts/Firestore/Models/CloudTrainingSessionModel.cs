using Firebase.Firestore;
using System;

[FirestoreData]
public class CloudTrainingSessionModel
{
    [FirestoreDocumentId]
    public string Id { get; set; }

    [FirestoreProperty]
    public string RouteId { get; set; }

    [FirestoreProperty]
    public string TrainerId { get; set; }

    [FirestoreProperty]
    public string GroupId { get; set; } // Для групповых тренировок

    // 🔥 ДАТЫ ТРЕНИРОВКИ
    [FirestoreProperty]
    public Timestamp ScheduledDate { get; set; } // Дата проведения

    [FirestoreProperty]
    public TimeSpan StartTime { get; set; } // Время начала (если повторяющаяся)

    [FirestoreProperty]
    public TimeSpan Duration { get; set; } // Продолжительность

    [FirestoreProperty]
    public string Recurrence { get; set; } // "none", "daily", "weekly", "monthly"

    [FirestoreProperty]
    public Timestamp SeriesEnd { get; set; } // Окончание серии повторений

    [FirestoreProperty]
    public bool IsActive { get; set; } = true;
}
