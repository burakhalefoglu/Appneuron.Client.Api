
using Business.Handlers.Arppus.Commands;
using FluentValidation;

namespace Business.Handlers.Arppus.ValidationRules
{

    public class CreateArppuValidator : AbstractValidator<CreateArppuCommand>
    {
        public CreateArppuValidator()
        {

        }
    }
    public class UpdateArppuValidator : AbstractValidator<UpdateArppuCommand>
    {
        public UpdateArppuValidator()
        {

        }
    }
}