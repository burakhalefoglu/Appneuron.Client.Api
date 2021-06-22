
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

namespace Business.Handlers.PlayerListByDaysWithDifficulty.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeletePlayerListByDayCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeletePlayerListByDayCommandHandler : IRequestHandler<DeletePlayerListByDayCommand, IResult>
        {
            private readonly IPlayerListByDayWithDifficultyRepository _playerListByDayRepository;
            private readonly IMediator _mediator;

            public DeletePlayerListByDayCommandHandler(IPlayerListByDayWithDifficultyRepository playerListByDayRepository, IMediator mediator)
            {
                _playerListByDayRepository = playerListByDayRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeletePlayerListByDayCommand request, CancellationToken cancellationToken)
            {


                await _playerListByDayRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

