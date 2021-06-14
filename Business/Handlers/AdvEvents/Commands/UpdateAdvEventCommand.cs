
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
using Business.Handlers.AdvEvents.ValidationRules;
using MongoDB.Bson;

namespace Business.Handlers.AdvEvents.Commands
{


    public class UpdateAdvEventCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ClientId { get; set; }
        public string ProjectID { get; set; }
        public string CustomerID { get; set; }
        public string TrigersInlevelName { get; set; }
        public string AdvType { get; set; }
        public int DifficultyLevel { get; set; }
        public float InMinutes { get; set; }
        public System.DateTime TrigerdTime { get; set; }

        public class UpdateAdvEventCommandHandler : IRequestHandler<UpdateAdvEventCommand, IResult>
        {
            private readonly IAdvEventRepository _advEventRepository;
            private readonly IMediator _mediator;

            public UpdateAdvEventCommandHandler(IAdvEventRepository advEventRepository, IMediator mediator)
            {
                _advEventRepository = advEventRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateAdvEventValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateAdvEventCommand request, CancellationToken cancellationToken)
            {



                var advEvent = new AdvEvent();
                advEvent.ClientId = request.ClientId;
                advEvent.ProjectID = request.ProjectID;
                advEvent.CustomerID = request.CustomerID;
                advEvent.TrigersInlevelName = request.TrigersInlevelName;
                advEvent.AdvType = request.AdvType;
                advEvent.DifficultyLevel = request.DifficultyLevel;
                advEvent.InMinutes = request.InMinutes;
                advEvent.TrigerdTime = request.TrigerdTime;


                await _advEventRepository.UpdateAsync(request.Id, advEvent);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

