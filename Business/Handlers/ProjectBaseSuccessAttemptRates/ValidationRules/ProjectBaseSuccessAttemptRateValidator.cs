
using Business.Handlers.ProjectBaseSuccessAttemptRates.Commands;
using FluentValidation;

namespace Business.Handlers.ProjectBaseSuccessAttemptRates.ValidationRules
{

    public class CreateProjectBaseSuccessAttemptRateValidator : AbstractValidator<CreateProjectBaseSuccessAttemptRateCommand>
    {
        public CreateProjectBaseSuccessAttemptRateValidator()
        {

        }
    }
    public class UpdateProjectBaseSuccessAttemptRateValidator : AbstractValidator<UpdateProjectBaseSuccessAttemptRateCommand>
    {
        public UpdateProjectBaseSuccessAttemptRateValidator()
        {

        }
    }
}