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

namespace Business.Handlers.LevelBasePlayerCountWithDifficulties.Queries
{
    public class GetLevelBasePlayerCountWithDifficultiesQuery : IRequest<IDataResult<IEnumerable<LevelBasePlayerCountWithDifficulty>>>
    {
        public class GetLevelBasePlayerCountWithDifficultiesQueryHandler : IRequestHandler<GetLevelBasePlayerCountWithDifficultiesQuery, IDataResult<IEnumerable<LevelBasePlayerCountWithDifficulty>>>
        {
            private readonly ILevelBasePlayerCountWithDifficultyRepository _levelBasePlayerCountWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetLevelBasePlayerCountWithDifficultiesQueryHandler(ILevelBasePlayerCountWithDifficultyRepository levelBasePlayerCountWithDifficultyRepository, IMediator mediator)
            {
                _levelBasePlayerCountWithDifficultyRepository = levelBasePlayerCountWithDifficultyRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<LevelBasePlayerCountWithDifficulty>>> Handle(GetLevelBasePlayerCountWithDifficultiesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<LevelBasePlayerCountWithDifficulty>>(await _levelBasePlayerCountWithDifficultyRepository.GetListAsync());
            }
        }
    }
}