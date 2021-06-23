using Business.Handlers.EveryLoginLevelDatas.Commands;
using FluentValidation;

namespace Business.Handlers.EveryLoginLevelDatas.ValidationRules
{
    public class CreateEveryLoginLevelDataValidator : AbstractValidator<CreateEveryLoginLevelDataCommand>
    {
        public CreateEveryLoginLevelDataValidator()
        {
            RuleFor(x => x.ProjectID).NotNull();
            RuleFor(x => x.CustomerID).NotNull();
            RuleFor(x => x.Levelname).NotNull();
            RuleFor(x => x.LevelsDifficultylevel).NotNull();
            RuleFor(x => x.PlayingTime).NotNull();
            RuleFor(x => x.AverageScores).NotNull();
            RuleFor(x => x.IsDead).NotNull();
            RuleFor(x => x.TotalPowerUsage).NotNull();
        }
    }

    public class UpdateEveryLoginLevelDataValidator : AbstractValidator<UpdateEveryLoginLevelDataCommand>
    {
        public UpdateEveryLoginLevelDataValidator()
        {
            RuleFor(x => x.ProjectID).NotNull();
            RuleFor(x => x.CustomerID).NotNull();
            RuleFor(x => x.Levelname).NotNull();
            RuleFor(x => x.LevelsDifficultylevel).NotNull();
            RuleFor(x => x.PlayingTime).NotNull();
            RuleFor(x => x.AverageScores).NotNull();
            RuleFor(x => x.IsDead).NotNull();
            RuleFor(x => x.TotalPowerUsage).NotNull();
        }
    }
}