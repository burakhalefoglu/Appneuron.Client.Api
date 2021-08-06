using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers.ApacheKafka;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.LevelBaseDieDatas.Queries
{
    public class GetLevelBaseDieDatasDtoByProjectIdQuery : IRequest<IDataResult<IEnumerable<LevelBaseDieDataDto>>>
    {
        public string ProjectID { get; set; }

        public class GetLevelBaseDieDatasByProjectIdQueryQueryHandler : IRequestHandler<GetLevelBaseDieDatasDtoByProjectIdQuery, IDataResult<IEnumerable<LevelBaseDieDataDto>>>
        {
            private readonly ILevelBaseDieDataRepository _levelBaseDieDataRepository;
            private readonly IMediator _mediator;

            public GetLevelBaseDieDatasByProjectIdQueryQueryHandler(ILevelBaseDieDataRepository levelBaseDieDataRepository, IMediator mediator)
            {
                _levelBaseDieDataRepository = levelBaseDieDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ApacheKafkaDatabaseActionLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<LevelBaseDieDataDto>>> Handle(GetLevelBaseDieDatasDtoByProjectIdQuery request, CancellationToken cancellationToken)
            {
                var levelBaseDieDataDtoslist = new List<LevelBaseDieDataDto>();
               var levelBaseDieDatasList =  await _levelBaseDieDataRepository.GetListAsync(p => p.ProjectID == request.ProjectID);

                levelBaseDieDatasList.ToList().ForEach(item =>
                {
                    levelBaseDieDataDtoslist.Add(new LevelBaseDieDataDto
                    {
                        ClientId = item.ClientId,
                        DiyingDifficultyLevel = item.DiyingDifficultyLevel,
                        levelName = item.levelName

                    });
                });


                return new SuccessDataResult<IEnumerable<LevelBaseDieDataDto>>(levelBaseDieDataDtoslist);
            }
        }
    }
}