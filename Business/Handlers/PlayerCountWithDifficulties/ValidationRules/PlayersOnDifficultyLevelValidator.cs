using Business.Handlers.PlayerCountWithDifficulties.Commands;
using FluentValidation;

namespace Business.Handlers.PlayerCountWithDifficulties.ValidationRules
{
    public class CreatePlayersOnDifficultyLevelValidator : AbstractValidator<CreatePlayersOnDifficultyLevelCommand>
    {
        public CreatePlayersOnDifficultyLevelValidator()
        {
            RuleFor(x => x.DifficultyLevel).NotNull();
            RuleFor(x => x.PlayerCount).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
        }
    }

    public class UpdatePlayersOnDifficultyLevelValidator : AbstractValidator<UpdatePlayersOnDifficultyLevelCommand>
    {
        public UpdatePlayersOnDifficultyLevelValidator()
        {
            RuleFor(x => x.DifficultyLevel).NotNull();
            RuleFor(x => x.PlayerCount).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
        }
    }
}