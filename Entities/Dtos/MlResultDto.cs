using System;
using Core.Entities;

namespace Entities.Dtos
{
    public class MlResultDto : IDto
    {
        public long ClientId { get; set; }
        public double ResultValue { get; set; }
        public DateTime DateTime { get; set; }
    }
}