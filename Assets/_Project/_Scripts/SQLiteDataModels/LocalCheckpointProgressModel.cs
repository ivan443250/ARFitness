using SQLite;
using System;

public class LocalCheckpointProgressModel
{
    [PrimaryKey]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Indexed]
    public string AttemptId { get; set; }

    [Indexed]
    public string CheckpointId { get; set; }

    public int OrderIndex { get; set; }

    // Статусы
    public bool InRadius { get; set; } = false;
    public bool QrScanned { get; set; } = false;
    public bool ExerciseDone { get; set; } = false;
    public bool PromptSubmitted { get; set; } = false;
    public bool Completed { get; set; } = false;

    // Временные метки
    public DateTime? EnterRadiusTime { get; set; }
    public DateTime? QrScanTime { get; set; }
    public DateTime? ExerciseStartTime { get; set; }
    public DateTime? ExerciseEndTime { get; set; }
    public DateTime? PromptSubmitTime { get; set; }
    public DateTime? CompletedTime { get; set; }

    // Результаты
    public int ExerciseRepetitions { get; set; }
    public double ExerciseDuration { get; set; } // для планки
    public string ExerciseData { get; set; } // JSON с данными акселерометра

    // Промт
    public string PromptGoal { get; set; }
    public string PromptData { get; set; }
    public string PromptFormat { get; set; }
    public int LocalScore { get; set; }
    public int FinalScore { get; set; }
    public bool PendingServerValidation { get; set; } = true;

    public int PointsEarned { get; set; }
    public double TimeSpent { get; set; } // секунд
}
