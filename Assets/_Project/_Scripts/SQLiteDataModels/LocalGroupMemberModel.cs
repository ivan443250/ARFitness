using SQLite;
using System;

[Table("group_members")]
public class LocalGroupMemberModel
{
    [PrimaryKey]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Indexed]
    public string GroupId { get; set; }

    [Indexed]
    public string UserId { get; set; }

    [MaxLength(20)]
    public string Role { get; set; } = "member"; // "member", "assistant", "admin"

    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
}
