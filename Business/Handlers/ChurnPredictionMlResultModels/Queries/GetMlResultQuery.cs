using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.ChurnPredictionMlResultModels.Queries;

public class GetMlResultQuery : IRequest<IDataResult<float>>
{
    public long ProjectId { get; set; }

    public class GetMlResultQueryHandler : IRequestHandler<GetMlResultQuery, IDataResult<float>>
    {
        private readonly IChurnPredictionSuccessRateRepository _churnPredictionMlResultRepository;

        public GetMlResultQueryHandler(IChurnPredictionSuccessRateRepository churnPredictionMlResultRepository)
        {
            _churnPredictionMlResultRepository = churnPredictionMlResultRepository;
        }

        [LogAspect(typeof(ConsoleLogger))]
        [SecuredOperation(Priority = 1)]
        public async Task<IDataResult<float>> Handle(GetMlResultQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _churnPredictionMlResultRepository.GetAsync(
                c => c.ProjectId == request.ProjectId);
            return new SuccessDataResult<float>(
                result is null ? 0 : result.Value);
        }
    }
}