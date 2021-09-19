using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class MlResultDto: IDto
    {
        public string ClientId { get; set; }
        public double ResultValue { get; set; }
        public DateTime DateTime { get; set; }
    }
}
