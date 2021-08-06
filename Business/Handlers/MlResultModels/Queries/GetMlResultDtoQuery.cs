
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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Core.Utilities.IoC;
using System.Linq;
using Business.Constants;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers.ApacheKafka;

namespace Business.Handlers.MlResultModels.Queries
{

    public class GetMlResultDtoQuery : IRequest<IDataResult<MlResultDto>>
    {
        public short ProductId { get; set; }
        public class GetMlResultDtoQueryHandler : IRequestHandler<GetMlResultDtoQuery, IDataResult<MlResultDto>>
        {
            private readonly IMlResultModelRepository _mlResultModelRepository;
            private readonly IMediator _mediator;
            private readonly IHttpContextAccessor _httpContextAccessor;


            public GetMlResultDtoQueryHandler(IMlResultModelRepository mlResultModelRepository, IMediator mediator)
            {
                _mlResultModelRepository = mlResultModelRepository;
                _mediator = mediator;
                _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ApacheKafkaDatabaseActionLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<MlResultDto>> Handle(GetMlResultDtoQuery request, CancellationToken cancellationToken)
            {
                var ClientId = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                var ProjectId = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.EndsWith("ProjectId"))?.Value;

                var MlResultList = await _mlResultModelRepository.GetListAsync(c => c.ClientId == ClientId 
                && c.ProjectId == ProjectId
                && c.ProductId == request.ProductId);
                if (MlResultList.ToList().Count == 0)
                {
                    return new SuccessDataResult<MlResultDto>(null, Messages.Unknown);
                }

                var MlResult = MlResultList.Distinct().ToList().OrderByDescending(item => item.DateTime).ToArray()[0];
                MlResultDto mlResultDto = new MlResultDto();
                mlResultDto.CenterOfDifficultyLevel = (int)MlResult.ResultValue;
                mlResultDto.RangeCount = 2;

                return new SuccessDataResult<MlResultDto>(mlResultDto);
            }
    }
}
}