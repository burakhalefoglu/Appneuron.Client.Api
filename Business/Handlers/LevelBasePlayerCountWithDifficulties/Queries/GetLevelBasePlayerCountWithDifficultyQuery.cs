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

namespace Business.Handlers.LevelBasePlayerCountWithDifficulties.Queries
{
    public class GetLevelBasePlayerCountWithDifficultyQuery : IRequest<IDataResult<LevelBasePlayerCountWithDifficulty>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetLevelBasePlayerCountWithDifficultyQueryHandler : IRequestHandler<GetLevelBasePlayerCountWithDifficultyQuery, IDataResult<LevelBasePlayerCountWithDifficulty>>
        {
            private readonly ILevelBasePlayerCountWithDifficultyRepository _levelBasePlayerCountWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetLevelBasePlayerCountWithDifficultyQueryHandler(ILevelBasePlayerCountWithDifficultyRepository levelBasePlayerCountWithDifficultyRepository, IMediator mediator)
            {
                _levelBasePlayerCountWithDifficultyRepository = levelBasePlayerCountWithDifficultyRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<LevelBasePlayerCountWithDifficulty>> Handle(GetLevelBasePlayerCountWithDifficultyQuery request, CancellationToken cancellationToken)
            {
                var levelBasePlayerCountWithDifficulty = await _levelBasePlayerCountWithDifficultyRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<LevelBasePlayerCountWithDifficulty>(levelBasePlayerCountWithDifficulty);
            }
        }
    }
}