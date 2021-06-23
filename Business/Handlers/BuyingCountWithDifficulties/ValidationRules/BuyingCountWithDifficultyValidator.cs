using Business.Handlers.BuyingCountWithDifficulties.Commands;
using FluentValidation;

namespace Business.Handlers.BuyingCountWithDifficulties.ValidationRules
{
    public class CreateBuyingCountWithDifficultyValidator : AbstractValidator<CreateBuyingCountWithDifficultyCommand>
    {
        public CreateBuyingCountWithDifficultyValidator()
        {
            RuleFor(x => x.BuyingCount).NotNull();
            RuleFor(x => x.DateTime).NotNull();
            RuleFor(x => x.DifficultyLevel).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
        }
    }

    public class UpdateBuyingCountWithDifficultyValidator : AbstractValidator<UpdateBuyingCountWithDifficultyCommand>
    {
        public UpdateBuyingCountWithDifficultyValidator()
        {
            RuleFor(x => x.BuyingCount).NotNull();
            RuleFor(x => x.DateTime).NotNull();
            RuleFor(x => x.DifficultyLevel).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
        }
    }
}