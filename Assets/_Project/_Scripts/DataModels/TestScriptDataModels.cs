using System;
using System.Collections.Generic;

namespace KFS.Core.Models
{
    #region Value Objects & Enums

    [Serializable]
    public struct GeoPoint
    {
        public double Latitude;
        public double Longitude;
    }

    [Serializable]
    public struct TimeWindow
    {
        public DateTime Start;   // serverTime
        public DateTime End;     // inclusive/exclusive по вашему правилу
    }

    public enum RoleType { Participant, Trainer, Judge, Assistant }
    public enum Visibility { Private, Public }
    public enum ActivityType { Training, Competition, ScheduledChallenge }
    public enum EventStatus { Draft, Published, Active, Closed, Archived }
    public enum AttemptState { NotStarted, Active, Completed, Disqualified, UnderReview }
    public enum CheckpointProgressState { OutOfRadius, InRadius, QrScanned, ExerciseDone }
    public enum ConfirmationMode { GpsAndQr, GpsOnly, QrOnly, ArMarker }
    public enum ExerciseType { Unknown, Squat, Jump, PushUp, Custom }
    public enum ExerciseVerification { Accelerometer, Camera }
    public enum LogAction { EnterRadius, Scan, Exercise, Prompt, HdopAdjusted, RadiusAdjusted, Start, Finish, Sync }
    public enum AnomalyFlagType { SpeedAnomaly, Teleport, RepeatScan, OutOfRadius, MissingScan }

    #endregion

    #region Access / Users / Groups

    [Serializable]
    public class UserProfile
    {
        public string Id { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public string AvatarUrl { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; } // m/f/other
        public string FitnessLevel { get; set; } // beginner/intermediate/advanced
        public List<RoleType> Roles { get; set; } = new();
        public List<string> GroupIds { get; set; } = new();
        public bool ConsentAccepted { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    [Serializable]
    public class Group
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string OwnerTrainerId { get; set; } = default!;
        public List<string> MemberIds { get; set; } = new();
        public string LevelTag { get; set; } // для AI-сегментации
        public DateTime CreatedAt { get; set; }
    }

    #endregion

    #region Routes / Checkpoints / Rules

    [Serializable]
    public class Route
    {
        public string Id { get; set; } = default!;
        public string OwnerId { get; set; } = default!;
        public string City { get; set; }
        public string Region { get; set; }
        public Visibility Visibility { get; set; } = Visibility.Private;
        public double DefaultRadiusMeters { get; set; } = 20;
        public int Version { get; set; } = 1; // новая версия при редактировании
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = "draft"; // draft/published/archived
        public List<Checkpoint> Checkpoints { get; set; } = new();
        public List<string> Tags { get; set; } = new();
    }

    [Serializable]
    public class Checkpoint
    {
        public string Id { get; set; } = default!;
        public int Index { get; set; }
        public string Name { get; set; }
        public string Hint { get; set; }
        public GeoPoint Location { get; set; }
        public double RadiusMeters { get; set; } // 10–100 м
        public int? SegmentTimeLimitSec { get; set; }
        public ConfirmationMode Confirmation { get; set; } = ConfirmationMode.GpsAndQr;
        public ExerciseSpec Exercise { get; set; }
        public string PromptRulesetId { get; set; }
        public string QrControlHash { get; set; } // для быстрой локальной проверки
    }

    [Serializable]
    public class ExerciseSpec
    {
        public ExerciseType Type { get; set; } = ExerciseType.Unknown;
        public int Reps { get; set; } = 0;
        public ExerciseVerification Verification { get; set; } = ExerciseVerification.Accelerometer;
    }

    [Serializable]
    public class PromptRuleset
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; }
        public List<string> RequiredKeywords { get; set; } = new();
        public int MinChars { get; set; } = 0;
        public int MaxChars { get; set; } = 500;
        public bool RequireStructure { get; set; } // например, поля Цель/Данные/Формат
        public bool AntiSpamUserUniqueness { get; set; } = true;
        public Dictionary<string, double> ScoreWeights { get; set; } = new(); // structure/meaning/uniqueness
    }

    #endregion

    #region Events / Publishing

    [Serializable]
    public class Event
    {
        public string Id { get; set; } = default!;
        public string RouteId { get; set; } = default!;
        public ActivityType Type { get; set; }
        public Visibility Visibility { get; set; } = Visibility.Private;
        public EventStatus Status { get; set; } = EventStatus.Draft;
        public TimeWindow StartWindow { get; set; } // окно старта/масс-старт
        public List<string> GroupIds { get; set; } = new();
        public List<string> AllowedUserIds { get; set; } = new();
        public bool SynchronousStart { get; set; } = false; // push-уведомление
        public ScoreWeights Score { get; set; } = new();
        public DateTime CreatedAt { get; set; }
    }

    [Serializable]
    public class ScoreWeights
    {
        public double Speed { get; set; } = 1.0;
        public double PromptQuality { get; set; } = 1.0;
        public double HonestyLog { get; set; } = 1.0; // “честность” по аномалиям
    }

