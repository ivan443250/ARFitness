using System;
using SQLite;

[Table("routes")]
public class LocallRoutesModel
{
    [PrimaryKey]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(50)]
    public string OwnerId { get; set; } // ID тренера/организатора

    [MaxLength(50)]
    public string City { get; set; }

    [MaxLength(20)]
    public string Visibility { get; set; } // "private", "public", "group"

    public string Description { get; set; }
    public double DefaultRadius { get; set; } = 20.0; // метров
    public int Version { get; set; } = 1;
    public string Status { get; set; } = "draft"; // "draft", "published", "archived"
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Геометрические параметры
    [MaxLength(20)]
    public string RouteType { get; set; } // "circle", "line", "polygon", "manual"
    public double CenterLat { get; set; }
    public double CenterLng { get; set; }
    public double Radius { get; set; }
    public int NumPoints { get; set; }
    public double StepDistance { get; set; }
    public double Bearing { get; set; }
    public double ZigzagOffset { get; set; }
    public int NumVertices { get; set; }
    public double Rotation { get; set; }

    public double TotalDistance { get; set; } // метров
    public double EstimatedTime { get; set; } // минут
}
