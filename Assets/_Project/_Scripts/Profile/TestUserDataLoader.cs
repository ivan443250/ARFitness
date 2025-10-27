using UnityEngine;
using System.Collections.Generic;
using System;
using Firebase.Firestore;

namespace TrainingSceneProfile
{
    public class TestUserDataLoader : MonoBehaviour
    {
        [Header("References")]
        public UserProfileView profileView;

        [Header("Test Settings")]
        public bool autoLoadOnStart = true;
        public UserType testUserType = UserType.Basic;

        public enum UserType
        {
            Basic,
            Advanced,
            Trainer,
            Beginner
        }

        void Start()
        {
            if (autoLoadOnStart && profileView != null)
            {
                LoadTestUserData(testUserType);
            }
        }

        public void LoadTestUserData(UserType userType)
        {
            User testUser = CreateTestUser(userType);

            if (profileView != null)
            {
                profileView.DisplayUser(testUser);
                Debug.Log($"Тестовые данные пользователя ({userType}) загружены!");
            }
            else
            {
                Debug.LogError("UserProfileView не назначен в инспекторе!");
            }
        }

        private User CreateTestUser(UserType userType)
        {
            return userType switch
            {
                UserType.Basic => CreateBasicUser(),
                UserType.Advanced => CreateAdvancedUser(),
                UserType.Trainer => CreateTrainerUser(),
                UserType.Beginner => CreateBeginnerUser(),
                _ => CreateBasicUser()
            };
        }

        private User CreateBasicUser()
        {
            return new User
            {
                Email = "ivan.testov@example.com",
                DisplayName = "Иван Тестов",
                AvatarUrl = "https://example.com/avatars/ivan.jpg",
                Profile = new UserProfile
                {
                    Age = 25,
                    Gender = UserConstants.Gender.Male,
                    Level = UserConstants.Level.Intermediate,
                    Experience = UserConstants.Experience.RegularTraining,
                    Goals = new List<string>
                {
                    UserConstants.Goals.Endurance,
                    UserConstants.Goals.Strength
                },
                    ParentEmail = "parent.ivan@example.com"
                },
                Stats = new UserStats
                {
                    TotalPoints = 1250,
                    TotalXP = 5400,
                    CompletedRoutes = 12,
                    TotalDistance = 45.7,
                    BestPromptScore = 95.5,
                    StreakDays = 7
                },
                GameProgress = new UserGameProgress
                {
                    Level = 5,
                    Currency = 3200,
                    UnlockedSkins = new List<string> { "skin_red", "skin_blue", "skin_epic" },
                    Achievements = new List<string> { "first_steps", "marathon_runner", "streak_master" }
                },
                Settings = new UserSettings
                {
                    Notifications = true,
                    Privacy = UserConstants.Privacy.Friends,
                    Language = "ru"
                },
                CreatedAt = Timestamp.FromDateTime(DateTime.UtcNow.AddDays(-30)),
                LastLogin = Timestamp.FromDateTime(DateTime.UtcNow),
                Role = UserConstants.Role.Participant
            };
        }

        private User CreateAdvancedUser()
        {
            return new User
            {
                Email = "anna.advanced@example.com",
                DisplayName = "Анна Продвинутая",
                AvatarUrl = "https://example.com/avatars/anna.jpg",
                Profile = new UserProfile
                {
                    Age = 28,
                    Gender = UserConstants.Gender.Female,
                    Level = UserConstants.Level.Advanced,
                    Experience = UserConstants.Experience.Section,
                    Goals = new List<string>
                {
                    UserConstants.Goals.Strength,
                    UserConstants.Goals.Flexibility,
                    UserConstants.Goals.GeneralFitness
                },
                    ParentEmail = ""
                },
                Stats = new UserStats
                {
                    TotalPoints = 3500,
                    TotalXP = 12800,
                    CompletedRoutes = 28,
                    TotalDistance = 156.3,
                    BestPromptScore = 98.2,
                    StreakDays = 21
                },
                GameProgress = new UserGameProgress
                {
                    Level = 12,
                    Currency = 8750,
                    UnlockedSkins = new List<string> { "skin_red", "skin_blue", "skin_epic", "skin_legendary", "skin_gold" },
                    Achievements = new List<string> { "first_steps", "marathon_runner", "streak_master", "route_explorer", "fitness_guru" }
                },
                Settings = new UserSettings
                {
                    Notifications = true,
                    Privacy = UserConstants.Privacy.Public,
                    Language = "en"
                },
                CreatedAt = Timestamp.FromDateTime(DateTime.UtcNow.AddDays(-120)),
                LastLogin = Timestamp.FromDateTime(DateTime.UtcNow.AddHours(-2)),
                Role = UserConstants.Role.Participant
            };
        }

