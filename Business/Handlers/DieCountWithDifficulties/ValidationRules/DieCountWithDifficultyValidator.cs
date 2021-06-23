using Business.Handlers.DieCountWithDifficulties.Commands;
using FluentValidation;

namespace Business.Handlers.DieCountWithDifficulties.ValidationRules
{
    public class CreateDieCountWithDifficultyValidator : AbstractValidator<CreateDieCountWithDifficultyCommand>
    {
        public CreateDieCountWithDifficultyValidator()
        {
            RuleFor(x => x.DateTime).NotNull();
            RuleFor(x => x.DifficultyLevel).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
            RuleFor(x => x.TotalDie).NotNull();
        }
    }

    public class UpdateDieCountWithDifficultyValidator : AbstractValidator<UpdateDieCountWithDifficultyCommand>
    {
        public UpdateDieCountWithDifficultyValidator()
        {
            RuleFor(x => x.DateTime).NotNull();
            RuleFor(x => x.DifficultyLevel).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
            RuleFor(x => x.TotalDie).NotNull();
        }
    }
}