using SQLite;
using System;

[Table("events")]
public class LocalEventModel
{
    [PrimaryKey]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [MaxLength(100)]
    public string Name { get; set; }

    [Indexed]
    public string RouteId { get; set; }

    [MaxLength(50)]
    public string OrganizerId { get; set; }

    [MaxLength(20)]
    public string EventType { get; set; } // "training", "competition", "challenge"

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime StartWindow { get; set; } // Окно старта для массовых событий

    [MaxLength(20)]
    public string Status { get; set; } = "scheduled"; // "scheduled", "active", "completed", "cancelled"

    [MaxLength(20)]
    public string Visibility { get; set; } = "private";

    public string GroupIds { get; set; } // JSON массив ID групп
    public int MaxParticipants { get; set; }
    public int CurrentParticipants { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
