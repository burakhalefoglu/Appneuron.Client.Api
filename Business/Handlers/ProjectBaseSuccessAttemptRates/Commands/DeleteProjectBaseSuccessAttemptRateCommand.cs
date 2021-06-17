
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

namespace Business.Handlers.ProjectBaseSuccessAttemptRates.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteProjectBaseSuccessAttemptRateCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteProjectBaseSuccessAttemptRateCommandHandler : IRequestHandler<DeleteProjectBaseSuccessAttemptRateCommand, IResult>
        {
            private readonly IProjectBaseSuccessAttemptRateRepository _projectBaseSuccessAttemptRateRepository;
            private readonly IMediator _mediator;

            public DeleteProjectBaseSuccessAttemptRateCommandHandler(IProjectBaseSuccessAttemptRateRepository projectBaseSuccessAttemptRateRepository, IMediator mediator)
            {
                _projectBaseSuccessAttemptRateRepository = projectBaseSuccessAttemptRateRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteProjectBaseSuccessAttemptRateCommand request, CancellationToken cancellationToken)
            {


                await _projectBaseSuccessAttemptRateRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

