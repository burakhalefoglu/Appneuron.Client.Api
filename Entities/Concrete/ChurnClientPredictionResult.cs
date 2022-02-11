using Core.Entities;
using Entities.Dtos;
using System;

namespace Entities.Concrete
{
    public class ChurnClientPredictionResult: IEntity
    {
        public long ClientId { get; set; }
        public long ProjectId { get; set; }
        public DateTime ChurnPredictionDate { get; set; }
        
        public bool Status = true;
        public long Id { get; set; }
    }
}
 