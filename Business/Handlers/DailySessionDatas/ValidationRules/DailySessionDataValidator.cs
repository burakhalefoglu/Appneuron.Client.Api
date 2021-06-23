using Business.Handlers.DailySessionDatas.Commands;
using FluentValidation;

namespace Business.Handlers.DailySessionDatas.ValidationRules
{
    public class CreateDailySessionDataValidator : AbstractValidator<CreateDailySessionDataCommand>
    {
        public CreateDailySessionDataValidator()
        {
            RuleFor(x => x.ProjectID).NotNull();
            RuleFor(x => x.CustomerID).NotNull();
            RuleFor(x => x.SessionFrequency).NotNull();
            RuleFor(x => x.TotalSessionTime).NotNull();
            RuleFor(x => x.TodayTime).NotNull();
        }
    }

    public class UpdateDailySessionDataValidator : AbstractValidator<UpdateDailySessionDataCommand>
    {
        public UpdateDailySessionDataValidator()
        {
            RuleFor(x => x.ProjectID).NotNull();
            RuleFor(x => x.CustomerID).NotNull();
            RuleFor(x => x.SessionFrequency).NotNull();
            RuleFor(x => x.TotalSessionTime).NotNull();
            RuleFor(x => x.TodayTime).NotNull();
        }
    }
}