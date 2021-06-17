using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.ChartModels
{
    public class PlayerCountOnDate
    {
        public DateTime CreatedDate { get; set; }
        public long ClientCount { get; set; }
    }
}
