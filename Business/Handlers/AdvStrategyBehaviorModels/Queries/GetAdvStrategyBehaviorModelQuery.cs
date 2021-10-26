
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using MongoDB.Bson;

namespace Business.Handlers.AdvStrategyBehaviorModels.Queries
{
    public class GetAdvStrategyBehaviorModelQuery : IRequest<IDataResult<AdvStrategyBehaviorModel>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetAdvStrategyBehaviorModelQueryHandler : IRequestHandler<GetAdvStrategyBehaviorModelQuery, IDataResult<AdvStrategyBehaviorModel>>
        {
            private readonly IAdvStrategyBehaviorModelRepository _advStrategyBehaviorModelRepository;
            private readonly IMediator _mediator;

            public GetAdvStrategyBehaviorModelQueryHandler(IAdvStrategyBehaviorModelRepository advStrategyBehaviorModelRepository, IMediator mediator)
            {
                _advStrategyBehaviorModelRepository = advStrategyBehaviorModelRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<AdvStrategyBehaviorModel>> Handle(GetAdvStrategyBehaviorModelQuery request, CancellationToken cancellationToken)
            {
                var advStrategyBehaviorModel = await _advStrategyBehaviorModelRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<AdvStrategyBehaviorModel>(advStrategyBehaviorModel);
            }
        }
    }
}
