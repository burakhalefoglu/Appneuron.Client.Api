using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.LevelBaseSessionModels.Queries
{
    public class GetLevelBaseSessionModelsByProjectIdQuery : IRequest<IDataResult<IEnumerable<LevelBaseSessionModel>>>
    {
        public long ProjectId { get; set; }

        public class GetLevelBaseSessionModelsByProjectIdQueryHandler : IRequestHandler<
            GetLevelBaseSessionModelsByProjectIdQuery, IDataResult<IEnumerable<LevelBaseSessionModel>>>
        {
            private readonly ILevelBaseSessionModelRepository _levelBaseSessionDataRepository;
            private readonly IMediator _mediator;

            public GetLevelBaseSessionModelsByProjectIdQueryHandler(
                ILevelBaseSessionModelRepository levelBaseSessionDataRepository, IMediator mediator)
            {
                _levelBaseSessionDataRepository = levelBaseSessionDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<LevelBaseSessionModel>>> Handle(
                GetLevelBaseSessionModelsByProjectIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<LevelBaseSessionModel>>
                (await _levelBaseSessionDataRepository.GetListAsync(p =>
                    p.ProjectId == request.ProjectId && p.Status == true));
            }
        }
    }
}