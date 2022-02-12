using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class ChurnClientPredictionResult : IEntity
    {
        public bool Status = true;
        public long ClientId { get; set; }
        public long ProjectId { get; set; }
        public DateTime ChurnPredictionDate { get; set; }
        public long Id { get; set; }
    }
}