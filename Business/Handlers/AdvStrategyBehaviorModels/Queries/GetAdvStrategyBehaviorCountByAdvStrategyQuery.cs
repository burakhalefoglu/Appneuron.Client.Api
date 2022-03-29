using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.AdvStrategyBehaviorModels.Queries;

public class GetAdvStrategyBehaviorCountByAdvStrategyQuery : IRequest<IDataResult<int>>
{
    public long ProjectId { get; set; }
    public string Name { get; set; }
    public int Version { get; set; }
    public DateTime StartTime { get; set; }

    public class
        GetAdvStrategyBehaviorCountByAdvStrategyQueryHandler : IRequestHandler<
            GetAdvStrategyBehaviorCountByAdvStrategyQuery, IDataResult<int>>
    {
        private readonly IAdvStrategyBehaviorModelRepository _advStrategyBehaviorModelRepository;
        private readonly IMediator _mediator;

        public GetAdvStrategyBehaviorCountByAdvStrategyQueryHandler(
            IAdvStrategyBehaviorModelRepository advStrategyBehaviorModelRepository, IMediator mediator)
        {
            _advStrategyBehaviorModelRepository = advStrategyBehaviorModelRepository;
            _mediator = mediator;
        }

        [LogAspect(typeof(ConsoleLogger))]
        [SecuredOperation(Priority = 1)]
        public async Task<IDataResult<int>> Handle(GetAdvStrategyBehaviorCountByAdvStrategyQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _advStrategyBehaviorModelRepository.GetListAsync(
                a => a.ProjectId == request.ProjectId &&
                     a.Name == request.Name &&
                     a.Version == request.Version &&
                     a.DateTime == request.StartTime &&
                     a.Status == true);

            return new SuccessDataResult<int>(result.ToList().Count);
        }
    }
}