using Business.Handlers.PowerUsageWithDifficulties.Commands;
using FluentValidation;

namespace Business.Handlers.PowerUsageWithDifficulties.ValidationRules
{
    public class CreatePowerUsageWithDifficultyValidator : AbstractValidator<CreatePowerUsageWithDifficultyCommand>
    {
        public CreatePowerUsageWithDifficultyValidator()
        {
            RuleFor(x => x.DateTime).NotNull();
            RuleFor(x => x.DifficultyLevel).NotNull();
            RuleFor(x => x.PowerUsageCount).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
        }
    }

    public class UpdatePowerUsageWithDifficultyValidator : AbstractValidator<UpdatePowerUsageWithDifficultyCommand>
    {
        public UpdatePowerUsageWithDifficultyValidator()
        {
            RuleFor(x => x.DateTime).NotNull();
            RuleFor(x => x.DifficultyLevel).NotNull();
            RuleFor(x => x.PowerUsageCount).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
        }
    }
}