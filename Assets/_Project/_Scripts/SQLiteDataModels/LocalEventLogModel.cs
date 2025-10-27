using SQLite;
using System;

[Table("event_logs")]
public class LocalEventLogModel
{
    [PrimaryKey]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Indexed]
    public string AttemptId { get; set; }

    [Indexed]
    public string UserId { get; set; }

    [MaxLength(50)]
    public string EventType { get; set; } // "checkpointEnter", "qrScan", "exercise", "prompt"

    public string CheckpointId { get; set; }
    public string RouteId { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Data { get; set; } // JSON с деталями события
    public bool OfflineMode { get; set; } = false;
    public bool Synced { get; set; } = false;

    // GPS данные
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public double? Accuracy { get; set; }
    public double? Hdop { get; set; } // Horizontal Dilution of Precision
}
