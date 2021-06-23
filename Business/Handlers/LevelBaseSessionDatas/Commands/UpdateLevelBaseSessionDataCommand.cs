using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.LevelBaseSessionDatas.ValidationRules;
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

namespace Business.Handlers.LevelBaseSessionDatas.Commands
{
    public class UpdateLevelBaseSessionDataCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ClientId { get; set; }
        public string ProjectID { get; set; }
        public string CustomerID { get; set; }
        public string levelName { get; set; }
        public int DifficultyLevel { get; set; }
        public float SessionTimeMinute { get; set; }
        public System.DateTime SessionStartTime { get; set; }
        public System.DateTime SessionFinishTime { get; set; }

        public class UpdateLevelBaseSessionDataCommandHandler : IRequestHandler<UpdateLevelBaseSessionDataCommand, IResult>
        {
            private readonly ILevelBaseSessionDataRepository _levelBaseSessionDataRepository;
            private readonly IMediator _mediator;

            public UpdateLevelBaseSessionDataCommandHandler(ILevelBaseSessionDataRepository levelBaseSessionDataRepository, IMediator mediator)
            {
                _levelBaseSessionDataRepository = levelBaseSessionDataRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateLevelBaseSessionDataValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateLevelBaseSessionDataCommand request, CancellationToken cancellationToken)
            {
                var levelBaseSessionData = new LevelBaseSessionData();
                levelBaseSessionData.ClientId = request.ClientId;
                levelBaseSessionData.ProjectID = request.ProjectID;
                levelBaseSessionData.CustomerID = request.CustomerID;
                levelBaseSessionData.levelName = request.levelName;
                levelBaseSessionData.DifficultyLevel = request.DifficultyLevel;
                levelBaseSessionData.SessionTimeMinute = request.SessionTimeMinute;
                levelBaseSessionData.SessionStartTime = request.SessionStartTime;
                levelBaseSessionData.SessionFinishTime = request.SessionFinishTime;

                await _levelBaseSessionDataRepository.UpdateAsync(request.Id, levelBaseSessionData);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}