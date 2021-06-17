
using Business.Handlers.Revenues.Commands;
using FluentValidation;

namespace Business.Handlers.Revenues.ValidationRules
{

    public class CreateRevenueValidator : AbstractValidator<CreateRevenueCommand>
    {
        public CreateRevenueValidator()
        {

        }
    }
    public class UpdateRevenueValidator : AbstractValidator<UpdateRevenueCommand>
    {
        public UpdateRevenueValidator()
        {

        }
    }
}