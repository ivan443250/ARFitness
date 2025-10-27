using SQLite;
using System;

[Table("app_settings")]
public class LocalAppSettingsModel
{
    [PrimaryKey]
    public string UserId { get; set; }

    public string Language { get; set; } = "ru-RU";
    public string Region { get; set; }
    public bool NotificationsEnabled { get; set; } = true;
    public bool VoiceGuidance { get; set; } = true;
    public int TextSize { get; set; } = 20; // pt
    public bool HighContrast { get; set; } = false;
    public double Volume { get; set; } = 0.8;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