        private User CreateTrainerUser()
        {
            return new User
            {
                Email = "trainer.alex@example.com",
                DisplayName = "Алексей Тренер",
                AvatarUrl = "https://example.com/avatars/alex.jpg",
                Profile = new UserProfile
                {
                    Age = 32,
                    Gender = UserConstants.Gender.Male,
                    Level = UserConstants.Level.Athlete,
                    Experience = UserConstants.Experience.Section,
                    Goals = new List<string>
                {
                    UserConstants.Goals.Endurance,
                    UserConstants.Goals.GeneralFitness
                },
                    ParentEmail = ""
                },
                Stats = new UserStats
                {
                    TotalPoints = 8900,
                    TotalXP = 45000,
                    CompletedRoutes = 67,
                    TotalDistance = 489.2,
                    BestPromptScore = 99.8,
                    StreakDays = 45
                },
                GameProgress = new UserGameProgress
                {
                    Level = 25,
                    Currency = 21500,
                    UnlockedSkins = new List<string> { "skin_red", "skin_blue", "skin_epic", "skin_legendary", "skin_gold", "skin_trainer" },
                    Achievements = new List<string> { "first_steps", "marathon_runner", "streak_master", "route_explorer", "fitness_guru", "elite_athlete", "trainer_pro" }
                },
                Settings = new UserSettings
                {
                    Notifications = false,
                    Privacy = UserConstants.Privacy.Public,
                    Language = "ru"
                },
                CreatedAt = Timestamp.FromDateTime(DateTime.UtcNow.AddDays(-365)),
                LastLogin = Timestamp.FromDateTime(DateTime.UtcNow.AddHours(-1)),
                Role = UserConstants.Role.Trainer
            };
        }

        private User CreateBeginnerUser()
        {
            return new User
            {
                Email = "beginner.sasha@example.com",
                DisplayName = "Саша Новичок",
                AvatarUrl = "https://example.com/avatars/sasha.jpg",
                Profile = new UserProfile
                {
                    Age = 16,
                    Gender = UserConstants.Gender.Other,
                    Level = UserConstants.Level.Beginner,
                    Experience = UserConstants.Experience.PhysicalEducation,
                    Goals = new List<string>
                {
                    UserConstants.Goals.GeneralFitness
                },
                    ParentEmail = "parent.sasha@example.com"
                },
                Stats = new UserStats
                {
                    TotalPoints = 150,
                    TotalXP = 800,
                    CompletedRoutes = 2,
                    TotalDistance = 8.5,
                    BestPromptScore = 72.3,
                    StreakDays = 3
                },
                GameProgress = new UserGameProgress
                {
                    Level = 1,
                    Currency = 500,
                    UnlockedSkins = new List<string> { "skin_red" },
                    Achievements = new List<string> { "first_steps" }
                },
                Settings = new UserSettings
                {
                    Notifications = true,
                    Privacy = UserConstants.Privacy.Private,
                    Language = "ru"
                },
                CreatedAt = Timestamp.FromDateTime(DateTime.UtcNow.AddDays(-5)),
                LastLogin = Timestamp.FromDateTime(DateTime.UtcNow),
                Role = UserConstants.Role.Participant
            };
        }

        // Методы для кнопок UI
        public void LoadBasicUser() => LoadTestUserData(UserType.Basic);
        public void LoadAdvancedUser() => LoadTestUserData(UserType.Advanced);
        public void LoadTrainerUser() => LoadTestUserData(UserType.Trainer);
        public void LoadBeginnerUser() => LoadTestUserData(UserType.Beginner);

        // Метод для случайного пользователя
        public void LoadRandomUser()
        {
            Array values = Enum.GetValues(typeof(UserType));
            UserType randomType = (UserType)values.GetValue(UnityEngine.Random.Range(0, values.Length));
            LoadTestUserData(randomType);
        }
        void Update()
        {
            // Быстрое тестирование клавишами
            if (Input.GetKeyDown(KeyCode.Alpha1)) LoadBasicUser();
            if (Input.GetKeyDown(KeyCode.Alpha2)) LoadAdvancedUser();
            if (Input.GetKeyDown(KeyCode.Alpha3)) LoadTrainerUser();
            if (Input.GetKeyDown(KeyCode.Alpha4)) LoadBeginnerUser();
            if (Input.GetKeyDown(KeyCode.R)) LoadRandomUser();
        }
    }
}
