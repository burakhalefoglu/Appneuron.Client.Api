using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.LevelBaseDieDatas.ValidationRules;
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

namespace Business.Handlers.LevelBaseDieDatas.Commands
{
    public class UpdateLevelBaseDieDataCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ClientId { get; set; }
        public string ProjectID { get; set; }
        public string CustomerID { get; set; }
        public int DiyingTimeAfterLevelStarting { get; set; }
        public string levelName { get; set; }
        public int DiyingDifficultyLevel { get; set; }
        public float DiyingLocationX { get; set; }
        public float DiyingLocationY { get; set; }
        public float DiyingLocationZ { get; set; }

        public class UpdateLevelBaseDieDataCommandHandler : IRequestHandler<UpdateLevelBaseDieDataCommand, IResult>
        {
            private readonly ILevelBaseDieDataRepository _levelBaseDieDataRepository;
            private readonly IMediator _mediator;

            public UpdateLevelBaseDieDataCommandHandler(ILevelBaseDieDataRepository levelBaseDieDataRepository, IMediator mediator)
            {
                _levelBaseDieDataRepository = levelBaseDieDataRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateLevelBaseDieDataValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateLevelBaseDieDataCommand request, CancellationToken cancellationToken)
            {
                var levelBaseDieData = new LevelBaseDieData();
                levelBaseDieData.ClientId = request.ClientId;
                levelBaseDieData.ProjectID = request.ProjectID;
                levelBaseDieData.CustomerID = request.CustomerID;
                levelBaseDieData.DiyingTimeAfterLevelStarting = request.DiyingTimeAfterLevelStarting;
                levelBaseDieData.levelName = request.levelName;
                levelBaseDieData.DiyingDifficultyLevel = request.DiyingDifficultyLevel;
                levelBaseDieData.DiyingLocationX = request.DiyingLocationX;
                levelBaseDieData.DiyingLocationY = request.DiyingLocationY;
                levelBaseDieData.DiyingLocationZ = request.DiyingLocationZ;

                await _levelBaseDieDataRepository.UpdateAsync(request.Id, levelBaseDieData);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}