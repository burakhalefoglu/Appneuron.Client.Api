using Core.Entities;

namespace Entities.Dtos;

public class LevelbaseSessionWithPlayingTimeDto : IDto
{
    public long ClientId { get; set; }
    public string LevelName { get; set; }
    public DateTime SessionStartTime { get; set; }
    public float SessionTimeMinute { get; set; }
}