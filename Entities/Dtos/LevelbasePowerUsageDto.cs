using Core.Entities;

namespace Entities.Dtos
{
    public class LevelbasePowerUsageDto : IDto
    {
        public long ClientId { get; set; }
        public string Levelname { get; set; }
        public int TotalPowerUsage { get; set; }
    }
}