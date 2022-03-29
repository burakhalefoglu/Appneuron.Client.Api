using Core.Entities;

namespace Entities.Concrete
{
    public class ChurnPredictionMlResultModel : IEntity
    {
        public long Id { get; set; }
        public bool Status { get; set; }
        public long ProjectId { get; set; }
        public float Value { get; set; }
        
        public DateTimeOffset CreatedAt { get; set; }
    }
}