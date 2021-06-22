
using Business.Handlers.StatisticsByNumbers.Commands;
using FluentValidation;

namespace Business.Handlers.StatisticsByNumbers.ValidationRules
{

    public class CreateStatisticsByNumberValidator : AbstractValidator<CreateStatisticsByNumberCommand>
    {
        public CreateStatisticsByNumberValidator()
        {
            RuleFor(x => x.TotalPlayer).NotEmpty();

        }
    }
    public class UpdateStatisticsByNumberValidator : AbstractValidator<UpdateStatisticsByNumberCommand>
    {
        public UpdateStatisticsByNumberValidator()
        {
            RuleFor(x => x.TotalPlayer).NotEmpty();

        }
    }
}