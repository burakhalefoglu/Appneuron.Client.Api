using Business.Handlers.LevelBasePlayerCountWithDifficulties.Commands;
using FluentValidation;

namespace Business.Handlers.LevelBasePlayerCountWithDifficulties.ValidationRules
{
    public class CreateLevelBasePlayerCountWithDifficultyValidator : AbstractValidator<CreateLevelBasePlayerCountWithDifficultyCommand>
    {
        public CreateLevelBasePlayerCountWithDifficultyValidator()
        {
            RuleFor(x => x.LevelIndex).NotEmpty();
            RuleFor(x => x.DateTime).NotEmpty();
            RuleFor(x => x.PlayerCount).NotEmpty();
            RuleFor(x => x.DifficultyLevel).NotEmpty();
        }
    }

    public class UpdateLevelBasePlayerCountWithDifficultyValidator : AbstractValidator<UpdateLevelBasePlayerCountWithDifficultyCommand>
    {
        public UpdateLevelBasePlayerCountWithDifficultyValidator()
        {
            RuleFor(x => x.LevelIndex).NotEmpty();
            RuleFor(x => x.DateTime).NotEmpty();
            RuleFor(x => x.PlayerCount).NotEmpty();
            RuleFor(x => x.DifficultyLevel).NotEmpty();
        }
    }
}