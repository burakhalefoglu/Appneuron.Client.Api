
using Business.Handlers.PlayerListByDaysWithDifficulty.Commands;
using FluentValidation;

namespace Business.Handlers.PlayerListByDaysWithDifficulty.ValidationRules
{

    public class CreatePlayerListByDayValidator : AbstractValidator<CreatePlayerListByDayCommand>
    {
        public CreatePlayerListByDayValidator()
        {

        }
    }
    public class UpdatePlayerListByDayValidator : AbstractValidator<UpdatePlayerListByDayCommand>
    {
        public UpdatePlayerListByDayValidator()
        {

        }
    }
}