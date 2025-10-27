using SQLite;
using System;

[Table("map_cache")]
public class LocalMapCacheModel
{
    [PrimaryKey]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Indexed]
    public string RouteId { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Radius { get; set; } // метров
    public int ZoomLevel { get; set; }
    public string TileData { get; set; } // Бинарные данные тайлов
    public DateTime CachedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
}
