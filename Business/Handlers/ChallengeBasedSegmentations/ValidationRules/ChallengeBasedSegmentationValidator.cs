using Business.Handlers.ChallengeBasedSegmentations.Commands;
using FluentValidation;

namespace Business.Handlers.ChallengeBasedSegmentations.ValidationRules
{
    public class CreateChallengeBasedSegmentationValidator : AbstractValidator<CreateChallengeBasedSegmentationCommand>
    {
        public CreateChallengeBasedSegmentationValidator()
        {
            RuleFor(x => x.CompetitiveClientCount).NotEmpty();
            RuleFor(x => x.NonCompetitiveClientCount).NotEmpty();
        }
    }

    public class UpdateChallengeBasedSegmentationValidator : AbstractValidator<UpdateChallengeBasedSegmentationCommand>
    {
        public UpdateChallengeBasedSegmentationValidator()
        {
            RuleFor(x => x.CompetitiveClientCount).NotEmpty();
            RuleFor(x => x.NonCompetitiveClientCount).NotEmpty();
        }
    }
}