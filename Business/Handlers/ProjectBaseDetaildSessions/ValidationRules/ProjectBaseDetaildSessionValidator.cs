
using Business.Handlers.ProjectBaseDetaildSessions.Commands;
using FluentValidation;

namespace Business.Handlers.ProjectBaseDetaildSessions.ValidationRules
{

    public class CreateProjectBaseDetaildSessionValidator : AbstractValidator<CreateProjectBaseDetaildSessionCommand>
    {
        public CreateProjectBaseDetaildSessionValidator()
        {

        }
    }
    public class UpdateProjectBaseDetaildSessionValidator : AbstractValidator<UpdateProjectBaseDetaildSessionCommand>
    {
        public UpdateProjectBaseDetaildSessionValidator()
        {

        }
    }
}