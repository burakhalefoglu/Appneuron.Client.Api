using Core.Entities;

namespace Entities.Dtos
{
    public class ClientClickDto : IDto
    {
        public long ClientId { get; set; }
        public int ClickCount { get; set; }
    }
}