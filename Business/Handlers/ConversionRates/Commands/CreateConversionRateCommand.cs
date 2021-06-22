
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.ConversionRates.ValidationRules;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.ConversionRates.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateConversionRateCommand : IRequest<IResult>
    {

        public string ProjectId { get; set; }
        public RevenueByDaily[] RevenueByDaily { get; set; }


        public class CreateConversionRateCommandHandler : IRequestHandler<CreateConversionRateCommand, IResult>
        {
            private readonly IConversionRateRepository _conversionRateRepository;
            private readonly IMediator _mediator;
            public CreateConversionRateCommandHandler(IConversionRateRepository conversionRateRepository, IMediator mediator)
            {
                _conversionRateRepository = conversionRateRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateConversionRateValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateConversionRateCommand request, CancellationToken cancellationToken)
            {
                var isThereConversionRateRecord = _conversionRateRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereConversionRateRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedConversionRate = new ConversionRate
                {
                    ProjectId = request.ProjectId,
                    RevenueByDaily = request.RevenueByDaily
                };

                await _conversionRateRepository.AddAsync(addedConversionRate);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}