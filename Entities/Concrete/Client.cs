using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Client : DocumentDbEntity
    {
        public string ClientId { get; set; }
        public string ProjectKey { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsPaidClient { get; set; }
        public DateTime PaidTime { get; set; }
    }
}
