using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.ChartModels.OneToOne
{
    public class PlayerCountOnLevel
    {
        public int LevelIndex { get; set; }
        public DateTime DateTime { get; set; }
        public long PaidPlayerCount { get; set; }
        public long OldTotalPlayerCount { get; set; }
    }
}
