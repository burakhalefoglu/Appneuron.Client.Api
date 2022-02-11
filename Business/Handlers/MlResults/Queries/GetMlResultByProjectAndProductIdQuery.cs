
using Business.BusinessAspects;
using Core.Utilities.Results;
using Core.Aspects.Autofac.Performance;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Caching;
using Entities.Dtos;
using System.Linq;

namespace Business.Handlers.MlResults.Queries
{

    public class GetMlResultByProjectAndProductIdQuery : IRequest<IDataResult<IEnumerable<MlResultDto>>>
    {
        public long ProjectId { get; set; }
        public int ProductId { get; set; }
        public class GetMlResultByProjectAndProductIdQueryHandler : IRequestHandler<GetMlResultByProjectAndProductIdQuery, IDataResult<IEnumerable<MlResultDto>>>
        {
            private readonly IMlResultRepository _mlResultRepository;
            private readonly IMediator _mediator;

            public GetMlResultByProjectAndProductIdQueryHandler(IMlResultRepository mlResultRepository, IMediator mediator)
            {
                _mlResultRepository = mlResultRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<MlResultDto>>> Handle(GetMlResultByProjectAndProductIdQuery request, CancellationToken cancellationToken)
            {
                var mlResultList = await _mlResultRepository.GetListAsync(m => m.ProductId == request.ProductId && m.ProjectId == request.ProjectId && m.Status == true);
                var mlResultDtoList = new List<MlResultDto>();
                mlResultList.ToList().ForEach(m =>
                {
                    mlResultDtoList.Add(new MlResultDto
                    {
                        ClientId = m.ClientId,
                        DateTime = m.DateTime,
                        ResultValue = m.ResultValue
                    });
                });
                return new SuccessDataResult<IEnumerable<MlResultDto>>(mlResultDtoList);
            }
        }
    }
}