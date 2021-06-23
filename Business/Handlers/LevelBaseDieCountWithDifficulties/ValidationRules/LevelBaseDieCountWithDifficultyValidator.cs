using Business.Handlers.LevelBaseDieCountWithDifficulties.Commands;
using FluentValidation;

namespace Business.Handlers.LevelBaseDieCountWithDifficulties.ValidationRules
{
    public class CreateLevelBaseDieCountWithDifficultyValidator : AbstractValidator<CreateLevelBaseDieCountWithDifficultyCommand>
    {
        public CreateLevelBaseDieCountWithDifficultyValidator()
        {
            RuleFor(x => x.LevelIndex).NotEmpty();
            RuleFor(x => x.DieCount).NotEmpty();
            RuleFor(x => x.DateTime).NotEmpty();
            RuleFor(x => x.DifficultyLevel).NotEmpty();
        }
    }

    public class UpdateLevelBaseDieCountWithDifficultyValidator : AbstractValidator<UpdateLevelBaseDieCountWithDifficultyCommand>
    {
        public UpdateLevelBaseDieCountWithDifficultyValidator()
        {
            RuleFor(x => x.LevelIndex).NotEmpty();
            RuleFor(x => x.DieCount).NotEmpty();
            RuleFor(x => x.DateTime).NotEmpty();
            RuleFor(x => x.DifficultyLevel).NotEmpty();
        }
    }
}