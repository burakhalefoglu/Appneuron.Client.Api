
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

namespace Business.Handlers.ChurnClientPredictionResults.Queries
{
    public class GetChurnClientPredictionResultQuery : IRequest<IDataResult<ChurnClientPredictionResult>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetChurnClientPredictionResultQueryHandler : IRequestHandler<GetChurnClientPredictionResultQuery, IDataResult<ChurnClientPredictionResult>>
        {
            private readonly IChurnClientPredictionResultRepository _churnClientPredictionResultRepository;
            private readonly IMediator _mediator;

            public GetChurnClientPredictionResultQueryHandler(IChurnClientPredictionResultRepository churnClientPredictionResultRepository, IMediator mediator)
            {
                _churnClientPredictionResultRepository = churnClientPredictionResultRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ChurnClientPredictionResult>> Handle(GetChurnClientPredictionResultQuery request, CancellationToken cancellationToken)
            {
                var churnClientPredictionResult = await _churnClientPredictionResultRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<ChurnClientPredictionResult>(churnClientPredictionResult);
            }
        }
    }
}
