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

namespace Business.Handlers.ConversionRates.Queries
{
    public class GetConversionRatesByProjectIdQuery : IRequest<IDataResult<IEnumerable<ConversionRate>>>
    {
        public string ProjectId { get; set; }

        public class GetConversionRatesByProjectIdQueryHandler : IRequestHandler<GetConversionRatesByProjectIdQuery, IDataResult<IEnumerable<ConversionRate>>>
        {
            private readonly IConversionRateRepository _conversionRateRepository;
            private readonly IMediator _mediator;

            public GetConversionRatesByProjectIdQueryHandler(IConversionRateRepository conversionRateRepository, IMediator mediator)
            {
                _conversionRateRepository = conversionRateRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ConversionRate>>> Handle(GetConversionRatesByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ConversionRate>>(await _conversionRateRepository.GetListAsync(p => p.ProjectId == request.ProjectId));
            }
        }
    }
}