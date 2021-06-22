
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.ChallengeBasedSegmentations.ValidationRules;
using Entities.Concrete.ChartModels;

namespace Business.Handlers.ChallengeBasedSegmentations.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateChallengeBasedSegmentationCommand : IRequest<IResult>
    {

        public string ProjectId { get; set; }
        public long CompetitiveClientCount { get; set; }
        public long NonCompetitiveClientCount { get; set; }


        public class CreateChallengeBasedSegmentationCommandHandler : IRequestHandler<CreateChallengeBasedSegmentationCommand, IResult>
        {
            private readonly IChallengeBasedSegmentationRepository _challengeBasedSegmentationRepository;
            private readonly IMediator _mediator;
            public CreateChallengeBasedSegmentationCommandHandler(IChallengeBasedSegmentationRepository challengeBasedSegmentationRepository, IMediator mediator)
            {
                _challengeBasedSegmentationRepository = challengeBasedSegmentationRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateChallengeBasedSegmentationValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateChallengeBasedSegmentationCommand request, CancellationToken cancellationToken)
            {
                var isThereChallengeBasedSegmentationRecord = _challengeBasedSegmentationRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereChallengeBasedSegmentationRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedChallengeBasedSegmentation = new ChallengeBasedSegmentation
                {
                    ProjectId = request.ProjectId,
                    CompetitiveClientCount = request.CompetitiveClientCount,
                    NonCompetitiveClientCount = request.NonCompetitiveClientCount,

                };

                await _challengeBasedSegmentationRepository.AddAsync(addedChallengeBasedSegmentation);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}