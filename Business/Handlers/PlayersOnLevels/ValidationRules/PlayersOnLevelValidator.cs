
using Business.Handlers.PlayersOnLevels.Commands;
using FluentValidation;

namespace Business.Handlers.PlayersOnLevels.ValidationRules
{

    public class CreatePlayersOnLevelValidator : AbstractValidator<CreatePlayersOnLevelCommand>
    {
        public CreatePlayersOnLevelValidator()
        {

        }
    }
    public class UpdatePlayersOnLevelValidator : AbstractValidator<UpdatePlayersOnLevelCommand>
    {
        public UpdatePlayersOnLevelValidator()
        {

        }
    }
}