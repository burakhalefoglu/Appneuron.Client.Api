using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.ChartModels;
using MediatR;
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Arppus.Queries
{
    public class GetArppuQuery : IRequest<IDataResult<Arppu>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetArppuQueryHandler : IRequestHandler<GetArppuQuery, IDataResult<Arppu>>
        {
            private readonly IArppuRepository _arppuRepository;
            private readonly IMediator _mediator;

            public GetArppuQueryHandler(IArppuRepository arppuRepository, IMediator mediator)
            {
                _arppuRepository = arppuRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Arppu>> Handle(GetArppuQuery request, CancellationToken cancellationToken)
            {
                var arppu = await _arppuRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<Arppu>(arppu);
            }
        }
    }
}