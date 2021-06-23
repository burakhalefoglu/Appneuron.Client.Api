using Business.Handlers.SuccessAttemptRateWithDifficulties.Commands;
using FluentValidation;

namespace Business.Handlers.SuccessAttemptRateWithDifficulties.ValidationRules
{
    public class CreateSuccessAttemptRateWithDifficultyValidator : AbstractValidator<CreateSuccessAttemptRateWithDifficultyCommand>
    {
        public CreateSuccessAttemptRateWithDifficultyValidator()
        {
            RuleFor(x => x.DateTime).NotNull();
            RuleFor(x => x.DifficultyLevel).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
            RuleFor(x => x.SuccessAttempt).NotNull();
        }
    }

    public class UpdateSuccessAttemptRateWithDifficultyValidator : AbstractValidator<UpdateSuccessAttemptRateWithDifficultyCommand>
    {
        public UpdateSuccessAttemptRateWithDifficultyValidator()
        {
            RuleFor(x => x.DateTime).NotNull();
            RuleFor(x => x.DifficultyLevel).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
            RuleFor(x => x.SuccessAttempt).NotNull();
        }
    }
}