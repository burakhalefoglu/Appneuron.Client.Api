
using Business.Handlers.OfferBehaviorModels.Commands;
using FluentValidation;

namespace Business.Handlers.OfferBehaviorModels.ValidationRules
{

    public class CreateOfferBehaviorModelValidator : AbstractValidator<CreateOfferBehaviorModelCommand>
    {
        public CreateOfferBehaviorModelValidator()
        {
            RuleFor(x => x.ProjectId).NotEmpty();
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.OfferId).NotEmpty();
            RuleFor(x => x.OfferName).NotEmpty();
            RuleFor(x => x.dateTime).NotEmpty();
            RuleFor(x => x.IsBuyOffer).NotEmpty();

        }
    }
    public class UpdateOfferBehaviorModelValidator : AbstractValidator<UpdateOfferBehaviorModelCommand>
    {
        public UpdateOfferBehaviorModelValidator()
        {
            RuleFor(x => x.ProjectId).NotEmpty();
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.OfferId).NotEmpty();
            RuleFor(x => x.OfferName).NotEmpty();
            RuleFor(x => x.dateTime).NotEmpty();
            RuleFor(x => x.IsBuyOffer).NotEmpty();

        }
    }
}