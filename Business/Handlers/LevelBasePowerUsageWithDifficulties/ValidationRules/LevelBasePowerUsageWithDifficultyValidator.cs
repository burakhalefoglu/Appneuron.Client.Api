using Business.Handlers.LevelBasePowerUsageWithDifficulties.Commands;
using FluentValidation;

namespace Business.Handlers.LevelBasePowerUsageWithDifficulties.ValidationRules
{
    public class CreateLevelBasePowerUsageWithDifficultyValidator : AbstractValidator<CreateLevelBasePowerUsageWithDifficultyCommand>
    {
        public CreateLevelBasePowerUsageWithDifficultyValidator()
        {
            RuleFor(x => x.DifficultyLevel).NotEmpty();
            RuleFor(x => x.PowerUsageCount).NotEmpty();
            RuleFor(x => x.DateTime).NotEmpty();
            RuleFor(x => x.LevelIndex).NotEmpty();
        }
    }

    public class UpdateLevelBasePowerUsageWithDifficultyValidator : AbstractValidator<UpdateLevelBasePowerUsageWithDifficultyCommand>
    {
        public UpdateLevelBasePowerUsageWithDifficultyValidator()
        {
            RuleFor(x => x.DifficultyLevel).NotEmpty();
            RuleFor(x => x.PowerUsageCount).NotEmpty();
            RuleFor(x => x.DateTime).NotEmpty();
            RuleFor(x => x.LevelIndex).NotEmpty();
        }
    }
}