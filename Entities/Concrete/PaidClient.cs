using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class PaidClient : DocumentDbEntity
    {
        public string ClientId { get; set; }
        public string ProjectKey { get; set; }
        public DateTime FirstPaidTime { get; set; }
    }
}
