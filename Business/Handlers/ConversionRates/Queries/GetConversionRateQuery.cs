using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.ChartModels;
using MediatR;
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ConversionRates.Queries
{
    public class GetConversionRateQuery : IRequest<IDataResult<ConversionRate>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetConversionRateQueryHandler : IRequestHandler<GetConversionRateQuery, IDataResult<ConversionRate>>
        {
            private readonly IConversionRateRepository _conversionRateRepository;
            private readonly IMediator _mediator;

            public GetConversionRateQueryHandler(IConversionRateRepository conversionRateRepository, IMediator mediator)
            {
                _conversionRateRepository = conversionRateRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ConversionRate>> Handle(GetConversionRateQuery request, CancellationToken cancellationToken)
            {
                var conversionRate = await _conversionRateRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<ConversionRate>(conversionRate);
            }
        }
    }
}