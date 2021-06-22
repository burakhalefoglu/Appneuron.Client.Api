
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
using System.Linq;
using System.Collections.Generic;

namespace Business.Handlers.StatisticsByNumbers.Queries
{
    public class GetStatisticsByProjectIdQuery : IRequest<IDataResult<IEnumerable<StatisticsByNumber>>>
    {
        public string projectID { get; set; }

        public class GetStatisticsByProjectIdQueryHandler : IRequestHandler<GetStatisticsByProjectIdQuery, IDataResult<IEnumerable<StatisticsByNumber>>>
        {
            private readonly IStatisticsByNumberRepository _statisticsByNumberRepository;
            private readonly IMediator _mediator;

            public GetStatisticsByProjectIdQueryHandler(IStatisticsByNumberRepository statisticsByNumberRepository, IMediator mediator)
            {
                _statisticsByNumberRepository = statisticsByNumberRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<StatisticsByNumber>>> Handle(GetStatisticsByProjectIdQuery request, CancellationToken cancellationToken)
            {
                var statisticsByNumber = await _statisticsByNumberRepository.GetListAsync(p => p.ProjectID == request.projectID);
                return new SuccessDataResult<IEnumerable<StatisticsByNumber>>(statisticsByNumber);
            }
        }
    }
}
