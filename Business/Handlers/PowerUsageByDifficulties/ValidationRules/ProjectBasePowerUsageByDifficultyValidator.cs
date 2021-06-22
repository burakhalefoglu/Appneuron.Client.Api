
using Business.Handlers.PowerUsageByDifficulties.Commands;
using FluentValidation;

namespace Business.Handlers.PowerUsageByDifficulties.ValidationRules
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