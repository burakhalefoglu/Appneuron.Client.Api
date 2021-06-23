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

namespace Business.Handlers.DailySessionWithDifficulties.Queries
{
    public class GetDailySessionWithDifficultyQuery : IRequest<IDataResult<DailySessionWithDifficulty>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetDailySessionWithDifficultyQueryHandler : IRequestHandler<GetDailySessionWithDifficultyQuery, IDataResult<DailySessionWithDifficulty>>
        {
            private readonly IDailySessionWithDifficultyRepository _dailySessionWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetDailySessionWithDifficultyQueryHandler(IDailySessionWithDifficultyRepository dailySessionWithDifficultyRepository, IMediator mediator)
            {
                _dailySessionWithDifficultyRepository = dailySessionWithDifficultyRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<DailySessionWithDifficulty>> Handle(GetDailySessionWithDifficultyQuery request, CancellationToken cancellationToken)
            {
                var dailySessionWithDifficulty = await _dailySessionWithDifficultyRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<DailySessionWithDifficulty>(dailySessionWithDifficulty);
            }
        }
    }
}