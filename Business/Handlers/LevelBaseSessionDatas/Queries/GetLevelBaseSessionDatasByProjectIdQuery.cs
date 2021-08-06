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

namespace Business.Handlers.LevelBaseSessionDatas.Queries
{
    public class GetLevelBaseSessionDatasByProjectIdQuery : IRequest<IDataResult<IEnumerable<LevelBaseSessionData>>>
    {
        public string ProjectID { get; set; }

        public class GetLevelBaseSessionDatasByProjectIdQueryHandler : IRequestHandler<GetLevelBaseSessionDatasByProjectIdQuery, IDataResult<IEnumerable<LevelBaseSessionData>>>
        {
            private readonly ILevelBaseSessionDataRepository _levelBaseSessionDataRepository;
            private readonly IMediator _mediator;

            public GetLevelBaseSessionDatasByProjectIdQueryHandler(ILevelBaseSessionDataRepository levelBaseSessionDataRepository, IMediator mediator)
            {
                _levelBaseSessionDataRepository = levelBaseSessionDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ApacheKafkaDatabaseActionLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<LevelBaseSessionData>>> Handle(GetLevelBaseSessionDatasByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<LevelBaseSessionData>>
                    (await _levelBaseSessionDataRepository.GetListAsync(p=>p.ProjectID == request.ProjectID));
            }
        }
    }
}