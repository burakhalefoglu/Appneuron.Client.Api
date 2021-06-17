
using Business.Handlers.ProjectBaseTotalDieWithDifficulties.Commands;
using FluentValidation;

namespace Business.Handlers.ProjectBaseTotalDieWithDifficulties.ValidationRules
{

    public class CreateProjectBaseTotalDieWithDifficultyValidator : AbstractValidator<CreateProjectBaseTotalDieWithDifficultyCommand>
    {
        public CreateProjectBaseTotalDieWithDifficultyValidator()
        {

        }
    }
    public class UpdateProjectBaseTotalDieWithDifficultyValidator : AbstractValidator<UpdateProjectBaseTotalDieWithDifficultyCommand>
    {
        public UpdateProjectBaseTotalDieWithDifficultyValidator()
        {

        }
    }
}