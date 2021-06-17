
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
using Entities.Concrete.ChartModels;

namespace Business.Handlers.PlayersOnLevels.Queries
{
    public class GetPlayersOnLevelQuery : IRequest<IDataResult<ProjectBasePlayerCountsOnLevel>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetPlayersOnLevelQueryHandler : IRequestHandler<GetPlayersOnLevelQuery, IDataResult<ProjectBasePlayerCountsOnLevel>>
        {
            private readonly IPlayersOnLevelRepository _playersOnLevelRepository;
            private readonly IMediator _mediator;

            public GetPlayersOnLevelQueryHandler(IPlayersOnLevelRepository playersOnLevelRepository, IMediator mediator)
            {
                _playersOnLevelRepository = playersOnLevelRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ProjectBasePlayerCountsOnLevel>> Handle(GetPlayersOnLevelQuery request, CancellationToken cancellationToken)
            {
                var playersOnLevel = await _playersOnLevelRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<ProjectBasePlayerCountsOnLevel>(playersOnLevel);
            }
        }
    }
}
