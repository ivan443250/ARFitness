using SQLite;
using System;

[Table("anomalies")]
public class LocalAnomalyModel
{
    [PrimaryKey]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Indexed]
    public string AttemptId { get; set; }

    [Indexed]
    public string UserId { get; set; }

    [MaxLength(50)]
    public string AnomalyType { get; set; } // "SPEED_ANOMALY", "TELEPORT", "REPEAT_SCAN", "GPS_JUMP"

    public string Description { get; set; }
    public double Speed { get; set; } // м/с
    public double Distance { get; set; } // метров
    public double TimeDifference { get; set; } // секунд

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public bool Reviewed { get; set; } = false;
    public string ReviewDecision { get; set; } // "confirmed", "dismissed", "pending"
    public string ReviewerId { get; set; } // ID судьи
}
