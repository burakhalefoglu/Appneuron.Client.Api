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

namespace Business.Handlers.PlayerCountWithDifficulties.Queries
{
    public class GetPlayersOnDifficultyLevelQuery : IRequest<IDataResult<PlayerCountWithDifficulty>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetPlayersOnDifficultyLevelQueryHandler : IRequestHandler<GetPlayersOnDifficultyLevelQuery, IDataResult<PlayerCountWithDifficulty>>
        {
            private readonly IPlayerCountOnDifficultyLevelRepository _playersOnDifficultyLevelRepository;
            private readonly IMediator _mediator;

            public GetPlayersOnDifficultyLevelQueryHandler(IPlayerCountOnDifficultyLevelRepository playersOnDifficultyLevelRepository, IMediator mediator)
            {
                _playersOnDifficultyLevelRepository = playersOnDifficultyLevelRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<PlayerCountWithDifficulty>> Handle(GetPlayersOnDifficultyLevelQuery request, CancellationToken cancellationToken)
            {
                var playersOnDifficultyLevel = await _playersOnDifficultyLevelRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<PlayerCountWithDifficulty>(playersOnDifficultyLevel);
            }
        }
    }
}