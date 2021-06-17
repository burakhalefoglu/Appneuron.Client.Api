
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;

namespace Business.Handlers.ChallengeBasedSegmentations.Queries
{
    public class GetChallengeBasedSegmentationQuery : IRequest<IDataResult<ProjectAndChallengeBasedSegmentation>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetChallengeBasedSegmentationQueryHandler : IRequestHandler<GetChallengeBasedSegmentationQuery, IDataResult<ProjectAndChallengeBasedSegmentation>>
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
            public async Task<IDataResult<ProjectAndChallengeBasedSegmentation>> Handle(GetChallengeBasedSegmentationQuery request, CancellationToken cancellationToken)
            {
                var challengeBasedSegmentation = await _challengeBasedSegmentationRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<ProjectAndChallengeBasedSegmentation>(challengeBasedSegmentation);
            }
        }
    }
}
