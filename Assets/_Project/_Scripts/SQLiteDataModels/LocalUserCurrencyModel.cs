using SQLite;
using System;

[Table("user_currency")]
public class LocalUserCurrencyModel
{
    [PrimaryKey]
    public string UserId { get; set; }

    public int FitCoins { get; set; } = 0;
    public int Experience { get; set; } = 0;
    public int Level { get; set; } = 1;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
