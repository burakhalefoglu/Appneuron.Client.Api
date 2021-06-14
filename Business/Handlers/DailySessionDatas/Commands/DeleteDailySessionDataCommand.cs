
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Business.Handlers.DailySessionDatas.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteDailySessionDataCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteDailySessionDataCommandHandler : IRequestHandler<DeleteDailySessionDataCommand, IResult>
        {
            private readonly IDailySessionDataRepository _dailySessionDataRepository;
            private readonly IMediator _mediator;

            public DeleteDailySessionDataCommandHandler(IDailySessionDataRepository dailySessionDataRepository, IMediator mediator)
            {
                _dailySessionDataRepository = dailySessionDataRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteDailySessionDataCommand request, CancellationToken cancellationToken)
            {


                await _dailySessionDataRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

