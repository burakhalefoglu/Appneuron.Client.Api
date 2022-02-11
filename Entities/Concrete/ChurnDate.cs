using Core.Entities;

namespace Entities.Concrete
{
    public class ChurnDate: IEntity
    {
        public long ProjectId { get; set; }
        public long ChurnDateMinutes { get; set; }
        public string DateTypeOnGui { get; set; }
        public long Id { get; set; }
        public bool Status = true;
    }
}
