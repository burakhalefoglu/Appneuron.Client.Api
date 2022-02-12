using Core.Entities;

namespace Entities.Concrete
{
    public class ChurnDate : IEntity
    {
        public bool Status = true;
        public long ProjectId { get; set; }
        public long ChurnDateMinutes { get; set; }
        public string DateTypeOnGui { get; set; }
        public long Id { get; set; }
    }
}