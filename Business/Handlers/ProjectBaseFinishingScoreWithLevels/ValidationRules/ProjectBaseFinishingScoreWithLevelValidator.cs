
using Business.Handlers.ProjectBaseFinishingScoreWithLevels.Commands;
using FluentValidation;

namespace Business.Handlers.ProjectBaseFinishingScoreWithLevels.ValidationRules
{

    public class CreateProjectBaseFinishingScoreWithLevelValidator : AbstractValidator<CreateProjectBaseFinishingScoreWithLevelCommand>
    {
        public CreateProjectBaseFinishingScoreWithLevelValidator()
        {

        }
    }
    public class UpdateProjectBaseFinishingScoreWithLevelValidator : AbstractValidator<UpdateProjectBaseFinishingScoreWithLevelCommand>
    {
        public UpdateProjectBaseFinishingScoreWithLevelValidator()
        {

        }
    }
}