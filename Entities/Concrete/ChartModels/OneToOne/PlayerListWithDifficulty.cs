using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.ChartModels.OneToOne
{
    public class PlayerListWithDifficulty
    {
        public DateTime DateTime { get; set; }
        public string ClientId { get; set; }
        public int DifficultyLevel { get; set; }
    }
}
