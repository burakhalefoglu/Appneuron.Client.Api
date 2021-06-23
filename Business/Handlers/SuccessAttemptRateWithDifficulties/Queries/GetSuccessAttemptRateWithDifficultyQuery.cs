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

namespace Business.Handlers.SuccessAttemptRateWithDifficulties.Queries
{
    public class GetSuccessAttemptRateWithDifficultyQuery : IRequest<IDataResult<SuccessAttemptRateWithDifficulty>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetSuccessAttemptRateWithDifficultyQueryHandler : IRequestHandler<GetSuccessAttemptRateWithDifficultyQuery, IDataResult<SuccessAttemptRateWithDifficulty>>
        {
            private readonly ISuccessAttemptRateWithDifficultyRepository _successAttemptRateWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetSuccessAttemptRateWithDifficultyQueryHandler(ISuccessAttemptRateWithDifficultyRepository successAttemptRateWithDifficultyRepository, IMediator mediator)
            {
                _successAttemptRateWithDifficultyRepository = successAttemptRateWithDifficultyRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<SuccessAttemptRateWithDifficulty>> Handle(GetSuccessAttemptRateWithDifficultyQuery request, CancellationToken cancellationToken)
            {
                var successAttemptRateWithDifficulty = await _successAttemptRateWithDifficultyRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<SuccessAttemptRateWithDifficulty>(successAttemptRateWithDifficulty);
            }
        }
    }
}