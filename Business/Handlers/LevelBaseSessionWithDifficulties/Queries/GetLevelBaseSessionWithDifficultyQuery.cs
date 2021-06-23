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

namespace Business.Handlers.LevelBaseSessionWithDifficulties.Queries
{
    public class GetLevelBaseSessionWithDifficultyQuery : IRequest<IDataResult<LevelBaseSessionWithDifficulty>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetLevelBaseSessionWithDifficultyQueryHandler : IRequestHandler<GetLevelBaseSessionWithDifficultyQuery, IDataResult<LevelBaseSessionWithDifficulty>>
        {
            private readonly ILevelBaseSessionWithDifficultyRepository _levelBaseSessionWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetLevelBaseSessionWithDifficultyQueryHandler(ILevelBaseSessionWithDifficultyRepository levelBaseSessionWithDifficultyRepository, IMediator mediator)
            {
                _levelBaseSessionWithDifficultyRepository = levelBaseSessionWithDifficultyRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<LevelBaseSessionWithDifficulty>> Handle(GetLevelBaseSessionWithDifficultyQuery request, CancellationToken cancellationToken)
            {
                var levelBaseSessionWithDifficulty = await _levelBaseSessionWithDifficultyRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<LevelBaseSessionWithDifficulty>(levelBaseSessionWithDifficulty);
            }
        }
    }
}