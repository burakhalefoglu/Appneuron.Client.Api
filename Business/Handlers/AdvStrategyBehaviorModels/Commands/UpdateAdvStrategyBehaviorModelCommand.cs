
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
using Business.Handlers.AdvStrategyBehaviorModels.ValidationRules;
using MongoDB.Bson;

namespace Business.Handlers.AdvStrategyBehaviorModels.Commands
{


    public class UpdateAdvStrategyBehaviorModelCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ClientId { get; set; }
        public string ProjectId { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public System.DateTime dateTime { get; set; }

        public class UpdateAdvStrategyBehaviorModelCommandHandler : IRequestHandler<UpdateAdvStrategyBehaviorModelCommand, IResult>
        {
            private readonly IAdvStrategyBehaviorModelRepository _advStrategyBehaviorModelRepository;
            private readonly IMediator _mediator;

            public UpdateAdvStrategyBehaviorModelCommandHandler(IAdvStrategyBehaviorModelRepository advStrategyBehaviorModelRepository, IMediator mediator)
            {
                _advStrategyBehaviorModelRepository = advStrategyBehaviorModelRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateAdvStrategyBehaviorModelValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateAdvStrategyBehaviorModelCommand request, CancellationToken cancellationToken)
            {



                var advStrategyBehaviorModel = new AdvStrategyBehaviorModel();
                advStrategyBehaviorModel.ClientId = request.ClientId;
                advStrategyBehaviorModel.ProjectId = request.ProjectId;
                advStrategyBehaviorModel.Version = request.Version;
                advStrategyBehaviorModel.Name = request.Name;
                advStrategyBehaviorModel.dateTime = request.dateTime;


                await _advStrategyBehaviorModelRepository.UpdateAsync(request.Id, advStrategyBehaviorModel);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

