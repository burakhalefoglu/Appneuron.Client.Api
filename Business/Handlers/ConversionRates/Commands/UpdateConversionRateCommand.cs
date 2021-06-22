
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.ConversionRates.ValidationRules;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.ConversionRates.Commands
{


    public class UpdateConversionRateCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public RevenueByDaily[] RevenueByDaily { get; set; }

        public class UpdateConversionRateCommandHandler : IRequestHandler<UpdateConversionRateCommand, IResult>
        {
            private readonly IConversionRateRepository _conversionRateRepository;
            private readonly IMediator _mediator;

            public UpdateConversionRateCommandHandler(IConversionRateRepository conversionRateRepository, IMediator mediator)
            {
                _conversionRateRepository = conversionRateRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateConversionRateValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateConversionRateCommand request, CancellationToken cancellationToken)
            {



                var conversionRate = new ConversionRate();
                conversionRate.ProjectId = request.ProjectId;
                conversionRate.RevenueByDaily = request.RevenueByDaily;

                await _conversionRateRepository.UpdateAsync(request.Id, conversionRate);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

