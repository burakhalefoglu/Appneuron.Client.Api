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

namespace Business.Handlers.PlayerCountWithDifficulties.Queries
{
    public class GetPlayersOnDifficultyLevelsByProjectIdQuery : IRequest<IDataResult<IEnumerable<PlayerCountWithDifficulty>>>
    {
        public string ProjectId { get; set; }

        public class GetPlayersOnDifficultyLevelsByProjectIdQueryHandler : IRequestHandler<GetPlayersOnDifficultyLevelsByProjectIdQuery, IDataResult<IEnumerable<PlayerCountWithDifficulty>>>
        {
            private readonly IPlayerCountOnDifficultyLevelRepository _playerCountOnDifficultyLevelRepository;
            private readonly IMediator _mediator;

            public GetPlayersOnDifficultyLevelsByProjectIdQueryHandler(IPlayerCountOnDifficultyLevelRepository playerCountOnDifficultyLevelRepository, IMediator mediator)
            {
                _playerCountOnDifficultyLevelRepository = playerCountOnDifficultyLevelRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<PlayerCountWithDifficulty>>> Handle(GetPlayersOnDifficultyLevelsByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<PlayerCountWithDifficulty>>(await _playerCountOnDifficultyLevelRepository.GetListAsync(p => p.ProjectId == request.ProjectId));
            }
        }
    }
}