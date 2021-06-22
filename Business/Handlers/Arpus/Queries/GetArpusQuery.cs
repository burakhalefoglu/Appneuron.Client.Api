
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
using Entities.Concrete.ChartModels;

namespace Business.Handlers.Arpus.Queries
{

    public class GetArpusQuery : IRequest<IDataResult<IEnumerable<Arpu>>>
    {
        public class GetArpusQueryHandler : IRequestHandler<GetArpusQuery, IDataResult<IEnumerable<Arpu>>>
        {
            private readonly IArpuRepository _arpuRepository;
            private readonly IMediator _mediator;

            public GetArpusQueryHandler(IArpuRepository arpuRepository, IMediator mediator)
            {
                _arpuRepository = arpuRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Arpu>>> Handle(GetArpusQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Arpu>>(await _arpuRepository.GetListAsync());
            }
        }
    }
}