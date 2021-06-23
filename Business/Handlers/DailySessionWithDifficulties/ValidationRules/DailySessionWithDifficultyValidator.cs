using Business.Handlers.DailySessionWithDifficulties.Commands;
using FluentValidation;

namespace Business.Handlers.DailySessionWithDifficulties.ValidationRules
{
    public class CreateDailySessionWithDifficultyValidator : AbstractValidator<CreateDailySessionWithDifficultyCommand>
    {
        public CreateDailySessionWithDifficultyValidator()
        {
            RuleFor(x => x.AvarageTimeSession).NotNull();
            RuleFor(x => x.DateTimePerThreeHour).NotNull();
            RuleFor(x => x.DifficultyLevel).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
        }
    }

    public class UpdateDailySessionWithDifficultyValidator : AbstractValidator<UpdateDailySessionWithDifficultyCommand>
    {
        public UpdateDailySessionWithDifficultyValidator()
        {
            RuleFor(x => x.AvarageTimeSession).NotNull();
            RuleFor(x => x.DateTimePerThreeHour).NotNull();
            RuleFor(x => x.DifficultyLevel).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
        }
    }
}