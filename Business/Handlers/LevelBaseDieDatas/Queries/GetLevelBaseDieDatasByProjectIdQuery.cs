using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers.ApacheKafka;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.LevelBaseDieDatas.Queries
{
    public class GetLevelBaseDieDatasByProjectIdQuery : IRequest<IDataResult<IEnumerable<LevelBaseDieData>>>
    {
        public string ProjectID { get; set; }

        public class GetLevelBaseDieDatasByProjectIdQueryHandler : IRequestHandler<GetLevelBaseDieDatasByProjectIdQuery, IDataResult<IEnumerable<LevelBaseDieData>>>
        {
            private readonly ILevelBaseDieDataRepository _levelBaseDieDataRepository;
            private readonly IMediator _mediator;

            public GetLevelBaseDieDatasByProjectIdQueryHandler(ILevelBaseDieDataRepository levelBaseDieDataRepository, IMediator mediator)
            {
                _levelBaseDieDataRepository = levelBaseDieDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ApacheKafkaDatabaseActionLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<LevelBaseDieData>>> Handle(GetLevelBaseDieDatasByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<LevelBaseDieData>>
                    (await _levelBaseDieDataRepository.GetListAsync(p=>p.ProjectID == request.ProjectID));
            }
        }
    }
}