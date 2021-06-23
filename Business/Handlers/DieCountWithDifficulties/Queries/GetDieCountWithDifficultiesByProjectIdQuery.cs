using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.ChartModels;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.DieCountWithDifficulties.Queries
{
    public class GetDieCountWithDifficultiesByProjectIdQuery : IRequest<IDataResult<IEnumerable<DieCountWithDifficulty>>>
    {
        public string ProjectId { get; set; }

        public class GetDieCountWithDifficultiesByProjectIdQueryHandler : IRequestHandler<GetDieCountWithDifficultiesByProjectIdQuery, IDataResult<IEnumerable<DieCountWithDifficulty>>>
        {
            private readonly IDieCountWithDifficultyRepository _dieCountWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetDieCountWithDifficultiesByProjectIdQueryHandler(IDieCountWithDifficultyRepository dieCountWithDifficultyRepository, IMediator mediator)
            {
                _dieCountWithDifficultyRepository = dieCountWithDifficultyRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<DieCountWithDifficulty>>> Handle(GetDieCountWithDifficultiesByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<DieCountWithDifficulty>>(await _dieCountWithDifficultyRepository.GetListAsync(p => p.ProjectId == request.ProjectId));
            }
        }
    }
}