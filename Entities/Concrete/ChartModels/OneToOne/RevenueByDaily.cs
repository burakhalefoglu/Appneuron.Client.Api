using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.ChartModels.OneToOne
{
    public class RevenueByDaily
    {
        public long TotalPlayer { get; set; }
        public DateTime DateTime { get; set; }
        public long PaidPlayer { get; set; }
    }
}
