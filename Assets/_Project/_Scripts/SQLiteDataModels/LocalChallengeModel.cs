using SQLite;
using System;

[Table("challenges")]
public class LocalChallengeModel
{
    [PrimaryKey]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Indexed]
    public string FromUserId { get; set; }

    [Indexed]
    public string ToUserId { get; set; }

    [MaxLength(100)]
    public string RouteId { get; set; }

    [MaxLength(50)]
    public string ChallengeType { get; set; } // "speed", "points", "completion"

    public string Message { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }

    [MaxLength(20)]
    public string Status { get; set; } = "pending"; // "pending", "accepted", "declined", "completed"

    public string Result { get; set; } // JSON с результатами
    public string WinnerId { get; set; }
}
