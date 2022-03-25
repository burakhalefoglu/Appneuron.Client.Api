using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class ChurnPredictionMlResultModel : IEntity
    {
        public long Id { get; set; }
        public bool Status { get; set; }
        public long ClientId { get; set; }
        public long CustomerId { get; set; }
        public long ProjectId { get; set; }
        public string ModelType { get; set; }
        public DateTime DateTime { get; set; }
        public float ModelResult { get; set; }
    }
}