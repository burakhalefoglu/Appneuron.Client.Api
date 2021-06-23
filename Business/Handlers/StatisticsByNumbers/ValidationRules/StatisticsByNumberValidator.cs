using Business.Handlers.StatisticsByNumbers.Commands;
using FluentValidation;

namespace Business.Handlers.StatisticsByNumbers.ValidationRules
{
    public class CreateStatisticsByNumberValidator : AbstractValidator<CreateStatisticsByNumberCommand>
    {
        public CreateStatisticsByNumberValidator()
        {
            RuleFor(x => x.ClientCount).NotEmpty();
            RuleFor(x => x.CreatedDate).NotEmpty();
            RuleFor(x => x.PaidPlayer).NotEmpty();
            RuleFor(x => x.ProjectID).NotEmpty();
        }
    }

    public class UpdateStatisticsByNumberValidator : AbstractValidator<UpdateStatisticsByNumberCommand>
    {
        public UpdateStatisticsByNumberValidator()
        {
            RuleFor(x => x.ClientCount).NotEmpty();
            RuleFor(x => x.CreatedDate).NotEmpty();
            RuleFor(x => x.PaidPlayer).NotEmpty();
            RuleFor(x => x.ProjectID).NotEmpty();
        }
    }
}