using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class ClientsOfferModelDto
    {
        public int Version { get; set; }
        public string OfferName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public bool Status = true;
    }
}
