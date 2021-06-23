using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.ChartModels;
using MediatR;
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.PowerUsageWithDifficulties.Queries
{
    public class GetPowerUsageWithDifficultyQuery : IRequest<IDataResult<PowerUsageWithDifficulty>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetPowerUsageWithDifficultyQueryHandler : IRequestHandler<GetPowerUsageWithDifficultyQuery, IDataResult<PowerUsageWithDifficulty>>
        {
            private readonly IPowerUsageWithDifficultyRepository _powerUsageWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetPowerUsageWithDifficultyQueryHandler(IPowerUsageWithDifficultyRepository powerUsageWithDifficultyRepository, IMediator mediator)
            {
                _powerUsageWithDifficultyRepository = powerUsageWithDifficultyRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<PowerUsageWithDifficulty>> Handle(GetPowerUsageWithDifficultyQuery request, CancellationToken cancellationToken)
            {
                var powerUsageWithDifficulty = await _powerUsageWithDifficultyRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<PowerUsageWithDifficulty>(powerUsageWithDifficulty);
            }
        }
    }
}