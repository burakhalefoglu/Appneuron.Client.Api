using Business.Handlers.PlayerListByDaysWithDifficulty.Commands;
using FluentValidation;

namespace Business.Handlers.PlayerListByDaysWithDifficulty.ValidationRules
{
    public class CreatePlayerListByDayValidator : AbstractValidator<CreatePlayerListByDayCommand>
    {
        public CreatePlayerListByDayValidator()
        {
            RuleFor(x => x.ClientId).NotNull();
            RuleFor(x => x.DateTime).NotNull();
            RuleFor(x => x.DifficultyLevel).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
        }
    }

    public class UpdatePlayerListByDayValidator : AbstractValidator<UpdatePlayerListByDayCommand>
    {
        public UpdatePlayerListByDayValidator()
        {
            RuleFor(x => x.ClientId).NotNull();
            RuleFor(x => x.DateTime).NotNull();
            RuleFor(x => x.DifficultyLevel).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
        }
    }
}