using Firebase.Firestore;
using System.Collections.Generic;

[FirestoreData]
public class CloudUserModel
{
    public CloudUserModel() { }

    public CloudUserModel(string uid, string email)
    {
        BasicInfo = new BasicInfo
        {
            Uid = uid,
            Email = email,
            CreatedAt = Timestamp.GetCurrentTimestamp(),
            UpdatedAt = Timestamp.GetCurrentTimestamp()
        };
        Economy = new Economy();
        Profile = new Profile();
    }

    [FirestoreProperty]
    public BasicInfo BasicInfo { get; set; }

    [FirestoreProperty]
    public Economy Economy { get; set; }

    [FirestoreProperty]
    public Profile Profile { get; set; }
}

[FirestoreData]
public class BasicInfo
{
    public BasicInfo() { }

    public BasicInfo(string uid, string displayName, string email)
    {
        Uid = uid;
        DisplayName = displayName;
        Email = email;
        CreatedAt = Timestamp.GetCurrentTimestamp();
        UpdatedAt = Timestamp.GetCurrentTimestamp();
    }

    [FirestoreProperty]
    public string AvatarPath { get; set; } = "";

    [FirestoreProperty]
    public Timestamp CreatedAt { get; set; }

    [FirestoreProperty]
    public string DisplayName { get; set; }

    [FirestoreProperty]
    public string Email { get; set; }

    [FirestoreProperty]
    public string RealName { get; set; } = "";

    [FirestoreProperty]
    public string Uid { get; set; }

    [FirestoreProperty]
    public Timestamp UpdatedAt { get; set; }
}

[FirestoreData]
public class Economy
{
    public Economy()
    {
        NftInventory = new List<string>();
        UnlockedSkins = new List<string>();
    }

    [FirestoreProperty]
    public string Achievements { get; set; } = "";

    [FirestoreProperty]
    public int FitCoins { get; set; }

    [FirestoreProperty]
    public List<string> NftInventory { get; set; }

    [FirestoreProperty]
    public int PremiumCurrency { get; set; }

    [FirestoreProperty]
    public List<string> UnlockedSkins { get; set; }
}

[FirestoreData]
public class Profile
{
    public Profile()
    {
        Auth = new Auth();
    }

    public Profile(int age, string fitnessLevel, string gender)
    {
        Age = age;
        FitnessLevel = fitnessLevel;
        Gender = gender;
        Auth = new Auth();
    }

    [FirestoreProperty]
    public int Age { get; set; }

    [FirestoreProperty]
    public Timestamp BirthDate { get; set; }

    [FirestoreProperty]
    public string FitnessLevel { get; set; }

    [FirestoreProperty]
    public string Gender { get; set; }

    [FirestoreProperty]
    public Auth Auth { get; set; }
}

[FirestoreData]
public class Auth
{
    public Auth()
    {
        Roles = new List<string> { "user" };
    }

    public Auth(string authMethod, bool isGuest = false, bool parentConsent = true)
    {
        AuthMethod = authMethod;
        IsGuest = isGuest;
        ParentConsent = parentConsent;
        Roles = new List<string> { "user" };
    }

    [FirestoreProperty]
    public string AuthMethod { get; set; } = "email";

    [FirestoreProperty]
    public bool IsGuest { get; set; }

    [FirestoreProperty]
    public bool ParentConsent { get; set; }

    [FirestoreProperty]
    public string ParentEmail { get; set; }

    [FirestoreProperty]
    public string Web3Wallet { get; set; } = "";

    [FirestoreProperty]
    public List<string> Roles { get; set; }
}