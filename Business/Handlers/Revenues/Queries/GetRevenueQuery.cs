
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
using Entities.Concrete.ChartModels;

namespace Business.Handlers.Revenues.Queries
{
    public class GetRevenueQuery : IRequest<IDataResult<ProjectBaseRevenue>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetRevenueQueryHandler : IRequestHandler<GetRevenueQuery, IDataResult<ProjectBaseRevenue>>
        {
            private readonly IRevenueRepository _revenueRepository;
            private readonly IMediator _mediator;

            public GetRevenueQueryHandler(IRevenueRepository revenueRepository, IMediator mediator)
            {
                _revenueRepository = revenueRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ProjectBaseRevenue>> Handle(GetRevenueQuery request, CancellationToken cancellationToken)
            {
                var revenue = await _revenueRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<ProjectBaseRevenue>(revenue);
            }
        }
    }
}
