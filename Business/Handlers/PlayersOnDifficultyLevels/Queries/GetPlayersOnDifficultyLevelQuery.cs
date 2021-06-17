
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

namespace Business.Handlers.PlayersOnDifficultyLevels.Queries
{
    public class GetPlayersOnDifficultyLevelQuery : IRequest<IDataResult<ProjectBasePlayerCountOnDifficultyLevel>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetPlayersOnDifficultyLevelQueryHandler : IRequestHandler<GetPlayersOnDifficultyLevelQuery, IDataResult<ProjectBasePlayerCountOnDifficultyLevel>>
        {
            private readonly IPlayersOnDifficultyLevelRepository _playersOnDifficultyLevelRepository;
            private readonly IMediator _mediator;

            public GetPlayersOnDifficultyLevelQueryHandler(IPlayersOnDifficultyLevelRepository playersOnDifficultyLevelRepository, IMediator mediator)
            {
                _playersOnDifficultyLevelRepository = playersOnDifficultyLevelRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ProjectBasePlayerCountOnDifficultyLevel>> Handle(GetPlayersOnDifficultyLevelQuery request, CancellationToken cancellationToken)
            {
                var playersOnDifficultyLevel = await _playersOnDifficultyLevelRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<ProjectBasePlayerCountOnDifficultyLevel>(playersOnDifficultyLevel);
            }
        }
    }
}
