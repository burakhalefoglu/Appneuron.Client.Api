using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.AdvEvents.ValidationRules;
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

namespace Business.Handlers.AdvEvents.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CreateAdvEventCommand : IRequest<IResult>
    {
        public string ClientId { get; set; }
        public string ProjectID { get; set; }
        public string CustomerID { get; set; }
        public string TrigersInlevelName { get; set; }
        public string AdvType { get; set; }
        public int DifficultyLevel { get; set; }
        public float InMinutes { get; set; }
        public System.DateTime TrigerdTime { get; set; }

        public class CreateAdvEventCommandHandler : IRequestHandler<CreateAdvEventCommand, IResult>
        {
            private readonly IAdvEventRepository _advEventRepository;
            private readonly IMediator _mediator;

            public CreateAdvEventCommandHandler(IAdvEventRepository advEventRepository, IMediator mediator)
            {
                _advEventRepository = advEventRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateAdvEventValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateAdvEventCommand request, CancellationToken cancellationToken)
            {
                var addedAdvEvent = new AdvEvent
                {
                    ClientId = request.ClientId,
                    ProjectID = request.ProjectID,
                    CustomerID = request.CustomerID,
                    TrigersInlevelName = request.TrigersInlevelName,
                    AdvType = request.AdvType,
                    DifficultyLevel = request.DifficultyLevel,
                    InMinutes = request.InMinutes,
                    TrigerdTime = request.TrigerdTime,
                };

                await _advEventRepository.AddAsync(addedAdvEvent);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}