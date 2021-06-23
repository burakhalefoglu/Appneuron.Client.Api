using Business.Handlers.AdvEvents.Commands;
using FluentValidation;

namespace Business.Handlers.AdvEvents.ValidationRules
{
    public class CreateAdvEventValidator : AbstractValidator<CreateAdvEventCommand>
    {
        public CreateAdvEventValidator()
        {
            RuleFor(x => x.ProjectID).NotNull();
            RuleFor(x => x.CustomerID).NotNull();
            RuleFor(x => x.TrigersInlevelName).NotNull();
            RuleFor(x => x.AdvType).NotNull();
            RuleFor(x => x.DifficultyLevel).NotNull();
            RuleFor(x => x.InMinutes).NotNull();
            RuleFor(x => x.TrigerdTime).NotNull();
        }
    }

    public class UpdateAdvEventValidator : AbstractValidator<UpdateAdvEventCommand>
    {
        public UpdateAdvEventValidator()
        {
            RuleFor(x => x.ProjectID).NotNull();
            RuleFor(x => x.CustomerID).NotNull();
            RuleFor(x => x.TrigersInlevelName).NotNull();
            RuleFor(x => x.AdvType).NotNull();
            RuleFor(x => x.DifficultyLevel).NotNull();
            RuleFor(x => x.InMinutes).NotNull();
            RuleFor(x => x.TrigerdTime).NotNull();
        }
    }
}