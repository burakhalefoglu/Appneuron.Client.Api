using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.DailySessionDatas.ValidationRules;
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

namespace Business.Handlers.DailySessionDatas.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CreateDailySessionDataCommand : IRequest<IResult>
    {
        public string ClientId { get; set; }
        public string ProjectID { get; set; }
        public string CustomerID { get; set; }
        public int SessionFrequency { get; set; }
        public float TotalSessionTime { get; set; }
        public System.DateTime TodayTime { get; set; }

        public class CreateDailySessionDataCommandHandler : IRequestHandler<CreateDailySessionDataCommand, IResult>
        {
            private readonly IDailySessionDataRepository _dailySessionDataRepository;
            private readonly IMediator _mediator;

            public CreateDailySessionDataCommandHandler(IDailySessionDataRepository dailySessionDataRepository, IMediator mediator)
            {
                _dailySessionDataRepository = dailySessionDataRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateDailySessionDataValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateDailySessionDataCommand request, CancellationToken cancellationToken)
            {
                var addedDailySessionData = new DailySessionData
                {
                    ClientId = request.ClientId,
                    ProjectID = request.ProjectID,
                    CustomerID = request.CustomerID,
                    SessionFrequency = request.SessionFrequency,
                    TotalSessionTime = request.TotalSessionTime,
                    TodayTime = request.TodayTime,
                };

                await _dailySessionDataRepository.AddAsync(addedDailySessionData);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}