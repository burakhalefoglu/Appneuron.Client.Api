using Core.Entities;

namespace Entities.Dtos
{
    public class LevelbaseFailDto : IDto
    {
        public long ClientId { get; set; }
        public string LevelName { get; set; }
        public int FailCount { get; set; }
    }
}