
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

namespace Business.Handlers.GameSessionEveryLoginDatas.Queries
{

    public class GetGameSessionEveryLoginDatasQuery : IRequest<IDataResult<IEnumerable<GameSessionEveryLoginData>>>
    {
        public class GetGameSessionEveryLoginDatasQueryHandler : IRequestHandler<GetGameSessionEveryLoginDatasQuery, IDataResult<IEnumerable<GameSessionEveryLoginData>>>
        {
            private readonly IGameSessionEveryLoginDataRepository _gameSessionEveryLoginDataRepository;
            private readonly IMediator _mediator;

            public GetGameSessionEveryLoginDatasQueryHandler(IGameSessionEveryLoginDataRepository gameSessionEveryLoginDataRepository, IMediator mediator)
            {
                _gameSessionEveryLoginDataRepository = gameSessionEveryLoginDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<GameSessionEveryLoginData>>> Handle(GetGameSessionEveryLoginDatasQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<GameSessionEveryLoginData>>(await _gameSessionEveryLoginDataRepository.GetListAsync());
            }
        }
    }
}