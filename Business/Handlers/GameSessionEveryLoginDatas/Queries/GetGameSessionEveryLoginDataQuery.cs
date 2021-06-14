
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using MongoDB.Bson;

namespace Business.Handlers.GameSessionEveryLoginDatas.Queries
{
    public class GetGameSessionEveryLoginDataQuery : IRequest<IDataResult<GameSessionEveryLoginData>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetGameSessionEveryLoginDataQueryHandler : IRequestHandler<GetGameSessionEveryLoginDataQuery, IDataResult<GameSessionEveryLoginData>>
        {
            private readonly IGameSessionEveryLoginDataRepository _gameSessionEveryLoginDataRepository;
            private readonly IMediator _mediator;

            public GetGameSessionEveryLoginDataQueryHandler(IGameSessionEveryLoginDataRepository gameSessionEveryLoginDataRepository, IMediator mediator)
            {
                _gameSessionEveryLoginDataRepository = gameSessionEveryLoginDataRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<GameSessionEveryLoginData>> Handle(GetGameSessionEveryLoginDataQuery request, CancellationToken cancellationToken)
            {
                var gameSessionEveryLoginData = await _gameSessionEveryLoginDataRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<GameSessionEveryLoginData>(gameSessionEveryLoginData);
            }
        }
    }
}
