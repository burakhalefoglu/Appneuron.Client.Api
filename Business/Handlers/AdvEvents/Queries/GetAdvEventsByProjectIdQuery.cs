using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.AdvEvents.Queries
{
    public class GetAdvEventsByProjectIdQuery : IRequest<IDataResult<IEnumerable<AdvEvent>>>
    {
        public string ProjectID { get; set; }

        public class GetAdvEventsByProjectIdQueryHandler : IRequestHandler<GetAdvEventsByProjectIdQuery, IDataResult<IEnumerable<AdvEvent>>>
        {
            private readonly IAdvEventRepository _advEventRepository;
            private readonly IMediator _mediator;

            public GetAdvEventsByProjectIdQueryHandler(IAdvEventRepository advEventRepository, IMediator mediator)
            {
                _advEventRepository = advEventRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(LogstashLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<AdvEvent>>> Handle(GetAdvEventsByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<AdvEvent>>(await _advEventRepository.GetListAsync(p=>p.ProjectId == request.ProjectID));
            }
        }
    }
}