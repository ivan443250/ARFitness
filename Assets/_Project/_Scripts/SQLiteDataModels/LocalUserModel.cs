using SQLite;
using System;

[Table("users")]
public class LocalUserModel
{
    [PrimaryKey]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [MaxLength(100)]
    public string Email { get; set; }

    [MaxLength(50)]
    public string Username { get; set; }

    [MaxLength(20)]
    public string AuthProvider { get; set; } // "email", "google", "facebook", "web3"

    [MaxLength(100)]
    public string WalletAddress { get; set; } // ��� Web3 �������������

    public int Age { get; set; }

    [MaxLength(10)]
    public string Gender { get; set; } // "male", "female", "other"

    [MaxLength(20)]
    public string FitnessLevel { get; set; } // "beginner", "intermediate", "advanced"

    public string AvatarUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    // ��� ����� <14 ���
    [MaxLength(100)]
    public string ParentEmail { get; set; }
    public bool ParentConsent { get; set; }
    public bool Web3ParentConsent { get; set; }
}