
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using MongoDB.Bson;

namespace Business.Handlers.AdvEvents.Queries
{
    public class GetAdvEventQuery : IRequest<IDataResult<AdvEvent>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetAdvEventQueryHandler : IRequestHandler<GetAdvEventQuery, IDataResult<AdvEvent>>
        {
            private readonly IAdvEventRepository _advEventRepository;
            private readonly IMediator _mediator;

            public GetAdvEventQueryHandler(IAdvEventRepository advEventRepository, IMediator mediator)
            {
                _advEventRepository = advEventRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<AdvEvent>> Handle(GetAdvEventQuery request, CancellationToken cancellationToken)
            {
                var advEvent = await _advEventRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<AdvEvent>(advEvent);
            }
        }
    }
}
