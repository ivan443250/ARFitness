using SQLite;
using System;

[Table("attempts")]
public class LocalAttemptModel
{
    [PrimaryKey]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Indexed]
    public string UserId { get; set; }

    [Indexed]
    public string RouteId { get; set; }

    [Indexed]
    public string EventId { get; set; }

    [MaxLength(20)]
    public string AttemptType { get; set; } // "training", "competition"

    public DateTime StartTime { get; set; } = DateTime.UtcNow;
    public DateTime? EndTime { get; set; }

    [MaxLength(20)]
    public string Status { get; set; } = "active"; // "active", "completed", "cancelled", "paused"

    public double TotalDistance { get; set; }
    public double TotalTime { get; set; } // секунд
    public int TotalPoints { get; set; }
    public int CheckpointsCompleted { get; set; }
    public int TotalCheckpoints { get; set; }

    // Для восстановления после сбоев
    public int CurrentCheckpointIndex { get; set; } = 0;
    public string SessionData { get; set; } // JSON с состоянием сессии
    public DateTime LastSaveTime { get; set; } = DateTime.UtcNow;
    public bool CleanShutdown { get; set; } = false;
}
