using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.ChartModels;
using MediatR;
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PlayerCountsOnLevels.Queries
{
    public class GetPlayersOnLevelQuery : IRequest<IDataResult<PlayerCountsOnLevel>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetPlayersOnLevelQueryHandler : IRequestHandler<GetPlayersOnLevelQuery, IDataResult<PlayerCountsOnLevel>>
        {
            private readonly IPlayerCountsOnLevelRepository _playersOnLevelRepository;
            private readonly IMediator _mediator;

            public GetPlayersOnLevelQueryHandler(IPlayerCountsOnLevelRepository playersOnLevelRepository, IMediator mediator)
            {
                _playersOnLevelRepository = playersOnLevelRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<PlayerCountsOnLevel>> Handle(GetPlayersOnLevelQuery request, CancellationToken cancellationToken)
            {
                var playersOnLevel = await _playersOnLevelRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<PlayerCountsOnLevel>(playersOnLevel);
            }
        }
    }
}