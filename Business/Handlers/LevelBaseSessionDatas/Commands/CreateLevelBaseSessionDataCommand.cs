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
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.LevelBaseSessionDatas.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CreateLevelBaseSessionDataCommand : IRequest<IResult>
    {
        public string ClientId { get; set; }
        public string ProjectID { get; set; }
        public string CustomerID { get; set; }
        public string levelName { get; set; }
        public int DifficultyLevel { get; set; }
        public float SessionTimeMinute { get; set; }
        public System.DateTime SessionStartTime { get; set; }
        public System.DateTime SessionFinishTime { get; set; }

        public class CreateLevelBaseSessionDataCommandHandler : IRequestHandler<CreateLevelBaseSessionDataCommand, IResult>
        {
            private readonly ILevelBaseSessionDataRepository _levelBaseSessionDataRepository;
            private readonly IMediator _mediator;

            public CreateLevelBaseSessionDataCommandHandler(ILevelBaseSessionDataRepository levelBaseSessionDataRepository, IMediator mediator)
            {
                _levelBaseSessionDataRepository = levelBaseSessionDataRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateLevelBaseSessionDataValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateLevelBaseSessionDataCommand request, CancellationToken cancellationToken)
            {
                var addedLevelBaseSessionData = new LevelBaseSessionData
                {
                    ClientId = request.ClientId,
                    ProjectID = request.ProjectID,
                    CustomerID = request.CustomerID,
                    levelName = request.levelName,
                    DifficultyLevel = request.DifficultyLevel,
                    SessionTimeMinute = request.SessionTimeMinute,
                    SessionStartTime = request.SessionStartTime,
                    SessionFinishTime = request.SessionFinishTime,
                };

                await _levelBaseSessionDataRepository.AddAsync(addedLevelBaseSessionData);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}