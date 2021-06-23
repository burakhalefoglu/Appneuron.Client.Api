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

namespace Business.Handlers.LevelBaseSessionDatas.Queries
{
    public class GetLevelBaseSessionDataQuery : IRequest<IDataResult<LevelBaseSessionData>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetLevelBaseSessionDataQueryHandler : IRequestHandler<GetLevelBaseSessionDataQuery, IDataResult<LevelBaseSessionData>>
        {
            private readonly ILevelBaseSessionDataRepository _levelBaseSessionDataRepository;
            private readonly IMediator _mediator;

            public GetLevelBaseSessionDataQueryHandler(ILevelBaseSessionDataRepository levelBaseSessionDataRepository, IMediator mediator)
            {
                _levelBaseSessionDataRepository = levelBaseSessionDataRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<LevelBaseSessionData>> Handle(GetLevelBaseSessionDataQuery request, CancellationToken cancellationToken)
            {
                var levelBaseSessionData = await _levelBaseSessionDataRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<LevelBaseSessionData>(levelBaseSessionData);
            }
        }
    }
}