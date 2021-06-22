using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.ChartModels
{
    public class StatisticsByNumber : DocumentDbEntity
    {
        public string ProjectID { get; set; }
        public long TotalPlayer { get; set; }
        public PlayerCountOnDate[] PlayerCountOnDate { get; set; }
    }
}
