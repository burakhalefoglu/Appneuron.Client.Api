
using Business.Handlers.Tests.Commands;
using FluentValidation;

namespace Business.Handlers.Tests.ValidationRules
{

    public class CreateTestValidator : AbstractValidator<CreateTestCommand>
    {
        public CreateTestValidator()
        {

        }
    }
    public class UpdateTestValidator : AbstractValidator<UpdateTestCommand>
    {
        public UpdateTestValidator()
        {

        }
    }
}