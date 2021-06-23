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

namespace Business.Handlers.Arpus.Queries
{
    public class GetArpusByProjectIdQueryQuery : IRequest<IDataResult<IEnumerable<Arpu>>>
    {
        public string ProjectId { get; set; }

        public class GetArpusByProjectIdQueryQueryHandler : IRequestHandler<GetArpusByProjectIdQueryQuery, IDataResult<IEnumerable<Arpu>>>
        {
            private readonly IArpuRepository _arpuRepository;
            private readonly IMediator _mediator;

            public GetArpusByProjectIdQueryQueryHandler(IArpuRepository arpuRepository, IMediator mediator)
            {
                _arpuRepository = arpuRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Arpu>>> Handle(GetArpusByProjectIdQueryQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Arpu>>(await _arpuRepository.GetListAsync(p => p.ProjectId == request.ProjectId));
            }
        }
    }
}