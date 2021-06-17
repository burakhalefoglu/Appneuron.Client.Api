
using Business.Handlers.ProjectBaseAdvClicks.Commands;
using FluentValidation;

namespace Business.Handlers.ProjectBaseAdvClicks.ValidationRules
{

    public class CreateProjectBaseAdvClickValidator : AbstractValidator<CreateProjectBaseAdvClickCommand>
    {
        public CreateProjectBaseAdvClickValidator()
        {

        }
    }
    public class UpdateProjectBaseAdvClickValidator : AbstractValidator<UpdateProjectBaseAdvClickCommand>
    {
        public UpdateProjectBaseAdvClickValidator()
        {

        }
    }
}