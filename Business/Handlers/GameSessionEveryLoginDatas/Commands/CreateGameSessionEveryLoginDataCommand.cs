using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.GameSessionEveryLoginDatas.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.GameSessionEveryLoginDatas.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CreateGameSessionEveryLoginDataCommand : IRequest<IResult>
    {
        public string ClientId { get; set; }
        public string ProjectID { get; set; }
        public string CustomerID { get; set; }
        public System.DateTime SessionStartTime { get; set; }
        public System.DateTime SessionFinishTime { get; set; }
        public float SessionTimeMinute { get; set; }

        public class CreateGameSessionEveryLoginDataCommandHandler : IRequestHandler<CreateGameSessionEveryLoginDataCommand, IResult>
        {
            private readonly IGameSessionEveryLoginDataRepository _gameSessionEveryLoginDataRepository;
            private readonly IMediator _mediator;

            public CreateGameSessionEveryLoginDataCommandHandler(IGameSessionEveryLoginDataRepository gameSessionEveryLoginDataRepository, IMediator mediator)
            {
                _gameSessionEveryLoginDataRepository = gameSessionEveryLoginDataRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateGameSessionEveryLoginDataValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateGameSessionEveryLoginDataCommand request, CancellationToken cancellationToken)
            {
                var addedGameSessionEveryLoginData = new GameSessionEveryLoginData
                {
                    ClientId = request.ClientId,
                    ProjectID = request.ProjectID,
                    CustomerID = request.CustomerID,
                    SessionStartTime = request.SessionStartTime,
                    SessionFinishTime = request.SessionFinishTime,
                    SessionTimeMinute = request.SessionTimeMinute,
                };

                await _gameSessionEveryLoginDataRepository.AddAsync(addedGameSessionEveryLoginData);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}