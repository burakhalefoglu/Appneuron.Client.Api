using Core.Entities;

namespace Entities.Concrete;

public class AdvStrategyBehaviorModel : IEntity
{
    public long ProjectId { get; set; }
    public long ClientId { get; set; }
    public long StrategyId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public int Version { get; set; }
    public string Name { get; set; }
    public long Id { get; set; }
    public bool Status { get; set; }
}