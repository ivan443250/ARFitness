using SQLite;
using System;

[Table("checkpoints")]
public class LocalCheckpointModel
{
    [PrimaryKey]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Indexed]
    public string RouteId { get; set; }

    public int OrderIndex { get; set; }

    [MaxLength(100)]
    public string Name { get; set; }

    public string Hint { get; set; } // Подсказка для участника

    // Координаты
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Radius { get; set; } = 20.0; // метров

    // Тип подтверждения
    [MaxLength(20)]
    public string ValidationType { get; set; } // "gps_qr", "gps_only", "qr_only", "ar_marker"

    // Физическое упражнение
    [MaxLength(50)]
    public string ExerciseType { get; set; } // "squats", "jumps", "pushups", "plank"
    public int TargetRepetitions { get; set; }
    public double TimeLimit { get; set; } // секунд

    // Промт-задание
    public string PromptKeyword { get; set; }
    public string PromptRulesetId { get; set; }

    // QR-код
    public string QrControlHash { get; set; }
    public DateTime QrExpiration { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
