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

namespace Business.Handlers.PlayerCountsOnLevels.Queries
{
    public class GetPlayersOnLevelsByProjectIdQuery : IRequest<IDataResult<IEnumerable<PlayerCountsOnLevel>>>
    {
        public string ProjectId { get; set; }

        public class GetPlayersOnLevelsByProjectIdQueryHandler : IRequestHandler<GetPlayersOnLevelsByProjectIdQuery, IDataResult<IEnumerable<PlayerCountsOnLevel>>>
        {
            private readonly IPlayerCountsOnLevelRepository _playerCountsOnLevelRepository;
            private readonly IMediator _mediator;

            public GetPlayersOnLevelsByProjectIdQueryHandler(IPlayerCountsOnLevelRepository playerCountsOnLevelRepository, IMediator mediator)
            {
                _playerCountsOnLevelRepository = playerCountsOnLevelRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<PlayerCountsOnLevel>>> Handle(GetPlayersOnLevelsByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<PlayerCountsOnLevel>>(await _playerCountsOnLevelRepository.GetListAsync(p => p.ProjectID == request.ProjectId));
            }
        }
    }
}