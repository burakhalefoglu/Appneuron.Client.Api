
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

namespace Business.Handlers.StatisticsByNumbers.Queries
{
    public class GetStatisticsByNumberQuery : IRequest<IDataResult<StatisticsByNumber>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetStatisticsByNumberQueryHandler : IRequestHandler<GetStatisticsByNumberQuery, IDataResult<StatisticsByNumber>>
        {
            private readonly IStatisticsByNumberRepository _statisticsByNumberRepository;
            private readonly IMediator _mediator;

            public GetStatisticsByNumberQueryHandler(IStatisticsByNumberRepository statisticsByNumberRepository, IMediator mediator)
            {
                _statisticsByNumberRepository = statisticsByNumberRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<StatisticsByNumber>> Handle(GetStatisticsByNumberQuery request, CancellationToken cancellationToken)
            {
                var statisticsByNumber = await _statisticsByNumberRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<StatisticsByNumber>(statisticsByNumber);
            }
        }
    }
}
