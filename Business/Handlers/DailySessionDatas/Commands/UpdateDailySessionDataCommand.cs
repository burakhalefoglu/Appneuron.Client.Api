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
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.DailySessionDatas.Commands
{
    public class UpdateDailySessionDataCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ClientId { get; set; }
        public string ProjectID { get; set; }
        public string CustomerID { get; set; }
        public int SessionFrequency { get; set; }
        public float TotalSessionTime { get; set; }
        public System.DateTime TodayTime { get; set; }

        public class UpdateDailySessionDataCommandHandler : IRequestHandler<UpdateDailySessionDataCommand, IResult>
        {
            private readonly IDailySessionDataRepository _dailySessionDataRepository;
            private readonly IMediator _mediator;

            public UpdateDailySessionDataCommandHandler(IDailySessionDataRepository dailySessionDataRepository, IMediator mediator)
            {
                _dailySessionDataRepository = dailySessionDataRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateDailySessionDataValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateDailySessionDataCommand request, CancellationToken cancellationToken)
            {
                var dailySessionData = new DailySessionData();
                dailySessionData.ClientId = request.ClientId;
                dailySessionData.ProjectID = request.ProjectID;
                dailySessionData.CustomerID = request.CustomerID;
                dailySessionData.SessionFrequency = request.SessionFrequency;
                dailySessionData.TotalSessionTime = request.TotalSessionTime;
                dailySessionData.TodayTime = request.TodayTime;

                await _dailySessionDataRepository.UpdateAsync(request.Id, dailySessionData);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}