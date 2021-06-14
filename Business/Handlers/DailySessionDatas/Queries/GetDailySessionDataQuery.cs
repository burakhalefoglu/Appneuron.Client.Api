
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

namespace Business.Handlers.DailySessionDatas.Queries
{
    public class GetDailySessionDataQuery : IRequest<IDataResult<DailySessionData>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetDailySessionDataQueryHandler : IRequestHandler<GetDailySessionDataQuery, IDataResult<DailySessionData>>
        {
            private readonly IDailySessionDataRepository _dailySessionDataRepository;
            private readonly IMediator _mediator;

            public GetDailySessionDataQueryHandler(IDailySessionDataRepository dailySessionDataRepository, IMediator mediator)
            {
                _dailySessionDataRepository = dailySessionDataRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<DailySessionData>> Handle(GetDailySessionDataQuery request, CancellationToken cancellationToken)
            {
                var dailySessionData = await _dailySessionDataRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<DailySessionData>(dailySessionData);
            }
        }
    }
}
