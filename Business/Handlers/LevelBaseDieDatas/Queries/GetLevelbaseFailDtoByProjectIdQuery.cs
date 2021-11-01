
using Business.BusinessAspects;
using Core.Utilities.Results;
using Core.Aspects.Autofac.Performance;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Caching;
using Entities.Dtos;
using System.Linq;

namespace Business.Handlers.LevelBaseDieDatas.Queries
{

    public class GetLevelbaseFailDtoByProjectIdQuery : IRequest<IDataResult<IEnumerable<LevelbaseFailDto>>>
    {
        public string ProjectId { get; set; }
        public class GetLevelbaseFailDtoByProjectIdQueryHandler : IRequestHandler<GetLevelbaseFailDtoByProjectIdQuery, IDataResult<IEnumerable<LevelbaseFailDto>>>
        {
            private readonly ILevelBaseDieDataRepository _levelBaseDieDataRepository;
            private readonly IMediator _mediator;

            public GetLevelbaseFailDtoByProjectIdQueryHandler(ILevelBaseDieDataRepository levelBaseDieDataRepository, IMediator mediator)
            {
                _levelBaseDieDataRepository = levelBaseDieDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<LevelbaseFailDto>>> Handle(GetLevelbaseFailDtoByProjectIdQuery request, CancellationToken cancellationToken)
            {
                var levelBaseDieDataList = await _levelBaseDieDataRepository.GetListAsync(l => l.ProjectId == request.ProjectId);
                var levelbaseFailDtoList = new List<LevelbaseFailDto>();

                levelBaseDieDataList.ToList().ForEach(levelBaseFail =>
                {
                    var levelbaseFailDto = levelbaseFailDtoList.FirstOrDefault(levelBaseFailDto => levelBaseFailDto.ClientId == levelBaseFail.ClientId
                    && levelBaseFailDto.LevelName == levelBaseFail.LevelName);

                    if(levelbaseFailDto == null)
                    {
                        levelbaseFailDtoList.Add(new LevelbaseFailDto
                        {
                            ClientId = levelBaseFail.ClientId,
                            FailCount = 1,
                            LevelName = levelBaseFail.LevelName
                        });
                    }
                    else
                    {
                        levelbaseFailDto.FailCount += 1;
                    }

                });

                return new SuccessDataResult<IEnumerable<LevelbaseFailDto>>(levelbaseFailDtoList);
            }
        }
    }
}