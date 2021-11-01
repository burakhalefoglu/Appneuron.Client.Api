
using Business.Handlers.Clients.Commands;
using FluentValidation;

namespace Business.Handlers.Clients.ValidationRules
{
    public class CreateClientValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientValidator()
        {
            RuleFor(x => x.ClientId).NotEmpty();
            RuleFor(x => x.ProjectKey).NotEmpty();
            RuleFor(x => x.IsPaidClient).NotEmpty();
            RuleFor(x => x.CreatedAt).NotEmpty();

        }
    }
}