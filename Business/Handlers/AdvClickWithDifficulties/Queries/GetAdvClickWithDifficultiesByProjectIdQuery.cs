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

namespace Business.Handlers.AdvClickWithDifficulties.Queries
{
    public class GetAdvClickWithDifficultiesByProjectIdQuery : IRequest<IDataResult<IEnumerable<AdvClickWithDifficulty>>>
    {
        public string ProjectId { get; set; }

        public class GetAdvClickWithDifficultiesByProjectIdQueryHandler : IRequestHandler<GetAdvClickWithDifficultiesByProjectIdQuery, IDataResult<IEnumerable<AdvClickWithDifficulty>>>
        {
            private readonly IAdvClickWithDifficultyRepository _advClickWithDifficultyRepository;
            private readonly IMediator _mediator;

            public GetAdvClickWithDifficultiesByProjectIdQueryHandler(IAdvClickWithDifficultyRepository advClickWithDifficultyRepository, IMediator mediator)
            {
                _advClickWithDifficultyRepository = advClickWithDifficultyRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<AdvClickWithDifficulty>>> Handle(GetAdvClickWithDifficultiesByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<AdvClickWithDifficulty>>(await _advClickWithDifficultyRepository.GetListAsync(p => p.ProjectId == request.ProjectId));
            }
        }
    }
}