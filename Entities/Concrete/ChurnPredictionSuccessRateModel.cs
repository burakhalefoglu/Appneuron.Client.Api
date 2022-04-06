using Core.Entities;

namespace Entities.Concrete;

public class ChurnPredictionSuccessRateModel : IEntity
{
    public long ProjectId { get; set; }
    public float Value { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public long Id { get; set; }
    public bool Status { get; set; }
}