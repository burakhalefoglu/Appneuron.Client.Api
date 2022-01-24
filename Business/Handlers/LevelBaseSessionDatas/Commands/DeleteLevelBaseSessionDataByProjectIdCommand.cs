using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.LevelBaseSessionDatas.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class DeleteLevelBaseSessionDataByProjectIdCommand : IRequest<IResult>
    {
        public string ProjectID { get; set; }

        public class DeleteLevelBaseSessionDataByProjectIdCommandHandler : IRequestHandler<DeleteLevelBaseSessionDataByProjectIdCommand, IResult>
        {
            private readonly ILevelBaseSessionDataRepository _levelBaseSessionDataRepository;
            private readonly IMediator _mediator;

            public DeleteLevelBaseSessionDataByProjectIdCommandHandler(ILevelBaseSessionDataRepository levelBaseSessionDataRepository, IMediator mediator)
            {
                _levelBaseSessionDataRepository = levelBaseSessionDataRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(LogstashLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteLevelBaseSessionDataByProjectIdCommand request, CancellationToken cancellationToken)
            {
                await _levelBaseSessionDataRepository.DeleteAsync(p=>p.ProjectId == request.ProjectID);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}