
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
using Business.Handlers.StatisticsByNumbers.ValidationRules;
using Entities.Concrete.ChartModels;

namespace Business.Handlers.StatisticsByNumbers.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateStatisticsByNumberCommand : IRequest<IResult>
    {

        public string ProjectID { get; set; }
        public long TotalPlayer { get; set; }
        public PlayerCountOnDate[] PlayerCountOnDate { get; set; }

        public class CreateStatisticsByNumberCommandHandler : IRequestHandler<CreateStatisticsByNumberCommand, IResult>
        {
            private readonly IStatisticsByNumberRepository _statisticsByNumberRepository;
            private readonly IMediator _mediator;
            public CreateStatisticsByNumberCommandHandler(IStatisticsByNumberRepository statisticsByNumberRepository, IMediator mediator)
            {
                _statisticsByNumberRepository = statisticsByNumberRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateStatisticsByNumberValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateStatisticsByNumberCommand request, CancellationToken cancellationToken)
            {
                var isThereStatisticsByNumberRecord = _statisticsByNumberRepository.Any(u => u.ProjectID == request.ProjectID);

                if (isThereStatisticsByNumberRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedStatisticsByNumber = new StatisticsByNumber
                {
                    ProjectID = request.ProjectID,
                    TotalPlayer = request.TotalPlayer,
                    PlayerCountOnDate = request.PlayerCountOnDate

                };

                await _statisticsByNumberRepository.AddAsync(addedStatisticsByNumber);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}