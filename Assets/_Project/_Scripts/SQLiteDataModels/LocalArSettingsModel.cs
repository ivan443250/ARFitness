using SQLite;
using System;

[Table("ar_settings")]
public class LocalArSettingsModel
{
    [PrimaryKey]
    public string UserId { get; set; }

    public bool UseRokidAir { get; set; } = false;
    public string ConnectionType { get; set; } // "usb_c", "bluetooth"
    public string DeviceId { get; set; }
    public DateTime LastConnected { get; set; }
    public double UsageTime { get; set; } // ����� �� ������
    public bool SafetyAcknowledged { get; set; } = false;

    // ����������
    public string CalibrationData { get; set; } // JSON � ������� ����������
    public DateTime CalibratedAt { get; set; }
}
