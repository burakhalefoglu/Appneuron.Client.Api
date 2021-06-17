
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Business.Handlers.PlayersOnLevels.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeletePlayersOnLevelCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeletePlayersOnLevelCommandHandler : IRequestHandler<DeletePlayersOnLevelCommand, IResult>
        {
            private readonly IPlayersOnLevelRepository _playersOnLevelRepository;
            private readonly IMediator _mediator;

            public DeletePlayersOnLevelCommandHandler(IPlayersOnLevelRepository playersOnLevelRepository, IMediator mediator)
            {
                _playersOnLevelRepository = playersOnLevelRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeletePlayersOnLevelCommand request, CancellationToken cancellationToken)
            {


                await _playersOnLevelRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

