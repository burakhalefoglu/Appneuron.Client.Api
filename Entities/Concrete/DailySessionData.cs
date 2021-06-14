using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class DailySessionData : DocumentDbEntity
    {
        public string ClientId { get; set; }
        public string ProjectID { get; set; }
        public string CustomerID { get; set; }
        public int SessionFrequency { get; set; }
        public float TotalSessionTime { get; set; }
        public DateTime TodayTime { get; set; }
    }
}
