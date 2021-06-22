
using Business.Handlers.ConversionRates.Commands;
using FluentValidation;

namespace Business.Handlers.ConversionRates.ValidationRules
{

    public class CreateConversionRateValidator : AbstractValidator<CreateConversionRateCommand>
    {
        public CreateConversionRateValidator()
        {

        }
    }
    public class UpdateConversionRateValidator : AbstractValidator<UpdateConversionRateCommand>
    {
        public UpdateConversionRateValidator()
        {

        }
    }
}