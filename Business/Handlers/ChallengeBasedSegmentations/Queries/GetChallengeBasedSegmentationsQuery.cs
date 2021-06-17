﻿
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;
using Entities.Concrete.ChartModels;

namespace Business.Handlers.ChallengeBasedSegmentations.Queries
{

    public class GetChallengeBasedSegmentationsQuery : IRequest<IDataResult<IEnumerable<ProjectAndChallengeBasedSegmentation>>>
    {
        public class GetChallengeBasedSegmentationsQueryHandler : IRequestHandler<GetChallengeBasedSegmentationsQuery, IDataResult<IEnumerable<ProjectAndChallengeBasedSegmentation>>>
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
            public async Task<IDataResult<IEnumerable<ProjectAndChallengeBasedSegmentation>>> Handle(GetChallengeBasedSegmentationsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ProjectAndChallengeBasedSegmentation>>(await _challengeBasedSegmentationRepository.GetListAsync());
            }
        }
    }
}