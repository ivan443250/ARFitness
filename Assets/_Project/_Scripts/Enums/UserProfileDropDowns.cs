using System;
using UnityEngine;

public class UserProfileDropDowns
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
}