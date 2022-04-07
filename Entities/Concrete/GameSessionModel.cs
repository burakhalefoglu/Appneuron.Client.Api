using Core.Entities;

namespace Entities.Concrete;

public class GameSessionModel : IEntity
{
    public long ClientId { get; set; }
    public long CustomerId { get; set; }
    public long ProjectId { get; set; }
    public DateTimeOffset SessionStartTime { get; set; }
    public DateTimeOffset SessionFinishTime { get; set; }
    public float SessionTime { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public long Id { get; set; }
    public bool Status { get; set; }
}