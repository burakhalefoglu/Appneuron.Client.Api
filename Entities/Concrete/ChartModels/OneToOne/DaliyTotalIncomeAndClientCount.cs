using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.ChartModels.OneToOne
{
    public class DaliyTotalIncomeAndClientCount
    {
        public DateTime DateTime { get; set; }
        public long TotalPlayer { get; set; }
        public long TotalRevenue { get; set; }
    }
}
