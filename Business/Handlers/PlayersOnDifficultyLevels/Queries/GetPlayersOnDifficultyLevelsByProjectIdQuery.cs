
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

namespace Business.Handlers.PlayerCountOnDifficultyLevels.Queries
{

    public class GetPlayersOnDifficultyLevelsByProjectIdQuery : IRequest<IDataResult<IEnumerable<PlayerCountOnDifficultyLevel>>>
    {
        public string ProjectId { get; set; }
        public class GetPlayersOnDifficultyLevelsByProjectIdQueryHandler : IRequestHandler<GetPlayersOnDifficultyLevelsByProjectIdQuery, IDataResult<IEnumerable<PlayerCountOnDifficultyLevel>>>
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
            public async Task<IDataResult<IEnumerable<PlayerCountOnDifficultyLevel>>> Handle(GetPlayersOnDifficultyLevelsByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<PlayerCountOnDifficultyLevel>>(await _playerCountOnDifficultyLevelRepository.GetListAsync(p => p.ProjectId == request.ProjectId));
            }
        }
    }
}