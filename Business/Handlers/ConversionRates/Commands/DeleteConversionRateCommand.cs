
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Business.Handlers.ConversionRates.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteConversionRateCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteConversionRateCommandHandler : IRequestHandler<DeleteConversionRateCommand, IResult>
        {
            private readonly IConversionRateRepository _conversionRateRepository;
            private readonly IMediator _mediator;

            public DeleteConversionRateCommandHandler(IConversionRateRepository conversionRateRepository, IMediator mediator)
            {
                _conversionRateRepository = conversionRateRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteConversionRateCommand request, CancellationToken cancellationToken)
            {


                await _conversionRateRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

