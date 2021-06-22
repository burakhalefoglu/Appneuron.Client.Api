using Core.Entities;
using Entities.Concrete.ChartModels.OneToOne;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.ChartModels
{
    public class PlayerCountsOnLevel : DocumentDbEntity
    {
        public string ProjectID { get; set; }
        public long TotalPlayerCount { get; set; }

        public PlayerCountOnLevel[] PlayerCountOnLevel { get; set; }
    }
}
