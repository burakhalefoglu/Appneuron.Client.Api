using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.ChartModels.OneToOne
{
    public class PlayerCountOnDifficulty
    {
        public int DifficultyLevel { get; set; }
        public long PlayerCount { get; set; }
    }
}
