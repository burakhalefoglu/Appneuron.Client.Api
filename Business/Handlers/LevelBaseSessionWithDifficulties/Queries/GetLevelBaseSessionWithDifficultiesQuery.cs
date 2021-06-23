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

namespace Business.Handlers.LevelBaseSessionWithDifficulties.Queries
{
    public class GetLevelBaseSessionWithDifficultiesQuery : IRequest<IDataResult<IEnumerable<LevelBaseSessionWithDifficulty>>>
    {
        public class GetLevelBaseSessionWithDifficultiesQueryHandler : IRequestHandler<GetLevelBaseSessionWithDifficultiesQuery, IDataResult<IEnumerable<LevelBaseSessionWithDifficulty>>>
        {
            private readonly ILevelBaseSessionWithDifficultyRepository _levelBaseSessionWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetLevelBaseSessionWithDifficultiesQueryHandler(ILevelBaseSessionWithDifficultyRepository levelBaseSessionWithDifficultyRepository, IMediator mediator)
            {
                _levelBaseSessionWithDifficultyRepository = levelBaseSessionWithDifficultyRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<LevelBaseSessionWithDifficulty>>> Handle(GetLevelBaseSessionWithDifficultiesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<LevelBaseSessionWithDifficulty>>(await _levelBaseSessionWithDifficultyRepository.GetListAsync());
            }
        }
    }
}