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
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.GameSessionEveryLoginDatas.Commands
{
    public class UpdateGameSessionEveryLoginDataCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ClientId { get; set; }
        public string ProjectID { get; set; }
        public string CustomerID { get; set; }
        public System.DateTime SessionStartTime { get; set; }
        public System.DateTime SessionFinishTime { get; set; }
        public float SessionTimeMinute { get; set; }

        public class UpdateGameSessionEveryLoginDataCommandHandler : IRequestHandler<UpdateGameSessionEveryLoginDataCommand, IResult>
        {
            private readonly IGameSessionEveryLoginDataRepository _gameSessionEveryLoginDataRepository;
            private readonly IMediator _mediator;

            public UpdateGameSessionEveryLoginDataCommandHandler(IGameSessionEveryLoginDataRepository gameSessionEveryLoginDataRepository, IMediator mediator)
            {
                _gameSessionEveryLoginDataRepository = gameSessionEveryLoginDataRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateGameSessionEveryLoginDataValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateGameSessionEveryLoginDataCommand request, CancellationToken cancellationToken)
            {
                var gameSessionEveryLoginData = new GameSessionEveryLoginData();
                gameSessionEveryLoginData.ClientId = request.ClientId;
                gameSessionEveryLoginData.ProjectID = request.ProjectID;
                gameSessionEveryLoginData.CustomerID = request.CustomerID;
                gameSessionEveryLoginData.SessionStartTime = request.SessionStartTime;
                gameSessionEveryLoginData.SessionFinishTime = request.SessionFinishTime;
                gameSessionEveryLoginData.SessionTimeMinute = request.SessionTimeMinute;

                await _gameSessionEveryLoginDataRepository.UpdateAsync(request.Id, gameSessionEveryLoginData);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}