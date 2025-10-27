using SQLite;
using System;

[Table("achievements")]
public class LocalAchievementModel
{
    [PrimaryKey]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Indexed]
    public string UserId { get; set; }

    [MaxLength(100)]
    public string AchievementId { get; set; }

    [MaxLength(100)]
    public string Name { get; set; }

    public string Description { get; set; }
    public string IconUrl { get; set; }
    public int PointsReward { get; set; }
    public string Category { get; set; } // "fitness", "creative", "social", "exploration"

    public DateTime UnlockedAt { get; set; } = DateTime.UtcNow;
    public int Progress { get; set; } = 0;
    public int Target { get; set; } = 1;
    public bool IsUnlocked { get; set; } = false;
}
