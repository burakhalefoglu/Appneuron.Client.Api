
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

namespace Business.Handlers.AdvEvents.Queries
{

    public class GetAdvEventsQuery : IRequest<IDataResult<IEnumerable<AdvEvent>>>
    {
        public class GetAdvEventsQueryHandler : IRequestHandler<GetAdvEventsQuery, IDataResult<IEnumerable<AdvEvent>>>
        {
            private readonly IAdvEventRepository _advEventRepository;
            private readonly IMediator _mediator;

            public GetAdvEventsQueryHandler(IAdvEventRepository advEventRepository, IMediator mediator)
            {
                _advEventRepository = advEventRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<AdvEvent>>> Handle(GetAdvEventsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<AdvEvent>>(await _advEventRepository.GetListAsync());
            }
        }
    }
}