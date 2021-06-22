
using Business.Handlers.ProjectBaseDailySessions.Commands;
using FluentValidation;

namespace Business.Handlers.ProjectBaseDailySessions.ValidationRules
{

    public class CreateProjectBaseDailySessionValidator : AbstractValidator<CreateProjectBaseDailySessionCommand>
    {
        public CreateProjectBaseDailySessionValidator()
        {

        }
    }
    public class UpdateProjectBaseDailySessionValidator : AbstractValidator<UpdateProjectBaseDailySessionCommand>
    {
        public UpdateProjectBaseDailySessionValidator()
        {

        }
    }
}