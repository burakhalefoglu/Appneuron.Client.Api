
using Business.Handlers.LevelBaseSessionDatas.Commands;
using FluentValidation;

namespace Business.Handlers.LevelBaseSessionDatas.ValidationRules
{

    public class CreateLevelBaseSessionDataValidator : AbstractValidator<CreateLevelBaseSessionDataCommand>
    {
        public CreateLevelBaseSessionDataValidator()
        {
            RuleFor(x => x.ProjectID).NotNull();
            RuleFor(x => x.CustomerID).NotNull();
            RuleFor(x => x.levelName).NotNull();
            RuleFor(x => x.DifficultyLevel).NotNull();
            RuleFor(x => x.SessionTimeMinute).NotNull();
            RuleFor(x => x.SessionStartTime).NotNull();
            RuleFor(x => x.SessionFinishTime).NotNull();

        }
    }
    public class UpdateLevelBaseSessionDataValidator : AbstractValidator<UpdateLevelBaseSessionDataCommand>
    {
        public UpdateLevelBaseSessionDataValidator()
        {
            RuleFor(x => x.ProjectID).NotNull();
            RuleFor(x => x.CustomerID).NotNull();
            RuleFor(x => x.levelName).NotNull();
            RuleFor(x => x.DifficultyLevel).NotNull();
            RuleFor(x => x.SessionTimeMinute).NotNull();
            RuleFor(x => x.SessionStartTime).NotNull();
            RuleFor(x => x.SessionFinishTime).NotNull();

        }
    }
}