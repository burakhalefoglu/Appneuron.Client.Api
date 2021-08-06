using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers.ApacheKafka;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.GameSessionEveryLoginDatas.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class DeleteGameSessionEveryLoginDataByProjectIdCommand : IRequest<IResult>
    {
        public string ProjectID { get; set; }

        public class DeleteGameSessionEveryLoginDataByProjectIdCommandHandler : IRequestHandler<DeleteGameSessionEveryLoginDataByProjectIdCommand, IResult>
        {
            private readonly IGameSessionEveryLoginDataRepository _gameSessionEveryLoginDataRepository;
            private readonly IMediator _mediator;

            public DeleteGameSessionEveryLoginDataByProjectIdCommandHandler(IGameSessionEveryLoginDataRepository gameSessionEveryLoginDataRepository, IMediator mediator)
            {
                _gameSessionEveryLoginDataRepository = gameSessionEveryLoginDataRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(ApacheKafkaDatabaseActionLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteGameSessionEveryLoginDataByProjectIdCommand request, CancellationToken cancellationToken)
            {
                await _gameSessionEveryLoginDataRepository.DeleteAsync(p=>p.ProjectID == request.ProjectID);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}