using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace TrainingSceneProfile
{
    public class UserProfileView : MonoBehaviour
    {
        [Header("Basic Info")]
        public TMP_Text textDisplayName;
        public Image avatarImage;

        [Header("Profile")]
        public TMP_Text underText;
        public TMP_Text textExperience;
        public TMP_Text textGoals;
        public TMP_Text textParentEmail;

        [Header("Stats")]
        public TMP_Text textTotalPoints;
        public TMP_Text textTotalXP;
        public TMP_Text textCompletedRoutes;
        public TMP_Text textTotalDistance;
        public TMP_Text textBestPromptScore;
        public TMP_Text textStreakDays;

        [Header("Game Progress")]
        public TMP_Text textGameLevel;
        public TMP_Text textCurrency;
        public TMP_Text textUnlockedSkins;
        public TMP_Text textAchievements;

        [Header("Settings")]
        public TMP_Text textNotifications;
        public TMP_Text textPrivacy;
        public TMP_Text textLanguage;

        public void DisplayUser(User user)
        {
            try
            {
                SetTextSafe(textDisplayName, user.DisplayName);

                if (IsProfileValid(user.Profile))
                {
                    SetTextSafe(underText, $"{GetLocalizedGender(user.Profile.Gender)}.,{user.Profile.Age.ToString()} | {GetLocalizedGender(user.Profile.Level)}");
                    SetTextSafe(textExperience, GetLocalizedExperience(user.Profile.Experience));
                    SetTextSafe(textParentEmail, user.Profile.ParentEmail);
                    SetTextSafe(textGoals, user.Profile.Goals[0]);
                }
                else
                {
                    SetProfileDefaults();
                }

                if (IsStatsValid(user.Stats))
                {
                    SetTextSafe(textTotalPoints, user.Stats.TotalPoints.ToString("N0"));
                    SetTextSafe(textTotalXP, user.Stats.TotalXP.ToString("N0"));
                    SetTextSafe(textCompletedRoutes, user.Stats.CompletedRoutes.ToString());
                    SetTextSafe(textTotalDistance, user.Stats.TotalDistance.ToString("F1") + " км");
                    SetTextSafe(textBestPromptScore, user.Stats.BestPromptScore.ToString("F1") + "%");
                    SetTextSafe(textStreakDays, user.Stats.StreakDays.ToString() + " дней");
                }
                else
                {
                    SetStatsDefaults();
                }

                if (IsGameProgressValid(user.GameProgress))
                {
                    SetTextSafe(textGameLevel, "Уровень " + user.GameProgress.Level.ToString());
                    SetTextSafe(textCurrency, user.GameProgress.Currency.ToString("N0") + " монет");
                    SetTextSafe(textUnlockedSkins, user.GameProgress.UnlockedSkins != null ?
                        string.Join(", ", user.GameProgress.UnlockedSkins) : "Нет скинов");
                    SetTextSafe(textAchievements, user.GameProgress.Achievements != null ?
                        string.Join(", ", user.GameProgress.Achievements) : "Нет достижений");
                }
                else
                {
                    SetGameProgressDefaults();
                }

                if (IsSettingsValid(user.Settings))
                {
                    SetTextSafe(textNotifications, user.Settings.Notifications ? "Включены" : "Выключены");
                    SetTextSafe(textPrivacy, GetLocalizedPrivacy(user.Settings.Privacy));
                    SetTextSafe(textLanguage, GetLocalizedLanguage(user.Settings.Language));
                }
                else
                {
                    SetSettingsDefaults();
                }

                Debug.Log("Данные пользователя успешно отображены!");
            }
            catch (Exception e)
            {
                Debug.LogError($"Ошибка при отображении пользователя: {e.Message}");
            }
        }

        private bool IsProfileValid(UserProfile profile)
        {
            return !string.IsNullOrEmpty(profile.Gender) ||
                   !string.IsNullOrEmpty(profile.Level) ||
                   !string.IsNullOrEmpty(profile.Experience) ||
                   profile.Age > 0;
        }

        private bool IsStatsValid(UserStats stats)
        {
            return stats.TotalPoints > 0 ||
                   stats.TotalXP > 0 ||
                   stats.CompletedRoutes > 0 ||
                   stats.TotalDistance > 0;
        }

        private bool IsGameProgressValid(UserGameProgress gameProgress)
        {
            return gameProgress.Level > 0 ||
                   gameProgress.Currency > 0 ||
                   (gameProgress.UnlockedSkins != null && gameProgress.UnlockedSkins.Count > 0) ||
                   (gameProgress.Achievements != null && gameProgress.Achievements.Count > 0);
        }

        private bool IsSettingsValid(UserSettings settings)
        {
            return !string.IsNullOrEmpty(settings.Privacy) ||
                   !string.IsNullOrEmpty(settings.Language);
        }

        private void SetProfileDefaults()
        {
            SetTextSafe(underText, "Не указан");
            SetTextSafe(textExperience, "Не указан");
            SetTextSafe(textGoals, "Нет целей");
            SetTextSafe(textParentEmail, "Не указан");
        }

        private void SetStatsDefaults()
        {
            SetTextSafe(textTotalPoints, "0");
            SetTextSafe(textTotalXP, "0");
            SetTextSafe(textCompletedRoutes, "0");
            SetTextSafe(textTotalDistance, "0 км");
            SetTextSafe(textBestPromptScore, "0%");
            SetTextSafe(textStreakDays, "0 дней");
        }

        private void SetGameProgressDefaults()
        {
            SetTextSafe(textGameLevel, "Уровень 1");
            SetTextSafe(textCurrency, "0 монет");
            SetTextSafe(textUnlockedSkins, "Нет скинов");
            SetTextSafe(textAchievements, "Нет достижений");
        }

        private void SetSettingsDefaults()
        {
            SetTextSafe(textNotifications, "Включены");
            SetTextSafe(textPrivacy, "Публичный");
            SetTextSafe(textLanguage, "Русский");
        }

        private void SetTextSafe(TMP_Text textElement, string value)
        {
            if (textElement != null)
                textElement.text = value ?? "N/A";
        }

        private string GetLocalizedGender(string gender)
        {
            return gender switch
            {
                UserConstants.Gender.Male => "Мужской",
                UserConstants.Gender.Female => "Женский",
                UserConstants.Gender.Other => "Другой",
                UserConstants.Gender.NotSpecified => "Не указан",
                _ => gender
            };
        }

        private string GetLocalizedLevel(string level)
        {
            return level switch
            {
                UserConstants.Level.Beginner => "Начинающий",
                UserConstants.Level.Intermediate => "Средний",
                UserConstants.Level.Advanced => "Продвинутый",
                UserConstants.Level.Athlete => "Атлет",
                _ => level
            };
        }

        private string GetLocalizedExperience(string experience)
        {
            return experience switch
            {
                UserConstants.Experience.Section => "Секция",
                UserConstants.Experience.RegularTraining => "Регулярные тренировки",
                UserConstants.Experience.PhysicalEducation => "Физкультура",
                UserConstants.Experience.None => "Нет опыта",
                _ => experience
            };
        }

        private string GetLocalizedPrivacy(string privacy)
        {
            return privacy switch
            {
                UserConstants.Privacy.Public => "Публичный",
                UserConstants.Privacy.Friends => "Только друзья",
                UserConstants.Privacy.Private => "Приватный",
                _ => privacy
            };
        }

        private string GetLocalizedLanguage(string language)
        {
            return language switch
            {
                "en" => "Английский",
                "ru" => "Русский",
                _ => language
            };
        }
    }
}