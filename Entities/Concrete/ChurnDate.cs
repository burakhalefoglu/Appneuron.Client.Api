using Core.Entities;

namespace Entities.Concrete
{
    public class ChurnDate: DocumentDbEntity
    {
        public string ProjectId { get; set; }
        public long churnDateMinutes { get; set; }
        public string DateTypeOnGui { get; set; }
    }
}
