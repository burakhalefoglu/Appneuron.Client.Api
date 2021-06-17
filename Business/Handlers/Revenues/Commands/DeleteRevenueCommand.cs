
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

namespace Business.Handlers.Revenues.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteRevenueCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteRevenueCommandHandler : IRequestHandler<DeleteRevenueCommand, IResult>
        {
            private readonly IRevenueRepository _revenueRepository;
            private readonly IMediator _mediator;

            public DeleteRevenueCommandHandler(IRevenueRepository revenueRepository, IMediator mediator)
            {
                _revenueRepository = revenueRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteRevenueCommand request, CancellationToken cancellationToken)
            {


                await _revenueRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

