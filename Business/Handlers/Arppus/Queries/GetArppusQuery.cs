using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.ChartModels;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Arppus.Queries
{
    public class GetArppusQuery : IRequest<IDataResult<IEnumerable<Arppu>>>
    {
        public class GetArppusQueryHandler : IRequestHandler<GetArppusQuery, IDataResult<IEnumerable<Arppu>>>
        {
            private readonly IArppuRepository _arppuRepository;
            private readonly IMediator _mediator;

            public GetArppusQueryHandler(IArppuRepository arppuRepository, IMediator mediator)
            {
                _arppuRepository = arppuRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Arppu>>> Handle(GetArppusQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Arppu>>(await _arppuRepository.GetListAsync());
            }
        }
    }
}