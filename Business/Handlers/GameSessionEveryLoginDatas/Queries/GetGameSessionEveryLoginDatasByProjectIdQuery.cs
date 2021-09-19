using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.GameSessionEveryLoginDatas.Queries
{
    public class GetGameSessionEveryLoginDatasByProjectIdQuery : IRequest<IDataResult<IEnumerable<GameSessionEveryLoginData>>>
    {
        public string ProjectID { get; set; }

        public class GetGameSessionEveryLoginDatasByProjectIdQueryHandler : IRequestHandler<GetGameSessionEveryLoginDatasByProjectIdQuery, IDataResult<IEnumerable<GameSessionEveryLoginData>>>
        {
            private readonly IGameSessionEveryLoginDataRepository _gameSessionEveryLoginDataRepository;
            private readonly IMediator _mediator;

            public GetGameSessionEveryLoginDatasByProjectIdQueryHandler(IGameSessionEveryLoginDataRepository gameSessionEveryLoginDataRepository, IMediator mediator)
            {
                _gameSessionEveryLoginDataRepository = gameSessionEveryLoginDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<GameSessionEveryLoginData>>> Handle(GetGameSessionEveryLoginDatasByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<GameSessionEveryLoginData>>
                    (await _gameSessionEveryLoginDataRepository.GetListAsync(p=>p.ProjectID == request.ProjectID));
            }
        }
    }
}