using Core.Entities;

namespace Entities.Dtos;

public class AdvStrategyShownCountDto: IDto
{
    public AdvStrategyShownCountDto()
    {
        StrategyNames = new List<string>();
        Counts = new List<int>();
    }

    public List<string> StrategyNames { get; set; }
    public  List<int>  Counts { get; set; }
}