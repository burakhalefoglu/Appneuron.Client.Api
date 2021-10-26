
using Business.Handlers.ChurnClientPredictionResults.Commands;
using FluentValidation;

namespace Business.Handlers.ChurnClientPredictionResults.ValidationRules
{

    public class CreateChurnClientPredictionResultValidator : AbstractValidator<CreateChurnClientPredictionResultCommand>
    {
        public CreateChurnClientPredictionResultValidator()
        {
            RuleFor(x => x.ProjectId).NotEmpty();
            RuleFor(x => x.ChurnPredictionDate).NotEmpty();

        }
    }
    public class UpdateChurnClientPredictionResultValidator : AbstractValidator<UpdateChurnClientPredictionResultCommand>
    {
        public UpdateChurnClientPredictionResultValidator()
        {
            RuleFor(x => x.ProjectId).NotEmpty();
            RuleFor(x => x.ChurnPredictionDate).NotEmpty();

        }
    }
}