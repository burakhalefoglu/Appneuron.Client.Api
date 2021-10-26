
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

namespace Business.Handlers.ChurnDates.Queries
{

    public class GetChurnDatesQuery : IRequest<IDataResult<IEnumerable<ChurnDate>>>
    {
        public class GetChurnDatesQueryHandler : IRequestHandler<GetChurnDatesQuery, IDataResult<IEnumerable<ChurnDate>>>
        {
            private readonly IChurnDateRepository _churnDateRepository;
            private readonly IMediator _mediator;

            public GetChurnDatesQueryHandler(IChurnDateRepository churnDateRepository, IMediator mediator)
            {
                _churnDateRepository = churnDateRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ChurnDate>>> Handle(GetChurnDatesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ChurnDate>>(await _churnDateRepository.GetListAsync());
            }
        }
    }
}