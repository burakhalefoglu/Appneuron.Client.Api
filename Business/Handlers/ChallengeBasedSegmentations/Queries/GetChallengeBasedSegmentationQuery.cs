using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.ChartModels;
using MediatR;
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ChallengeBasedSegmentations.Queries
{
    public class GetChallengeBasedSegmentationQuery : IRequest<IDataResult<ChallengeBasedSegmentation>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetChallengeBasedSegmentationQueryHandler : IRequestHandler<GetChallengeBasedSegmentationQuery, IDataResult<ChallengeBasedSegmentation>>
        {
            private readonly IChallengeBasedSegmentationRepository _challengeBasedSegmentationRepository;
            private readonly IMediator _mediator;

            public GetChallengeBasedSegmentationQueryHandler(IChallengeBasedSegmentationRepository challengeBasedSegmentationRepository, IMediator mediator)
            {
                _challengeBasedSegmentationRepository = challengeBasedSegmentationRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ChallengeBasedSegmentation>> Handle(GetChallengeBasedSegmentationQuery request, CancellationToken cancellationToken)
            {
                var challengeBasedSegmentation = await _challengeBasedSegmentationRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<ChallengeBasedSegmentation>(challengeBasedSegmentation);
            }
        }
    }
}