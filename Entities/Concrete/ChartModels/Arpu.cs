using Core.Entities;
using System;

namespace Entities.Concrete.ChartModels
{
    public class Arpu : DocumentDbEntity
    {
        public string ProjectId { get; set; }
        public DateTime DateTime { get; set; }
        public long TotalPlayer { get; set; }
        public long TotalRevenue { get; set; }
    }
}