
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.ChurnClientPredictionResults.Queries
{

    public class GetChurnClientPredictionResultsQuery : IRequest<IDataResult<IEnumerable<ChurnClientPredictionResult>>>
    {
        public class GetChurnClientPredictionResultsQueryHandler : IRequestHandler<GetChurnClientPredictionResultsQuery, IDataResult<IEnumerable<ChurnClientPredictionResult>>>
        {
            private readonly IChurnClientPredictionResultRepository _churnClientPredictionResultRepository;
            private readonly IMediator _mediator;

            public GetChurnClientPredictionResultsQueryHandler(IChurnClientPredictionResultRepository churnClientPredictionResultRepository, IMediator mediator)
            {
                _churnClientPredictionResultRepository = churnClientPredictionResultRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ChurnClientPredictionResult>>> Handle(GetChurnClientPredictionResultsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ChurnClientPredictionResult>>(await _churnClientPredictionResultRepository.GetListAsync());
            }
        }
    }
}