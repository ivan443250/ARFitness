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
                    SetTextSafe(textTotalDistance, user.Stats.TotalDistance.ToString("F1") + " ��");
                    SetTextSafe(textBestPromptScore, user.Stats.BestPromptScore.ToString("F1") + "%");
                    SetTextSafe(textStreakDays, user.Stats.StreakDays.ToString() + " ����");
                }
                else
                {
                    SetStatsDefaults();
                }

                if (IsGameProgressValid(user.GameProgress))
                {
                    SetTextSafe(textGameLevel, "������� " + user.GameProgress.Level.ToString());
                    SetTextSafe(textCurrency, user.GameProgress.Currency.ToString("N0") + " �����");
                    SetTextSafe(textUnlockedSkins, user.GameProgress.UnlockedSkins != null ?
                        string.Join(", ", user.GameProgress.UnlockedSkins) : "��� ������");
                    SetTextSafe(textAchievements, user.GameProgress.Achievements != null ?
                        string.Join(", ", user.GameProgress.Achievements) : "��� ����������");
                }
                else
                {
                    SetGameProgressDefaults();
                }

                if (IsSettingsValid(user.Settings))
                {
                    SetTextSafe(textNotifications, user.Settings.Notifications ? "��������" : "���������");
                    SetTextSafe(textPrivacy, GetLocalizedPrivacy(user.Settings.Privacy));
                    SetTextSafe(textLanguage, GetLocalizedLanguage(user.Settings.Language));
                }
                else
                {
                    SetSettingsDefaults();
                }

                Debug.Log("������ ������������ ������� ����������!");
            }
            catch (Exception e)
            {
                Debug.LogError($"������ ��� ����������� ������������: {e.Message}");
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
            SetTextSafe(underText, "�� ������");
            SetTextSafe(textExperience, "�� ������");
            SetTextSafe(textGoals, "��� �����");
            SetTextSafe(textParentEmail, "�� ������");
        }

        private void SetStatsDefaults()
        {
            SetTextSafe(textTotalPoints, "0");
            SetTextSafe(textTotalXP, "0");
            SetTextSafe(textCompletedRoutes, "0");
            SetTextSafe(textTotalDistance, "0 ��");
            SetTextSafe(textBestPromptScore, "0%");
            SetTextSafe(textStreakDays, "0 ����");
        }

        private void SetGameProgressDefaults()
        {
            SetTextSafe(textGameLevel, "������� 1");
            SetTextSafe(textCurrency, "0 �����");
            SetTextSafe(textUnlockedSkins, "��� ������");
            SetTextSafe(textAchievements, "��� ����������");
        }

        private void SetSettingsDefaults()
        {
            SetTextSafe(textNotifications, "��������");
            SetTextSafe(textPrivacy, "���������");
            SetTextSafe(textLanguage, "�������");
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
                UserConstants.Gender.Male => "�������",
                UserConstants.Gender.Female => "�������",
                UserConstants.Gender.Other => "������",
                UserConstants.Gender.NotSpecified => "�� ������",
                _ => gender
            };
        }

        private string GetLocalizedLevel(string level)
        {
            return level switch
            {
                UserConstants.Level.Beginner => "����������",
                UserConstants.Level.Intermediate => "�������",
                UserConstants.Level.Advanced => "�����������",
                UserConstants.Level.Athlete => "�����",
                _ => level
            };
        }

        private string GetLocalizedExperience(string experience)
        {
            return experience switch
            {
                UserConstants.Experience.Section => "������",
                UserConstants.Experience.RegularTraining => "���������� ����������",
                UserConstants.Experience.PhysicalEducation => "�����������",
                UserConstants.Experience.None => "��� �����",
                _ => experience
            };
        }

        private string GetLocalizedPrivacy(string privacy)
        {
            return privacy switch
            {
                UserConstants.Privacy.Public => "���������",
                UserConstants.Privacy.Friends => "������ ������",
                UserConstants.Privacy.Private => "���������",
                _ => privacy
            };
        }

        private string GetLocalizedLanguage(string language)
        {
            return language switch
            {
                "en" => "����������",
                "ru" => "�������",
                _ => language
            };
        }
    }
}