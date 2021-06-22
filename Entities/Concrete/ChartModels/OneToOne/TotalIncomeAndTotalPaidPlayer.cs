using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.ChartModels.OneToOne
{
    public class TotalIncomeAndTotalPaidPlayer
    {
        public DateTime DateTime { get; set; }
        public long TotalIncome { get; set; }
        public long TotalIncomePlayer { get; set; }
    }
}
