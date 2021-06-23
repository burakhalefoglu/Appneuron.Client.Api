using Business.Handlers.LevelBaseSessionWithDifficulties.Commands;
using FluentValidation;

namespace Business.Handlers.LevelBaseSessionWithDifficulties.ValidationRules
{
    public class CreateLevelBaseSessionWithDifficultyValidator : AbstractValidator<CreateLevelBaseSessionWithDifficultyCommand>
    {
        public CreateLevelBaseSessionWithDifficultyValidator()
        {
            RuleFor(x => x.SessionTime).NotNull();
            RuleFor(x => x.SessionCount).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
            RuleFor(x => x.LevelIndex).NotNull();
            RuleFor(x => x.DifficultyLevel).NotNull();
        }
    }

    public class UpdateLevelBaseSessionWithDifficultyValidator : AbstractValidator<UpdateLevelBaseSessionWithDifficultyCommand>
    {
        public UpdateLevelBaseSessionWithDifficultyValidator()
        {
            RuleFor(x => x.SessionTime).NotNull();
            RuleFor(x => x.SessionCount).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
            RuleFor(x => x.LevelIndex).NotNull();
            RuleFor(x => x.DifficultyLevel).NotNull();
        }
    }
}