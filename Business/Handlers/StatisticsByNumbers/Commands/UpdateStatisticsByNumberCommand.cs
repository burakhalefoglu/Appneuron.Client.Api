
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.StatisticsByNumbers.ValidationRules;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;

namespace Business.Handlers.StatisticsByNumbers.Commands
{


    public class UpdateStatisticsByNumberCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectID { get; set; }
        public long TotalPlayer { get; set; }
        public PlayerCountOnDate[] PlayerCountOnDate { get; set; }

        public class UpdateStatisticsByNumberCommandHandler : IRequestHandler<UpdateStatisticsByNumberCommand, IResult>
        {
            private readonly IStatisticsByNumberRepository _statisticsByNumberRepository;
            private readonly IMediator _mediator;

            public UpdateStatisticsByNumberCommandHandler(IStatisticsByNumberRepository statisticsByNumberRepository, IMediator mediator)
            {
                _statisticsByNumberRepository = statisticsByNumberRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateStatisticsByNumberValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateStatisticsByNumberCommand request, CancellationToken cancellationToken)
            {



                var statisticsByNumber = new StatisticsByNumber();
                statisticsByNumber.ProjectID = request.ProjectID;
                statisticsByNumber.TotalPlayer = request.TotalPlayer;
                statisticsByNumber.PlayerCountOnDate = request.PlayerCountOnDate;


                await _statisticsByNumberRepository.UpdateAsync(request.Id, statisticsByNumber);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

