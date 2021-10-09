
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

namespace Business.Handlers.MlResultModels.Queries
{

    public class GetMlResultModelsQuery : IRequest<IDataResult<IEnumerable<ChurnBlokerMlResult>>>
    {
        public class GetMlResultModelsQueryHandler : IRequestHandler<GetMlResultModelsQuery, IDataResult<IEnumerable<ChurnBlokerMlResult>>>
        {
            private readonly IMlResultRepository _mlResultModelRepository;
            private readonly IMediator _mediator;

            public GetMlResultModelsQueryHandler(IMlResultRepository mlResultModelRepository, IMediator mediator)
            {
                _mlResultModelRepository = mlResultModelRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ChurnBlokerMlResult>>> Handle(GetMlResultModelsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ChurnBlokerMlResult>>(await _mlResultModelRepository.GetListAsync());
            }
        }
    }
}