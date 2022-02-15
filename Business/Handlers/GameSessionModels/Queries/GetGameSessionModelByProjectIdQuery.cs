using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.GameSessionModels.Queries
{
    public class
        GetGameSessionModelByProjectIdQuery : IRequest<IDataResult<IEnumerable<GameSessionModel>>>
    {
        public long ProjectId { get; set; }

        public class GetGameSessionModelByProjectIdQueryHandler : IRequestHandler<
            GetGameSessionModelByProjectIdQuery, IDataResult<IEnumerable<GameSessionModel>>>
        {
            private readonly IGameSessionModelRepository _gameSessionEveryLoginDataRepository;
            private readonly IMediator _mediator;

            public GetGameSessionModelByProjectIdQueryHandler(
                IGameSessionModelRepository gameSessionEveryLoginDataRepository, IMediator mediator)
            {
                _gameSessionEveryLoginDataRepository = gameSessionEveryLoginDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<GameSessionModel>>> Handle(
                GetGameSessionModelByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<GameSessionModel>>
                (await _gameSessionEveryLoginDataRepository.GetListAsync(p =>
                    p.ProjectId == request.ProjectId && p.Status == true));
            }
        }
    }
}