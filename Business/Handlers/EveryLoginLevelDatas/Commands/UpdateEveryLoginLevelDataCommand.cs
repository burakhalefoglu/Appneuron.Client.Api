using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.EveryLoginLevelDatas.ValidationRules;
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

namespace Business.Handlers.EveryLoginLevelDatas.Commands
{
    public class UpdateEveryLoginLevelDataCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ClientId { get; set; }
        public string ProjectID { get; set; }
        public string CustomerID { get; set; }
        public string Levelname { get; set; }
        public int LevelsDifficultylevel { get; set; }
        public int PlayingTime { get; set; }
        public int AverageScores { get; set; }
        public int IsDead { get; set; }
        public int TotalPowerUsage { get; set; }

        public class UpdateEveryLoginLevelDataCommandHandler : IRequestHandler<UpdateEveryLoginLevelDataCommand, IResult>
        {
            private readonly IEveryLoginLevelDataRepository _everyLoginLevelDataRepository;
            private readonly IMediator _mediator;

            public UpdateEveryLoginLevelDataCommandHandler(IEveryLoginLevelDataRepository everyLoginLevelDataRepository, IMediator mediator)
            {
                _everyLoginLevelDataRepository = everyLoginLevelDataRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateEveryLoginLevelDataValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateEveryLoginLevelDataCommand request, CancellationToken cancellationToken)
            {
                var everyLoginLevelData = new EveryLoginLevelData();
                everyLoginLevelData.ClientId = request.ClientId;
                everyLoginLevelData.ProjectID = request.ProjectID;
                everyLoginLevelData.CustomerID = request.CustomerID;
                everyLoginLevelData.Levelname = request.Levelname;
                everyLoginLevelData.LevelsDifficultylevel = request.LevelsDifficultylevel;
                everyLoginLevelData.PlayingTime = request.PlayingTime;
                everyLoginLevelData.AverageScores = request.AverageScores;
                everyLoginLevelData.IsDead = request.IsDead;
                everyLoginLevelData.TotalPowerUsage = request.TotalPowerUsage;

                await _everyLoginLevelDataRepository.UpdateAsync(request.Id, everyLoginLevelData);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}