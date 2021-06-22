
using Business.BusinessAspects;
using Core.Utilities.Results;
using Core.Aspects.Autofac.Performance;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Caching;
using Entities.Concrete.ChartModels;

namespace Business.Handlers.ChallengeBasedSegmentations.Queries
{

    public class GetChallengeBasedSegmentationsByProjectIdQuery : IRequest<IDataResult<IEnumerable<ChallengeBasedSegmentation>>>
    {
        public string ProjectId { get; set; }

        public class GetChallengeBasedSegmentationsByProjectIdQueryHandler : IRequestHandler<GetChallengeBasedSegmentationsByProjectIdQuery, IDataResult<IEnumerable<ChallengeBasedSegmentation>>>
        {
            private readonly IChallengeBasedSegmentationRepository _challengeBasedSegmentationRepository;
            private readonly IMediator _mediator;

            public GetChallengeBasedSegmentationsByProjectIdQueryHandler(IChallengeBasedSegmentationRepository challengeBasedSegmentationRepository, IMediator mediator)
            {
                _challengeBasedSegmentationRepository = challengeBasedSegmentationRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ChallengeBasedSegmentation>>> Handle(GetChallengeBasedSegmentationsByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ChallengeBasedSegmentation>>(await _challengeBasedSegmentationRepository.GetListAsync(p => p.ProjectId == request.ProjectId));
            }
        }
    }
}