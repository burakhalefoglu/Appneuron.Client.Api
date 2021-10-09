using Core.Entities;
using System;

namespace Entities.Concrete
{
    public class ClientDataModel : DocumentDbEntity
    {
        public string ClientId { get; set; }
        public string ProjectId { get; set; }
        public int IsPaidClient { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime PaidTime { get; set; }
    }
}
