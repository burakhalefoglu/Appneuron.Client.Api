using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.ChartModels.OneToOne
{
    public class DailyRevenue
    {
        public DateTime DayTime { get; set; }
        public long RevenueCount { get; set; }
    }
}
