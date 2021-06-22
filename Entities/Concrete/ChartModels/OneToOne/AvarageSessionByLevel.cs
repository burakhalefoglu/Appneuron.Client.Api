using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.ChartModels.OneToOne
{
    public class AvarageSessionByLevel
    {
        public int LevelIndex { get; set; }
        public int DifficultyLevel { get; set; }
        public string PlayerCount { get; set; }
    }
}
