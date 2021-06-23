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

namespace Business.Handlers.AdvClickWithDifficulties.Queries
{
    public class GetAdvClickWithDifficultyQuery : IRequest<IDataResult<AdvClickWithDifficulty>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetAdvClickWithDifficultyQueryHandler : IRequestHandler<GetAdvClickWithDifficultyQuery, IDataResult<AdvClickWithDifficulty>>
        {
            private readonly IAdvClickWithDifficultyRepository _advClickWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetAdvClickWithDifficultyQueryHandler(IAdvClickWithDifficultyRepository advClickWithDifficultyRepository, IMediator mediator)
            {
                _advClickWithDifficultyRepository = advClickWithDifficultyRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<AdvClickWithDifficulty>> Handle(GetAdvClickWithDifficultyQuery request, CancellationToken cancellationToken)
            {
                var advClickWithDifficulty = await _advClickWithDifficultyRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<AdvClickWithDifficulty>(advClickWithDifficulty);
            }
        }
    }
}