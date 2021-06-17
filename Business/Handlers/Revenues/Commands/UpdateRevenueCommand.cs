
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
using Business.Handlers.Revenues.ValidationRules;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.Revenues.Commands
{


    public class UpdateRevenueCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public DailyRevenue[] DailyRevenue { get; set; }


        public class UpdateRevenueCommandHandler : IRequestHandler<UpdateRevenueCommand, IResult>
        {
            private readonly IRevenueRepository _revenueRepository;
            private readonly IMediator _mediator;

            public UpdateRevenueCommandHandler(IRevenueRepository revenueRepository, IMediator mediator)
            {
                _revenueRepository = revenueRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateRevenueValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateRevenueCommand request, CancellationToken cancellationToken)
            {



                var revenue = new ProjectBaseRevenue();
                revenue.ProjectId = request.ProjectId;
                revenue.DailyRevenue = request.DailyRevenue;


                await _revenueRepository.UpdateAsync(request.Id, revenue);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

