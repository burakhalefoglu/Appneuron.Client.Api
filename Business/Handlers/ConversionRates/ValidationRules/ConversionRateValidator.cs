using Business.Handlers.ConversionRates.Commands;
using FluentValidation;

namespace Business.Handlers.ConversionRates.ValidationRules
{
    public class CreateConversionRateValidator : AbstractValidator<CreateConversionRateCommand>
    {
        public CreateConversionRateValidator()
        {
            RuleFor(x => x.TotalPlayer).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
            RuleFor(x => x.PaidPlayer).NotNull();
            RuleFor(x => x.DateTime).NotNull();
        }
    }

    public class UpdateConversionRateValidator : AbstractValidator<UpdateConversionRateCommand>
    {
        public UpdateConversionRateValidator()
        {
            RuleFor(x => x.TotalPlayer).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
            RuleFor(x => x.PaidPlayer).NotNull();
            RuleFor(x => x.DateTime).NotNull();
        }
    }
}