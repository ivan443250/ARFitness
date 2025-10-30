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
        { "Мужской" , Sex.Male },
        { "Женский" , Sex.Female },
        { "Скрыть" , Sex.Hidden },
        { "Не указывать", Sex.None }
    };

    public static Dictionary<string, StartLevel> ProfileStartLevelOptions = new Dictionary<string, UserProfileConstants.StartLevel>
    {
        { "Новичок" , StartLevel.Beginner },
        { "Базовый" , StartLevel.Basic },
        { "Средний" , StartLevel.Middle },
        { "Спортсмен", StartLevel.Athlete }
    };

    public static Dictionary<string, SportExperience> ProfileSportExperienceOptions = new Dictionary<string, SportExperience>
    {
        { "Секции" , SportExperience.Sections },
        { "Регулярные тренировки" , SportExperience.RegularTrainings },
        { "Просто физкультура" , SportExperience.PE },
    };

    public static Dictionary<string, Goal> ProfileGoalOptions = new Dictionary<string, Goal>
    {
        { "Сбросить вес" , Goal.LossWeight },
        { "Повысить выносливость" , Goal.IncreeseEndurance },
        { "Другое" , Goal.Other },
    };
}