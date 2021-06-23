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

namespace Business.Handlers.DieCountWithDifficulties.Queries
{
    public class GetDieCountWithDifficultyQuery : IRequest<IDataResult<DieCountWithDifficulty>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetDieCountWithDifficultyQueryHandler : IRequestHandler<GetDieCountWithDifficultyQuery, IDataResult<DieCountWithDifficulty>>
        {
            private readonly IDieCountWithDifficultyRepository _dieCountWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetDieCountWithDifficultyQueryHandler(IDieCountWithDifficultyRepository dieCountWithDifficultyRepository, IMediator mediator)
            {
                _dieCountWithDifficultyRepository = dieCountWithDifficultyRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<DieCountWithDifficulty>> Handle(GetDieCountWithDifficultyQuery request, CancellationToken cancellationToken)
            {
                var dieCountWithDifficulty = await _dieCountWithDifficultyRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<DieCountWithDifficulty>(dieCountWithDifficulty);
            }
        }
    }
}