using Firebase.Firestore;
using System;
using UnityEngine;

public class CloudChallengeModel
{
    [FirestoreDocumentId]
    public string Id { get; set; }

    [FirestoreProperty]
    public string FromUserId { get; set; }

    [FirestoreProperty]
    public string ToUserId { get; set; }

    [FirestoreProperty]
    public string RouteId { get; set; }

    // 🔥 СРОКИ ЧЕЛЛЕНДЖА
    [FirestoreProperty]
    public Timestamp ChallengeCreated { get; set; } = Timestamp.FromDateTime(DateTime.UtcNow);

    [FirestoreProperty]
    public Timestamp ChallengeExpires { get; set; } // Дедлайн принятия

    [FirestoreProperty]
    public Timestamp CompletionDeadline { get; set; } // Дедлайн выполнения

    [FirestoreProperty]
    public TimeSpan TimeLimit { get; set; } // Лимит времени на прохождение

    [FirestoreProperty]
    public string Status { get; set; } = "pending"; // "pending", "accepted", "completed", "expired"
}
