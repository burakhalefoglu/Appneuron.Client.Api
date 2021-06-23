using Core.Entities;
using System;

namespace Entities.Concrete.ChartModels
{
    public class StatisticsByNumber : DocumentDbEntity
    {
        public string ProjectID { get; set; }
        public DateTime CreatedDate { get; set; }
        public long ClientCount { get; set; }
        public long PaidPlayer { get; set; }
    }
}