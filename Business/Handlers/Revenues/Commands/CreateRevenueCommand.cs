
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
using Business.Handlers.Revenues.ValidationRules;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.Revenues.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateRevenueCommand : IRequest<IResult>
    {

        public string ProjectId { get; set; }
        public DailyRevenue[] DailyRevenue { get; set; }


        public class CreateRevenueCommandHandler : IRequestHandler<CreateRevenueCommand, IResult>
        {
            private readonly IRevenueRepository _revenueRepository;
            private readonly IMediator _mediator;
            public CreateRevenueCommandHandler(IRevenueRepository revenueRepository, IMediator mediator)
            {
                _revenueRepository = revenueRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateRevenueValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateRevenueCommand request, CancellationToken cancellationToken)
            {
                var isThereRevenueRecord = _revenueRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereRevenueRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedRevenue = new ProjectBaseRevenue
                {
                    ProjectId = request.ProjectId,
                    DailyRevenue = request.DailyRevenue

                };

                await _revenueRepository.AddAsync(addedRevenue);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}