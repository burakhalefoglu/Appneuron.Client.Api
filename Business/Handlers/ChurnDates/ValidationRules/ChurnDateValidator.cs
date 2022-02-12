using Business.Handlers.ChurnDates.Commands;
using FluentValidation;

namespace Business.Handlers.ChurnDates.ValidationRules
{
    public class CreateChurnDateValidator : AbstractValidator<CreateChurnDateCommand>
    {
        public CreateChurnDateValidator()
        {
            RuleFor(x => x.ChurnDateMinutes).NotEmpty();
        }
    }

    public class UpdateChurnDateValidator : AbstractValidator<UpdateChurnDateCommand>
    {
        public UpdateChurnDateValidator()
        {
            RuleFor(x => x.ChurnDateMinutes).NotEmpty();
        }
    }
}