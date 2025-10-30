using System.Collections.Generic;

public static class UserProfileConstants
{
    public enum Sex
    {
        Male,
        Female,
        Hidden,
        None,
    }

    public enum StartLevel
    {
        Beginner,
        Basic,
        Middle,
        Athlete,
    }

    public enum SportExperience
    {
        Sections,
        RegularTrainings,
        PE,
    }

    public enum Goal
    {
        LossWeight,
        IncreeseEndurance,
        Other,
    }

    public static Dictionary<string, Sex> ProfileSexOptions = new Dictionary<string, Sex>
    {
        { "�������" , Sex.Male },
        { "�������" , Sex.Female },
        { "������" , Sex.Hidden },
        { "�� ���������", Sex.None }
    };

    public static Dictionary<string, StartLevel> ProfileStartLevelOptions = new Dictionary<string, UserProfileConstants.StartLevel>
    {
        { "�������" , StartLevel.Beginner },
        { "�������" , StartLevel.Basic },
        { "�������" , StartLevel.Middle },
        { "���������", StartLevel.Athlete }
    };

    public static Dictionary<string, SportExperience> ProfileSportExperienceOptions = new Dictionary<string, SportExperience>
    {
        { "������" , SportExperience.Sections },
        { "���������� ����������" , SportExperience.RegularTrainings },
        { "������ �����������" , SportExperience.PE },
    };

    public static Dictionary<string, Goal> ProfileGoalOptions = new Dictionary<string, Goal>
    {
        { "�������� ���" , Goal.LossWeight },
        { "�������� ������������" , Goal.IncreeseEndurance },
        { "������" , Goal.Other },
    };
}