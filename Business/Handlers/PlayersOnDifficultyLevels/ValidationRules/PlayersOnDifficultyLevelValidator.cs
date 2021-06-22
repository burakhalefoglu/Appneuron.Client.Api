
using Business.Handlers.PlayerCountOnDifficultyLevels.Commands;
using FluentValidation;

namespace Business.Handlers.PlayerCountOnDifficultyLevels.ValidationRules
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