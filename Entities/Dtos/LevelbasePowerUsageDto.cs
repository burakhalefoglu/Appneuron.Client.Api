using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class LevelbasePowerUsageDto : IDto
    {
        public string ClientId { get; set; }
        public string Levelname { get; set; }
        public int TotalPowerUsage { get; set; }

    }
}
