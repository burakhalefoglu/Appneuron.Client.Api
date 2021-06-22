
using Business.Handlers.ProjectBaseDieCountWithLevels.Commands;
using FluentValidation;

namespace Business.Handlers.ProjectBaseDieCountWithLevels.ValidationRules
{

    public class CreateProjectBaseDieCountWithLevelValidator : AbstractValidator<CreateProjectBaseDieCountWithLevelCommand>
    {
        public CreateProjectBaseDieCountWithLevelValidator()
        {

        }
    }
    public class UpdateProjectBaseDieCountWithLevelValidator : AbstractValidator<UpdateProjectBaseDieCountWithLevelCommand>
    {
        public UpdateProjectBaseDieCountWithLevelValidator()
        {

        }
    }
}