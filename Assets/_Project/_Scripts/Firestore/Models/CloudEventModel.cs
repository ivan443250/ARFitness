using Firebase.Firestore;
using System.Collections.Generic;
using System;

[FirestoreData]
public class CloudEventModel
{
    [FirestoreDocumentId]
    public string Id { get; set; }

    [FirestoreProperty]
    public string Name { get; set; }

    [FirestoreProperty]
    public string OrganizerId { get; set; } // ID тренера

    [FirestoreProperty]
    public string EventType { get; set; } // "training", "competition", "challenge"

    // 🔥 КРИТИЧЕСКИЕ ПОЛЯ ДАТЫ И ВРЕМЕНИ
    [FirestoreProperty]
    public Timestamp ScheduledStart { get; set; } // Планируемое время начала

    [FirestoreProperty]
    public Timestamp ScheduledEnd { get; set; } // Планируемое время окончания

    [FirestoreProperty]
    public Timestamp RegistrationStart { get; set; } // Начало регистрации

    [FirestoreProperty]
    public Timestamp RegistrationEnd { get; set; } // Конец регистрации

    [FirestoreProperty]
    public TimeSpan StartWindow { get; set; } // Окно старта (например, 30 минут)

    [FirestoreProperty]
    public string TimeZone { get; set; } = "UTC+3"; // Часовой пояс

    // Для массовых стартов
    [FirestoreProperty]
    public bool IsMassStart { get; set; } = false;

    [FirestoreProperty]
    public Timestamp MassStartTime { get; set; } // Точное время массового старта

    // Статус относительно времени
    [FirestoreProperty]
    public string TimeStatus { get; set; } // "scheduled", "registration_open", "live", "completed", "cancelled"

    [FirestoreProperty]
    public string RouteId { get; set; }

    [FirestoreProperty]
    public string Visibility { get; set; } = "private";

    [FirestoreProperty]
    public List<string> GroupIds { get; set; } = new List<string>();

    [FirestoreProperty]
    public Timestamp CreatedAt { get; set; } = Timestamp.FromDateTime(DateTime.UtcNow);

    public CloudEventModel(
        string name,
        string organizerId,
        string eventType,
        DateTime scheduledStart,
        DateTime scheduledEnd,
        string routeId,
        DateTime? registrationStart = null,
        DateTime? registrationEnd = null,
        TimeSpan? startWindow = null,
        string timeZone = "UTC+3",
        bool isMassStart = false,
        DateTime? massStartTime = null,
        string visibility = "private",
        List<string> groupIds = null)
    {
        Name = name;
        OrganizerId = organizerId;
        EventType = eventType;
        ScheduledStart = Timestamp.FromDateTime(scheduledStart);
        ScheduledEnd = Timestamp.FromDateTime(scheduledEnd);
        RouteId = routeId;
        TimeZone = timeZone;
        IsMassStart = isMassStart;
        Visibility = visibility;
        GroupIds = groupIds ?? new List<string>();

        // Умные значения по умолчанию
        RegistrationStart = registrationStart.HasValue
            ? Timestamp.FromDateTime(registrationStart.Value)
            : Timestamp.FromDateTime(DateTime.UtcNow);

        RegistrationEnd = registrationEnd.HasValue
            ? Timestamp.FromDateTime(registrationEnd.Value)
            : Timestamp.FromDateTime(scheduledStart.AddHours(-1)); // Заканчивается за час до старта

        StartWindow = startWindow ?? TimeSpan.FromMinutes(30);

        MassStartTime = isMassStart && massStartTime.HasValue
            ? Timestamp.FromDateTime(massStartTime.Value)
            : ScheduledStart;

        // Автоматическое определение статуса
        TimeStatus = CalculateTimeStatus(scheduledStart, scheduledEnd);
        CreatedAt = Timestamp.FromDateTime(DateTime.UtcNow);
    }

    // Пустой конструктор для Firestore
    public CloudEventModel() { }

    private string CalculateTimeStatus(DateTime start, DateTime end)
    {
        var now = DateTime.UtcNow;

        if (now < start) return "scheduled";
        if (now >= start && now <= end) return "live";
        if (now > end) return "completed";

        return "scheduled";
    }
}