using Core.Entities;

namespace Entities.Dtos;

public class OfferBehaviorSuccessDto : IDto
{
    public OfferBehaviorSuccessDto()
    {
        OfferNames = new List<string>();
        SuccessPercents = new List<int>();
    }

    public List<string> OfferNames { get; set; }
    public  List<int>  SuccessPercents { get; set; }
}