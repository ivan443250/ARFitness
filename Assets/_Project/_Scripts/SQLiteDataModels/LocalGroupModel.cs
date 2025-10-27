using SQLite;
using System;

[Table("groups")]
public class LocalGroupModel
{
    [PrimaryKey]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(50)]
    public string OwnerId { get; set; }

    [MaxLength(20)]
    public string GroupType { get; set; } // "school", "team", "friends", "public"

    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public int MemberCount { get; set; } = 0;
    public int MaxMembers { get; set; } = 50;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
}
