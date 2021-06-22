
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

namespace Business.Handlers.ConversionRates.Queries
{

    public class GetConversionRatesQuery : IRequest<IDataResult<IEnumerable<ConversionRate>>>
    {
        public class GetConversionRatesQueryHandler : IRequestHandler<GetConversionRatesQuery, IDataResult<IEnumerable<ConversionRate>>>
        {
            private readonly IConversionRateRepository _conversionRateRepository;
            private readonly IMediator _mediator;

            public GetConversionRatesQueryHandler(IConversionRateRepository conversionRateRepository, IMediator mediator)
            {
                _conversionRateRepository = conversionRateRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ConversionRate>>> Handle(GetConversionRatesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ConversionRate>>(await _conversionRateRepository.GetListAsync());
            }
        }
    }
}