    #endregion

    #region Attempts / Logs / Anomalies

    [Serializable]
    public class Attempt
    {
        public string Id { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public string RouteId { get; set; } = default!;
        public int RouteVersion { get; set; } = 1;
        public string? EventId { get; set; } // null для одиночной тренировки
        public AttemptState State { get; set; } = AttemptState.NotStarted;
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public double? TotalDistanceMeters { get; set; }
        public ScoreBreakdown Score { get; set; } = new();
        public List<CheckpointAttempt> Checkpoints { get; set; } = new();
        public List<AttemptLog> Logs { get; set; } = new();         // attempts.logs
        public List<AnomalyLog> Anomalies { get; set; } = new();    // быстрый доступ к флагам
    }

    [Serializable]
    public class CheckpointAttempt
    {
        public string CheckpointId { get; set; } = default!;
        public int Index { get; set; }
        public CheckpointProgressState State { get; set; } = CheckpointProgressState.OutOfRadius;
        public DateTime? EnteredRadiusAt { get; set; }
        public DateTime? QrScannedAt { get; set; }
        public DateTime? ExerciseDoneAt { get; set; }
        public string PromptSubmissionId { get; set; }
        public double SegmentDistanceMeters { get; set; }
        public double SegmentTimeSec { get; set; }
        public double SegmentScore { get; set; }
    }

    [Serializable]
    public class AttemptLog
    {
        public DateTime Timestamp { get; set; }
        public GeoPoint? Position { get; set; }
        public double? AccuracyMeters { get; set; }
        public double? Hdop { get; set; } // для динамической корректировки
        public LogAction Action { get; set; }
        public Dictionary<string, string> Extras { get; set; } = new(); // произвольные поля
    }

    [Serializable]
    public class RadiusAdjustmentLog
    {
        public DateTime Timestamp { get; set; }
        public string CheckpointId { get; set; }
        public double OldRadiusMeters { get; set; }
        public double NewRadiusMeters { get; set; }
        public double? Hdop { get; set; }
        public string Reason { get; set; } // e.g. "HDOP>2.0"
    }

    [Serializable]
    public class AnomalyLog
    {
        public DateTime Timestamp { get; set; }
        public AnomalyFlagType FlagType { get; set; }
        public double? SpeedMps { get; set; } // для SPEED_ANOMALY
        public string? FromCheckpointId { get; set; }
        public string? ToCheckpointId { get; set; }
        public string Note { get; set; }
    }

    [Serializable]
    public class PromptSubmission
    {
        public string Id { get; set; } = default!;
        public string AttemptId { get; set; } = default!;
        public string CheckpointId { get; set; } = default!;
        public string Text { get; set; } = default!;
        public PromptEvaluation Evaluation { get; set; } = new();
        public DateTime CreatedAt { get; set; }
    }

    [Serializable]
    public class PromptEvaluation
    {
        public bool Passed { get; set; }
        public double Structure { get; set; }
        public double Meaning { get; set; }
        public double Uniqueness { get; set; }
        public double TotalScore { get; set; }
        public string Feedback { get; set; }
    }

    [Serializable]
    public class ScoreBreakdown
    {
        public double PointsSpeed { get; set; }
        public double PointsPrompt { get; set; }
        public double PointsHonesty { get; set; }
        public double Bonus { get; set; }
        public double Total => PointsSpeed + PointsPrompt + PointsHonesty + Bonus;
    }

    #endregion

    #region Security / Publishing / QR

    [Serializable]
    public class Publication
    {
        public string Id { get; set; } = default!;
        public string RouteId { get; set; } = default!;
        public Visibility Visibility { get; set; }
        public string? LinkedEventId { get; set; }
        public DateTime PublishedAt { get; set; }
        public string AccessCode { get; set; } // для private по коду/ссылке
    }

    [Serializable]
    public class QrPayload // полезная нагрузка на печать
    {
        public string CheckpointId { get; set; } = default!;
        public string RouteId { get; set; } = default!;
        public string ChecksumHmac { get; set; } = default!;
        public DateTime? Exp { get; set; } // срок действия для соревнований
    }

    #endregion

    #region Analytics / Leaderboard / Inventory (минимум для ТЗ)

    [Serializable]
    public class LeaderboardEntry
    {
        public string Id { get; set; } = default!;     // eventId или global
        public string UserId { get; set; } = default!;
        public double TotalPoints { get; set; }
        public TimeSpan? TotalTime { get; set; }
        public int Rank { get; set; }
        public DateTime CalculatedAt { get; set; }
    }

    [Serializable]
    public class HeatmapBin
    {
        public string Id { get; set; } = default!;
        public GeoPoint Center { get; set; }
        public int AttemptsCount { get; set; }
        public int FailedConfirmations { get; set; }
    }

    [Serializable]
    public class NftAsset
    {
        public string Id { get; set; } = default!;
        public string OwnerUserId { get; set; } = default!;
        public string ContractAddress { get; set; }
        public string TokenId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public DateTime AcquiredAt { get; set; }
    }

    #endregion
}
