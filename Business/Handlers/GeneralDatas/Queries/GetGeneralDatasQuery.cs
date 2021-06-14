
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

namespace Business.Handlers.GeneralDatas.Queries
{

    public class GetGeneralDatasQuery : IRequest<IDataResult<IEnumerable<GeneralData>>>
    {
        public class GetGeneralDatasQueryHandler : IRequestHandler<GetGeneralDatasQuery, IDataResult<IEnumerable<GeneralData>>>
        {
            private readonly IGeneralDataRepository _generalDataRepository;
            private readonly IMediator _mediator;

            public GetGeneralDatasQueryHandler(IGeneralDataRepository generalDataRepository, IMediator mediator)
            {
                _generalDataRepository = generalDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<GeneralData>>> Handle(GetGeneralDatasQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<GeneralData>>(await _generalDataRepository.GetListAsync());
            }
        }
    }
}