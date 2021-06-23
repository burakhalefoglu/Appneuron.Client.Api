using Business.Handlers.GameSessionEveryLoginDatas.Commands;
using FluentValidation;

namespace Business.Handlers.GameSessionEveryLoginDatas.ValidationRules
{
    public class CreateGameSessionEveryLoginDataValidator : AbstractValidator<CreateGameSessionEveryLoginDataCommand>
    {
        public CreateGameSessionEveryLoginDataValidator()
        {
            RuleFor(x => x.ProjectID).NotNull();
            RuleFor(x => x.CustomerID).NotNull();
            RuleFor(x => x.SessionStartTime).NotNull();
            RuleFor(x => x.SessionFinishTime).NotNull();
            RuleFor(x => x.SessionTimeMinute).NotNull();
        }
    }

    public class UpdateGameSessionEveryLoginDataValidator : AbstractValidator<UpdateGameSessionEveryLoginDataCommand>
    {
        public UpdateGameSessionEveryLoginDataValidator()
        {
            RuleFor(x => x.ProjectID).NotNull();
            RuleFor(x => x.CustomerID).NotNull();
            RuleFor(x => x.SessionStartTime).NotNull();
            RuleFor(x => x.SessionFinishTime).NotNull();
            RuleFor(x => x.SessionTimeMinute).NotNull();
        }
    }
}