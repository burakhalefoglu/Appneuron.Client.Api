using Core.Entities;

namespace Entities.Concrete;

public class ClientDataModel : IEntity
{
    public long ProjectId { get; set; }
    public byte IsPaidClient { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset PaidTime { get; set; }
    public long Id { get; set; }
    public bool Status { get; set; }
}