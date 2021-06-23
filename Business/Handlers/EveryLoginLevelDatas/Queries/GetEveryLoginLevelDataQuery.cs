using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.EveryLoginLevelDatas.Queries
{
    public class GetEveryLoginLevelDataQuery : IRequest<IDataResult<EveryLoginLevelData>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetEveryLoginLevelDataQueryHandler : IRequestHandler<GetEveryLoginLevelDataQuery, IDataResult<EveryLoginLevelData>>
        {
            private readonly IEveryLoginLevelDataRepository _everyLoginLevelDataRepository;
            private readonly IMediator _mediator;

            public GetEveryLoginLevelDataQueryHandler(IEveryLoginLevelDataRepository everyLoginLevelDataRepository, IMediator mediator)
            {
                _everyLoginLevelDataRepository = everyLoginLevelDataRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<EveryLoginLevelData>> Handle(GetEveryLoginLevelDataQuery request, CancellationToken cancellationToken)
            {
                var everyLoginLevelData = await _everyLoginLevelDataRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<EveryLoginLevelData>(everyLoginLevelData);
            }
        }
    }
}