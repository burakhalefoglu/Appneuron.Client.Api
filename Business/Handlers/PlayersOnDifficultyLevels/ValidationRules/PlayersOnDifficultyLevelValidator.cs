
using Business.Handlers.PlayersOnDifficultyLevels.Commands;
using FluentValidation;

namespace Business.Handlers.PlayersOnDifficultyLevels.ValidationRules
{

    public class CreatePlayersOnDifficultyLevelValidator : AbstractValidator<CreatePlayersOnDifficultyLevelCommand>
    {
        public CreatePlayersOnDifficultyLevelValidator()
        {

        }
    }
    public class UpdatePlayersOnDifficultyLevelValidator : AbstractValidator<UpdatePlayersOnDifficultyLevelCommand>
    {
        public UpdatePlayersOnDifficultyLevelValidator()
        {

        }
    }
}