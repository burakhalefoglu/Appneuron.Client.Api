
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
using Business.Handlers.AdvStrategyBehaviorModels.ValidationRules;

namespace Business.Handlers.AdvStrategyBehaviorModels.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateAdvStrategyBehaviorModelCommand : IRequest<IResult>
    {

        public string ClientId { get; set; }
        public string ProjectId { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public System.DateTime dateTime { get; set; }


        public class CreateAdvStrategyBehaviorModelCommandHandler : IRequestHandler<CreateAdvStrategyBehaviorModelCommand, IResult>
        {
            private readonly IAdvStrategyBehaviorModelRepository _advStrategyBehaviorModelRepository;
            private readonly IMediator _mediator;
            public CreateAdvStrategyBehaviorModelCommandHandler(IAdvStrategyBehaviorModelRepository advStrategyBehaviorModelRepository, IMediator mediator)
            {
                _advStrategyBehaviorModelRepository = advStrategyBehaviorModelRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateAdvStrategyBehaviorModelValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateAdvStrategyBehaviorModelCommand request, CancellationToken cancellationToken)
            {
                var isThereAdvStrategyBehaviorModelRecord = _advStrategyBehaviorModelRepository.Any(u => u.ClientId == request.ClientId);

                if (isThereAdvStrategyBehaviorModelRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedAdvStrategyBehaviorModel = new AdvStrategyBehaviorModel
                {
                    ClientId = request.ClientId,
                    ProjectId = request.ProjectId,
                    Version = request.Version,
                    Name = request.Name,
                    dateTime = request.dateTime,

                };

                await _advStrategyBehaviorModelRepository.AddAsync(addedAdvStrategyBehaviorModel);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}