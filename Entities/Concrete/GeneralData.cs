using Core.Entities;

namespace Entities.Concrete
{
    public class GeneralData : DocumentDbEntity
    {
        public string ClientId { get; set; }
        public string ProjectID { get; set; }
        public string CustomerID { get; set; }
        public int PlayersDifficultylevel { get; set; }
    }
}