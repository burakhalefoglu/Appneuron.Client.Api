﻿using Core.Entities;
using System;

namespace Entities.Concrete.ChartModels
{
    public class LevelBaseFinishingScoreWithDifficulty : DocumentDbEntity
    {
        public string ProjectId { get; set; }
        public int LevelIndex { get; set; }
        public long AvarageScore { get; set; }
        public int DifficultyLevel { get; set; }
        public DateTime DateTime { get; set; }
    }
}