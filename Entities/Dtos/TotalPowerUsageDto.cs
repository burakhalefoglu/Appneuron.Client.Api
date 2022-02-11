using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class TotalPowerUsageDto : IDto
    {
        public long ClientId { get; set; }
        public int TotalPowerUsage { get; set; }
        public DateTime Date { get; set; }

    }
}
