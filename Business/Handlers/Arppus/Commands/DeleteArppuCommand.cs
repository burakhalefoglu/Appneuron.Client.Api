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

namespace Business.Handlers.Arppus.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class DeleteArppuCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteArppuCommandHandler : IRequestHandler<DeleteArppuCommand, IResult>
        {
            private readonly IArppuRepository _arppuRepository;
            private readonly IMediator _mediator;

            public DeleteArppuCommandHandler(IArppuRepository arppuRepository, IMediator mediator)
            {
                _arppuRepository = arppuRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteArppuCommand request, CancellationToken cancellationToken)
            {
                await _arppuRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}