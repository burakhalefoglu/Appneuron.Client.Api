using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class ChurnBlokerMlResult : IEntity
    {
        public bool Status = true;
        public long ProjectId { get; set; }
        public long ProductId { get; set; }
        public long ClientId { get; set; }
        public double ResultValue { get; set; }
        public DateTime DateTime { get; set; }
        public long Id { get; set; }
    }
}