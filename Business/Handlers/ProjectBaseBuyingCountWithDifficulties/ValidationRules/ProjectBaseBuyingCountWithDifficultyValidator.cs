
using Business.Handlers.ProjectBaseBuyingCountWithDifficulties.Commands;
using FluentValidation;

namespace Business.Handlers.ProjectBaseBuyingCountWithDifficulties.ValidationRules
{

    public class CreateProjectBaseBuyingCountWithDifficultyValidator : AbstractValidator<CreateProjectBaseBuyingCountWithDifficultyCommand>
    {
        public CreateProjectBaseBuyingCountWithDifficultyValidator()
        {

        }
    }
    public class UpdateProjectBaseBuyingCountWithDifficultyValidator : AbstractValidator<UpdateProjectBaseBuyingCountWithDifficultyCommand>
    {
        public UpdateProjectBaseBuyingCountWithDifficultyValidator()
        {

        }
    }
}