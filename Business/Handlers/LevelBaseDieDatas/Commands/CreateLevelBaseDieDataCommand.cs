
using Business.BusinessAspects;
using Business.Constants;
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
using System.Linq;
using Business.Handlers.LevelBaseDieDatas.ValidationRules;

namespace Business.Handlers.LevelBaseDieDatas.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateLevelBaseDieDataCommand : IRequest<IResult>
    {

        public string ClientId { get; set; }
        public string ProjectID { get; set; }
        public string CustomerID { get; set; }
        public int DiyingTimeAfterLevelStarting { get; set; }
        public string levelName { get; set; }
        public int DiyingDifficultyLevel { get; set; }
        public float DiyingLocationX { get; set; }
        public float DiyingLocationY { get; set; }
        public float DiyingLocationZ { get; set; }


        public class CreateLevelBaseDieDataCommandHandler : IRequestHandler<CreateLevelBaseDieDataCommand, IResult>
        {
            private readonly ILevelBaseDieDataRepository _levelBaseDieDataRepository;
            private readonly IMediator _mediator;
            public CreateLevelBaseDieDataCommandHandler(ILevelBaseDieDataRepository levelBaseDieDataRepository, IMediator mediator)
            {
                _levelBaseDieDataRepository = levelBaseDieDataRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateLevelBaseDieDataValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateLevelBaseDieDataCommand request, CancellationToken cancellationToken)
            {

                var addedLevelBaseDieData = new LevelBaseDieData
                {
                    ClientId = request.ClientId,
                    ProjectID = request.ProjectID,
                    CustomerID = request.CustomerID,
                    DiyingTimeAfterLevelStarting = request.DiyingTimeAfterLevelStarting,
                    levelName = request.levelName,
                    DiyingDifficultyLevel = request.DiyingDifficultyLevel,
                    DiyingLocationX = request.DiyingLocationX,
                    DiyingLocationY = request.DiyingLocationY,
                    DiyingLocationZ = request.DiyingLocationZ,

                };

                await _levelBaseDieDataRepository.AddAsync(addedLevelBaseDieData);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}