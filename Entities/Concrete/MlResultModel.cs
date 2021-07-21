using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class MlResultModel : DocumentDbEntity
    {
        public string ProjectId { get; set; }
        public short ProductId { get; set; }
        public string ClientId { get; set; }
        public double ResultValue { get; set; }
        public DateTime DateTime { get; set; }
    }
}
