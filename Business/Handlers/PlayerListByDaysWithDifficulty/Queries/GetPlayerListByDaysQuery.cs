
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

namespace Business.Handlers.PlayerListByDaysWithDifficulty.Queries
{

    public class GetPlayerListByDaysQuery : IRequest<IDataResult<IEnumerable<PlayerListByDayWithDifficulty>>>
    {
        public class GetPlayerListByDaysQueryHandler : IRequestHandler<GetPlayerListByDaysQuery, IDataResult<IEnumerable<PlayerListByDayWithDifficulty>>>
        {
            private readonly IPlayerListByDayWithDifficultyRepository _playerListByDayRepository;
            private readonly IMediator _mediator;

            public GetPlayerListByDaysQueryHandler(IPlayerListByDayWithDifficultyRepository playerListByDayRepository, IMediator mediator)
            {
                _playerListByDayRepository = playerListByDayRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<PlayerListByDayWithDifficulty>>> Handle(GetPlayerListByDaysQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<PlayerListByDayWithDifficulty>>(await _playerListByDayRepository.GetListAsync());
            }
        }
    }
}