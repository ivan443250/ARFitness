using System;
using SQLite;

[Table("guest_users")]
public class LocalGuestUserModel
{
    [PrimaryKey]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string DeviceId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastActivity { get; set; } = DateTime.UtcNow;
    public string TemporaryProgress { get; set; } // JSON с прогрессом
}