
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
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers.ApacheKafka;

namespace Business.Handlers.MlResultModels.Queries
{
    public class GetMlResultModelQuery : IRequest<IDataResult<MlResultModel>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetMlResultModelQueryHandler : IRequestHandler<GetMlResultModelQuery, IDataResult<MlResultModel>>
        {
            private readonly IMlResultModelRepository _mlResultModelRepository;
            private readonly IMediator _mediator;

            public GetMlResultModelQueryHandler(IMlResultModelRepository mlResultModelRepository, IMediator mediator)
            {
                _mlResultModelRepository = mlResultModelRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(ApacheKafkaDatabaseActionLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<MlResultModel>> Handle(GetMlResultModelQuery request, CancellationToken cancellationToken)
            {
                var mlResultModel = await _mlResultModelRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<MlResultModel>(mlResultModel);
            }
        }
    }
}
