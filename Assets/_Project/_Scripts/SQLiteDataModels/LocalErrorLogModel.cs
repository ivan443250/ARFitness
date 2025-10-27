using SQLite;
using System;

public class LocalErrorLogModel
{
    [PrimaryKey]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Indexed]
    public string UserId { get; set; }

    [MaxLength(50)]
    public string ErrorType { get; set; } // "app_crash", "sync_error", "gps_error"

    public string Message { get; set; }
    public string StackTrace { get; set; }
    public string DeviceInfo { get; set; } // JSON � ����������� �� ����������
    public string SessionData { get; set; } // JSON � ������� ������

    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
    public bool Resolved { get; set; } = false;
}
