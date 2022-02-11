using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class ChurnBlokerMlResult : IEntity
    {
        public long ProjectId { get; set; }
        public long ProductId { get; set; }
        public long ClientId { get; set; }
        public double ResultValue { get; set; }
        public DateTime DateTime { get; set; }
        public long Id { get; set; }
        public bool Status = true;
    }
}



