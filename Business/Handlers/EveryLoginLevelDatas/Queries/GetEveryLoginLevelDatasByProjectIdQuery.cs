using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.EveryLoginLevelDatas.Queries
{
    public class GetEveryLoginLevelDatasByProjectIdQuery : IRequest<IDataResult<IEnumerable<EveryLoginLevelData>>>
    {
        public long ProjectId { get; set; }

        public class GetEveryLoginLevelDatasByProjectIdQueryHandler : IRequestHandler<
            GetEveryLoginLevelDatasByProjectIdQuery, IDataResult<IEnumerable<EveryLoginLevelData>>>
        {
            private readonly IEveryLoginLevelDataRepository _everyLoginLevelDataRepository;
            private readonly IMediator _mediator;

            public GetEveryLoginLevelDatasByProjectIdQueryHandler(
                IEveryLoginLevelDataRepository everyLoginLevelDataRepository, IMediator mediator)
            {
                _everyLoginLevelDataRepository = everyLoginLevelDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<EveryLoginLevelData>>> Handle(
                GetEveryLoginLevelDatasByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<EveryLoginLevelData>>
                (await _everyLoginLevelDataRepository.GetListAsync(p =>
                    p.ProjectId == request.ProjectId && p.Status == true));
            }
        }
    }
}