using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class DailyDieCountDto : IDto
    {
        public string ClientId { get; set; } 
        public int DieCount { get; set; }
        public DateTime DateTime { get; set; }

    }
}
