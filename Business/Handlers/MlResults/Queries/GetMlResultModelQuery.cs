
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

namespace Business.Handlers.MlResultModels.Queries
{
    public class GetMlResultModelQuery : IRequest<IDataResult<ChurnBlokerMlResult>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetMlResultModelQueryHandler : IRequestHandler<GetMlResultModelQuery, IDataResult<ChurnBlokerMlResult>>
        {
            private readonly IMlResultRepository _mlResultModelRepository;
            private readonly IMediator _mediator;

            public GetMlResultModelQueryHandler(IMlResultRepository mlResultModelRepository, IMediator mediator)
            {
                _mlResultModelRepository = mlResultModelRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ChurnBlokerMlResult>> Handle(GetMlResultModelQuery request, CancellationToken cancellationToken)
            {
                var mlResultModel = await _mlResultModelRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<ChurnBlokerMlResult>(mlResultModel);
            }
        }
    }
}
