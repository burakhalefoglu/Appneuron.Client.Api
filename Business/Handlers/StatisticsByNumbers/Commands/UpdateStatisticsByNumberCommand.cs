using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.StatisticsByNumbers.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.ChartModels;
using MediatR;
using MongoDB.Bson;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.StatisticsByNumbers.Commands
{
    public class UpdateStatisticsByNumberCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectID { get; set; }
        public DateTime CreatedDate { get; set; }
        public long ClientCount { get; set; }
        public long PaidPlayer { get; set; }

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
                statisticsByNumber.ClientCount = request.ClientCount;
                statisticsByNumber.CreatedDate = request.CreatedDate;
                statisticsByNumber.PaidPlayer = request.PaidPlayer;

                await _statisticsByNumberRepository.UpdateAsync(request.Id, statisticsByNumber);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}