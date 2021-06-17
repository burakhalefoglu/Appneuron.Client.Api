
using Business.Handlers.ProjectBasePowerUsageByDifficulties.Commands;
using FluentValidation;

namespace Business.Handlers.ProjectBasePowerUsageByDifficulties.ValidationRules
{

    public class CreateProjectBasePowerUsageByDifficultyValidator : AbstractValidator<CreateProjectBasePowerUsageByDifficultyCommand>
    {
        public CreateProjectBasePowerUsageByDifficultyValidator()
        {

        }
    }
    public class UpdateProjectBasePowerUsageByDifficultyValidator : AbstractValidator<UpdateProjectBasePowerUsageByDifficultyCommand>
    {
        public UpdateProjectBasePowerUsageByDifficultyValidator()
        {

        }
    }
}