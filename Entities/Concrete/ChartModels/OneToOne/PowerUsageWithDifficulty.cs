using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.ChartModels.OneToOne
{
    public class PowerUsageWithDifficulty
    {
        public int DifficultyLevel { get; set; }
        public long PowerUsageCount { get; set; }
    }
}
