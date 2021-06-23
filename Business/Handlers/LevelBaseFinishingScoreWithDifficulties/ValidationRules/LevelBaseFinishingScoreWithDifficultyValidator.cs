using Business.Handlers.LevelBaseFinishingScoreWithDifficulties.Commands;
using FluentValidation;

namespace Business.Handlers.LevelBaseFinishingScoreWithDifficulties.ValidationRules
{
    public class CreateLevelBaseFinishingScoreWithDifficultyValidator : AbstractValidator<CreateLevelBaseFinishingScoreWithDifficultyCommand>
    {
        public CreateLevelBaseFinishingScoreWithDifficultyValidator()
        {
            RuleFor(x => x.LevelIndex).NotEmpty();
            RuleFor(x => x.AvarageScore).NotEmpty();
            RuleFor(x => x.DifficultyLevel).NotEmpty();
            RuleFor(x => x.DateTime).NotEmpty();
        }
    }

    public class UpdateLevelBaseFinishingScoreWithDifficultyValidator : AbstractValidator<UpdateLevelBaseFinishingScoreWithDifficultyCommand>
    {
        public UpdateLevelBaseFinishingScoreWithDifficultyValidator()
        {
            RuleFor(x => x.LevelIndex).NotEmpty();
            RuleFor(x => x.AvarageScore).NotEmpty();
            RuleFor(x => x.DifficultyLevel).NotEmpty();
            RuleFor(x => x.DateTime).NotEmpty();
        }
    }
}