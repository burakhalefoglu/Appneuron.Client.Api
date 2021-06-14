
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
using Business.Handlers.EveryLoginLevelDatas.ValidationRules;

namespace Business.Handlers.EveryLoginLevelDatas.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateEveryLoginLevelDataCommand : IRequest<IResult>
    {

        public string ClientId { get; set; }
        public string ProjectID { get; set; }
        public string CustomerID { get; set; }
        public string Levelname { get; set; }
        public int LevelsDifficultylevel { get; set; }
        public int PlayingTime { get; set; }
        public int AverageScores { get; set; }
        public int IsDead { get; set; }
        public int TotalPowerUsage { get; set; }


        public class CreateEveryLoginLevelDataCommandHandler : IRequestHandler<CreateEveryLoginLevelDataCommand, IResult>
        {
            private readonly IEveryLoginLevelDataRepository _everyLoginLevelDataRepository;
            private readonly IMediator _mediator;
            public CreateEveryLoginLevelDataCommandHandler(IEveryLoginLevelDataRepository everyLoginLevelDataRepository, IMediator mediator)
            {
                _everyLoginLevelDataRepository = everyLoginLevelDataRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateEveryLoginLevelDataValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateEveryLoginLevelDataCommand request, CancellationToken cancellationToken)
            {

                var addedEveryLoginLevelData = new EveryLoginLevelData
                {
                    ClientId = request.ClientId,
                    ProjectID = request.ProjectID,
                    CustomerID = request.CustomerID,
                    Levelname = request.Levelname,
                    LevelsDifficultylevel = request.LevelsDifficultylevel,
                    PlayingTime = request.PlayingTime,
                    AverageScores = request.AverageScores,
                    IsDead = request.IsDead,
                    TotalPowerUsage = request.TotalPowerUsage,

                };

                await _everyLoginLevelDataRepository.AddAsync(addedEveryLoginLevelData);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}