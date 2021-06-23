using Business.Handlers.Arppus.Commands;
using FluentValidation;

namespace Business.Handlers.Arppus.ValidationRules
{
    public class CreateArppuValidator : AbstractValidator<CreateArppuCommand>
    {
        public CreateArppuValidator()
        {
            RuleFor(x => x.DateTime).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
            RuleFor(x => x.TotalIncome).NotNull();
            RuleFor(x => x.TotalIncomePlayer).NotNull();
        }
    }

    public class UpdateArppuValidator : AbstractValidator<UpdateArppuCommand>
    {
        public UpdateArppuValidator()
        {
            RuleFor(x => x.DateTime).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
            RuleFor(x => x.TotalIncome).NotNull();
            RuleFor(x => x.TotalIncomePlayer).NotNull();
        }
    }
}