using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class ChurnBlokerMlResult : IEntity
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public long ProductId { get; set; }
        public long ClientId { get; set; }
        
        public string ModelType { get; set; }
        public float ModelResult { get; set; }
        public DateTime DateTime { get; set; }
        
        public bool Status = true;

    }
}