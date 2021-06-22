
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.ChallengeBasedSegmentations.ValidationRules;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;

namespace Business.Handlers.ChallengeBasedSegmentations.Commands
{


    public class UpdateChallengeBasedSegmentationCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public long CompetitiveClientCount { get; set; }
        public long NonCompetitiveClientCount { get; set; }

        public class UpdateChallengeBasedSegmentationCommandHandler : IRequestHandler<UpdateChallengeBasedSegmentationCommand, IResult>
        {
            private readonly IChallengeBasedSegmentationRepository _challengeBasedSegmentationRepository;
            private readonly IMediator _mediator;

            public UpdateChallengeBasedSegmentationCommandHandler(IChallengeBasedSegmentationRepository challengeBasedSegmentationRepository, IMediator mediator)
            {
                _challengeBasedSegmentationRepository = challengeBasedSegmentationRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateChallengeBasedSegmentationValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateChallengeBasedSegmentationCommand request, CancellationToken cancellationToken)
            {



                var challengeBasedSegmentation = new ChallengeBasedSegmentation();
                challengeBasedSegmentation.ProjectId = request.ProjectId;
                challengeBasedSegmentation.CompetitiveClientCount = request.CompetitiveClientCount;
                challengeBasedSegmentation.NonCompetitiveClientCount = request.NonCompetitiveClientCount;


                await _challengeBasedSegmentationRepository.UpdateAsync(request.Id, challengeBasedSegmentation);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

