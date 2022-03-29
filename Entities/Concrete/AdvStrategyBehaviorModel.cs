using Core.Entities;

namespace Entities.Concrete;

public class AdvStrategyBehaviorModel : IEntity
{
    public DateTime DateTime { get; set; }

    public long ClientId { get; set; }
    public long ProjectId { get; set; }
    public int Version { get; set; }
    public string Name { get; set; }
    public bool Status { get; set; }
    public long Id { get; set; }
}