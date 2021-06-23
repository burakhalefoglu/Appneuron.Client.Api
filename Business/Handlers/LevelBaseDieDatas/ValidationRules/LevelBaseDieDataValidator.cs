using Business.Handlers.LevelBaseDieDatas.Commands;
using FluentValidation;

namespace Business.Handlers.LevelBaseDieDatas.ValidationRules
{
    public class CreateLevelBaseDieDataValidator : AbstractValidator<CreateLevelBaseDieDataCommand>
    {
        public CreateLevelBaseDieDataValidator()
        {
            RuleFor(x => x.ProjectID).NotNull();
            RuleFor(x => x.CustomerID).NotNull();
            RuleFor(x => x.DiyingTimeAfterLevelStarting).NotNull();
            RuleFor(x => x.levelName).NotNull();
            RuleFor(x => x.DiyingDifficultyLevel).NotNull();
            RuleFor(x => x.DiyingLocationX).NotNull();
            RuleFor(x => x.DiyingLocationY).NotNull();
            RuleFor(x => x.DiyingLocationZ).NotNull();
        }
    }

    public class UpdateLevelBaseDieDataValidator : AbstractValidator<UpdateLevelBaseDieDataCommand>
    {
        public UpdateLevelBaseDieDataValidator()
        {
            RuleFor(x => x.ProjectID).NotNull();
            RuleFor(x => x.CustomerID).NotNull();
            RuleFor(x => x.DiyingTimeAfterLevelStarting).NotNull();
            RuleFor(x => x.levelName).NotNull();
            RuleFor(x => x.DiyingDifficultyLevel).NotNull();
            RuleFor(x => x.DiyingLocationX).NotNull();
            RuleFor(x => x.DiyingLocationY).NotNull();
            RuleFor(x => x.DiyingLocationZ).NotNull();
        }
    }
}