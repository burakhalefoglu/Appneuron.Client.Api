using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class ClientDto
    {
        public string ClientId { get; set; }
        public string ProjectKey { get; set; }
        public int IsPaidClient { get; set; }
    }
}
