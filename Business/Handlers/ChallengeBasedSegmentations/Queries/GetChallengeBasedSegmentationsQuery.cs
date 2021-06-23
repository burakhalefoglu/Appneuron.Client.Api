using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.ChartModels;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ChallengeBasedSegmentations.Queries
{
    public class GetChallengeBasedSegmentationsQuery : IRequest<IDataResult<IEnumerable<ChallengeBasedSegmentation>>>
    {
        public class GetChallengeBasedSegmentationsQueryHandler : IRequestHandler<GetChallengeBasedSegmentationsQuery, IDataResult<IEnumerable<ChallengeBasedSegmentation>>>
        {
            private readonly IChallengeBasedSegmentationRepository _challengeBasedSegmentationRepository;
            private readonly IMediator _mediator;

            public GetChallengeBasedSegmentationsQueryHandler(IChallengeBasedSegmentationRepository challengeBasedSegmentationRepository, IMediator mediator)
            {
                _challengeBasedSegmentationRepository = challengeBasedSegmentationRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ChallengeBasedSegmentation>>> Handle(GetChallengeBasedSegmentationsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ChallengeBasedSegmentation>>(await _challengeBasedSegmentationRepository.GetListAsync());
            }
        }
    }
}