using SQLite;
using System;

[Table("qr_codes")]
public class LocalQrCodesModel
{
    [PrimaryKey]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Indexed]
    public string CheckpointId { get; set; }

    [Indexed]
    public string RouteId { get; set; }

    public string QrData { get; set; } // {checkpointId, routeId, checksum, exp}
    public string HmacSignature { get; set; }
    public string ImagePath { get; set; } // Локальный путь к изображению QR
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
    public bool IsUsed { get; set; } = false;
}
