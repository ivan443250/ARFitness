using SQLite;
using System;

[Table("inventory")]
public class LocalInventoryItemModel
{
    [PrimaryKey]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Indexed]
    public string UserId { get; set; }

    [MaxLength(100)]
    public string ItemId { get; set; } // ID из NFT контракта

    [MaxLength(50)]
    public string ItemType { get; set; } // "skin", "avatar", "boost", "badge"

    [MaxLength(100)]
    public string Name { get; set; }

    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Metadata { get; set; } // JSON с дополнительными данными

    public bool IsEquipped { get; set; } = false;
    public bool IsTradable { get; set; } = false;
    public DateTime AcquiredAt { get; set; } = DateTime.UtcNow;
    public string TransactionHash { get; set; } // Для Web3 предметов
}
