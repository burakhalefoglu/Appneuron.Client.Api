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

namespace Business.Handlers.LevelBaseDieDatas.Queries
{
    public class GetLevelBaseDieDataQuery : IRequest<IDataResult<LevelBaseDieData>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetLevelBaseDieDataQueryHandler : IRequestHandler<GetLevelBaseDieDataQuery, IDataResult<LevelBaseDieData>>
        {
            private readonly ILevelBaseDieDataRepository _levelBaseDieDataRepository;
            private readonly IMediator _mediator;

            public GetLevelBaseDieDataQueryHandler(ILevelBaseDieDataRepository levelBaseDieDataRepository, IMediator mediator)
            {
                _levelBaseDieDataRepository = levelBaseDieDataRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<LevelBaseDieData>> Handle(GetLevelBaseDieDataQuery request, CancellationToken cancellationToken)
            {
                var levelBaseDieData = await _levelBaseDieDataRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<LevelBaseDieData>(levelBaseDieData);
            }
        }
    }
}