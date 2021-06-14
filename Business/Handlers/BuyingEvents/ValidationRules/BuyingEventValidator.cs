
using Business.Handlers.BuyingEvents.Commands;
using FluentValidation;

namespace Business.Handlers.BuyingEvents.ValidationRules
{

    public class CreateBuyingEventValidator : AbstractValidator<CreateBuyingEventCommand>
    {
        public CreateBuyingEventValidator()
        {
            RuleFor(x => x.ProjectID).NotNull();
            RuleFor(x => x.CustomerID).NotNull();
            RuleFor(x => x.TrigersInlevelName).NotNull();
            RuleFor(x => x.ProductType).NotNull();
            RuleFor(x => x.DifficultyLevel).NotNull();
            RuleFor(x => x.InWhatMinutes).NotNull();
            RuleFor(x => x.TrigerdTime).NotNull();

        }
    }
    public class UpdateBuyingEventValidator : AbstractValidator<UpdateBuyingEventCommand>
    {
        public UpdateBuyingEventValidator()
        {
            RuleFor(x => x.ProjectID).NotNull();
            RuleFor(x => x.CustomerID).NotNull();
            RuleFor(x => x.TrigersInlevelName).NotNull();
            RuleFor(x => x.ProductType).NotNull();
            RuleFor(x => x.DifficultyLevel).NotNull();
            RuleFor(x => x.InWhatMinutes).NotNull();
            RuleFor(x => x.TrigerdTime).NotNull();

        }
    }
}