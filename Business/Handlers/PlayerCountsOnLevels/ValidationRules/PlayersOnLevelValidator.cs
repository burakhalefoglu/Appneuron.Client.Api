
using Business.Handlers.PlayerCountsOnLevels.Commands;
using FluentValidation;

namespace Business.Handlers.PlayerCountsOnLevels.ValidationRules
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