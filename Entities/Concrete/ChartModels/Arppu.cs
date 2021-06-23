using Core.Entities;
using System;

namespace Entities.Concrete.ChartModels
{
    public class Arppu : DocumentDbEntity
    {
        public string ProjectId { get; set; }
        public DateTime DateTime { get; set; }
        public long TotalIncome { get; set; }
        public long TotalIncomePlayer { get; set; }
    }
}