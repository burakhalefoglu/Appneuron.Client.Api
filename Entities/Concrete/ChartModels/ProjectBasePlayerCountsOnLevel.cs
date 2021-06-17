using Core.Entities;
using Entities.Concrete.ChartModels.OneToOne;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.ChartModels
{
    public class ProjectBasePlayerCountsOnLevel : DocumentDbEntity
    {
        public string ProjectID { get; set; }
        public PlayerCountOnLevel[] PlayerCountOnLevel { get; set; }
    }
}
