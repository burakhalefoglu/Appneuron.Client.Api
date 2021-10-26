
using Business.Handlers.AdvStrategyBehaviorModels.Commands;
using FluentValidation;

namespace Business.Handlers.AdvStrategyBehaviorModels.ValidationRules
{

    public class CreateAdvStrategyBehaviorModelValidator : AbstractValidator<CreateAdvStrategyBehaviorModelCommand>
    {
        public CreateAdvStrategyBehaviorModelValidator()
        {
            RuleFor(x => x.ProjectId).NotEmpty();
            RuleFor(x => x.Version).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.dateTime).NotEmpty();

        }
    }
    public class UpdateAdvStrategyBehaviorModelValidator : AbstractValidator<UpdateAdvStrategyBehaviorModelCommand>
    {
        public UpdateAdvStrategyBehaviorModelValidator()
        {
            RuleFor(x => x.ProjectId).NotEmpty();
            RuleFor(x => x.Version).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.dateTime).NotEmpty();

        }
    }
}