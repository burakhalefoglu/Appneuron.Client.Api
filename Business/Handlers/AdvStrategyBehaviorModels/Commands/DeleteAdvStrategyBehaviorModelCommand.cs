
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

namespace Business.Handlers.AdvStrategyBehaviorModels.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteAdvStrategyBehaviorModelCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteAdvStrategyBehaviorModelCommandHandler : IRequestHandler<DeleteAdvStrategyBehaviorModelCommand, IResult>
        {
            private readonly IAdvStrategyBehaviorModelRepository _advStrategyBehaviorModelRepository;
            private readonly IMediator _mediator;

            public DeleteAdvStrategyBehaviorModelCommandHandler(IAdvStrategyBehaviorModelRepository advStrategyBehaviorModelRepository, IMediator mediator)
            {
                _advStrategyBehaviorModelRepository = advStrategyBehaviorModelRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteAdvStrategyBehaviorModelCommand request, CancellationToken cancellationToken)
            {


                await _advStrategyBehaviorModelRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

