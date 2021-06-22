using Core.Entities;
using Entities.Concrete.ChartModels.OneToOne;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.ChartModels
{
    public class ProjectBuyingCountWithDifficulty : DocumentDbEntity
    {
        public string ProjectId { get; set; }
        public BuyingCountWithDifficulty[] BuyingCountWithDifficulty { get; set; }
    }
}
