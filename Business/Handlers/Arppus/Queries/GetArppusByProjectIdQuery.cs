
using Business.BusinessAspects;
using Core.Utilities.Results;
using Core.Aspects.Autofac.Performance;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Caching;
using Entities.Concrete.ChartModels;

namespace Business.Handlers.Arppus.Queries
{

    public class GetArppusByProjectIdQuery : IRequest<IDataResult<IEnumerable<Arppu>>>
    {
        public string ProjectId { get; set; }

        public class GetArppusByProjectIdQueryHandler : IRequestHandler<GetArppusByProjectIdQuery, IDataResult<IEnumerable<Arppu>>>
        {
            private readonly IArppuRepository _arppuRepository;
            private readonly IMediator _mediator;

            public GetArppusByProjectIdQueryHandler(IArppuRepository arppuRepository, IMediator mediator)
            {
                _arppuRepository = arppuRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Arppu>>> Handle(GetArppusByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Arppu>>(await _arppuRepository.GetListAsync(p => p.ProjectId == request.ProjectId));
            }
        }
    }
}