
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
using Entities.Concrete.ChartModels;

namespace Business.Handlers.Arpus.Queries
{
    public class GetArpuQuery : IRequest<IDataResult<Arpu>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetArpuQueryHandler : IRequestHandler<GetArpuQuery, IDataResult<Arpu>>
        {
            private readonly IArpuRepository _arpuRepository;
            private readonly IMediator _mediator;

            public GetArpuQueryHandler(IArpuRepository arpuRepository, IMediator mediator)
            {
                _arpuRepository = arpuRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Arpu>> Handle(GetArpuQuery request, CancellationToken cancellationToken)
            {
                var arpu = await _arpuRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<Arpu>(arpu);
            }
        }
    }
}
