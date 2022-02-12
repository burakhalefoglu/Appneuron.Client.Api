using System;
using Core.Entities;

namespace Entities.Dtos
{
    public class BuyingEventDto : IDto
    {
        public long ClientId { get; set; }
        public DateTime TrigerdTime { get; set; }
    }
}