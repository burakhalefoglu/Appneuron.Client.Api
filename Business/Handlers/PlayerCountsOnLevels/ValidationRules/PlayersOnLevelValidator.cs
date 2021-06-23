using Business.Handlers.PlayerCountsOnLevels.Commands;
using FluentValidation;

namespace Business.Handlers.PlayerCountsOnLevels.ValidationRules
{
    public class CreatePlayersOnLevelValidator : AbstractValidator<CreatePlayersOnLevelCommand>
    {
        public CreatePlayersOnLevelValidator()
        {
            RuleFor(x => x.LevelIndex).NotNull();
            RuleFor(x => x.PaidPlayerCount).NotNull();
            RuleFor(x => x.ProjectID).NotNull();
            RuleFor(x => x.TotalPlayerCount).NotNull();
            RuleFor(x => x.DateTime).NotNull();
        }
    }

    public class UpdatePlayersOnLevelValidator : AbstractValidator<UpdatePlayersOnLevelCommand>
    {
        public UpdatePlayersOnLevelValidator()
        {
            RuleFor(x => x.LevelIndex).NotNull();
            RuleFor(x => x.PaidPlayerCount).NotNull();
            RuleFor(x => x.ProjectID).NotNull();
            RuleFor(x => x.TotalPlayerCount).NotNull();
            RuleFor(x => x.DateTime).NotNull();
        }
    }
}