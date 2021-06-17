
using Business.Handlers.PlayerListByDays.Commands;
using FluentValidation;

namespace Business.Handlers.PlayerListByDays.ValidationRules
{

    public class CreatePlayerListByDayValidator : AbstractValidator<CreatePlayerListByDayCommand>
    {
        public CreatePlayerListByDayValidator()
        {
            RuleFor(x => x.DateTime).NotEmpty();

        }
    }
    public class UpdatePlayerListByDayValidator : AbstractValidator<UpdatePlayerListByDayCommand>
    {
        public UpdatePlayerListByDayValidator()
        {
            RuleFor(x => x.DateTime).NotEmpty();

        }
    }
}