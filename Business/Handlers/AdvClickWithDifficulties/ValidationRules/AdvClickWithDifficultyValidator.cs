using Business.Handlers.AdvClickWithDifficulties.Commands;
using FluentValidation;

namespace Business.Handlers.AdvClickWithDifficulties.ValidationRules
{
    public class CreateAdvClickWithDifficultyValidator : AbstractValidator<CreateAdvClickWithDifficultyCommand>
    {
        public CreateAdvClickWithDifficultyValidator()
        {
            RuleFor(x => x.AdvClick).NotNull();
            RuleFor(x => x.DateTime).NotNull();
            RuleFor(x => x.DifficultyLevel).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
        }
    }

    public class UpdateAdvClickWithDifficultyValidator : AbstractValidator<UpdateAdvClickWithDifficultyCommand>
    {
        public UpdateAdvClickWithDifficultyValidator()
        {
            RuleFor(x => x.AdvClick).NotNull();
            RuleFor(x => x.DateTime).NotNull();
            RuleFor(x => x.DifficultyLevel).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
        }
    }
}