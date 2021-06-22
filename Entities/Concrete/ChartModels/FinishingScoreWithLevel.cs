using Core.Entities;
using Entities.Concrete.ChartModels.OneToOne;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.ChartModels
{
    public class FinishingScoreWithLevel : DocumentDbEntity
    {
        public string ProjectId { get; set; }
        public LevelBaseScore[] LevelBaseScore { get; set; }
    }
}
