using System;
using Core.Entities;

namespace Entities.Dtos
{
    public class DailyDieCountDto : IDto
    {
        public long ClientId { get; set; }
        public int DieCount { get; set; }
        public DateTime DateTime { get; set; }
    }
}