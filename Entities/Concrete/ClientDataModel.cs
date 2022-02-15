using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class ClientDataModel : IEntity
    {        
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public byte IsPaidClient { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime PaidTime { get; set; }
        
        public bool Status = true;

    }
}