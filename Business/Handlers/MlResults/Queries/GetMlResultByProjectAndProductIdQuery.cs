using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos;
using MediatR;

namespace Business.Handlers.MlResults.Queries
{
    public class GetMlResultByProjectAndProductIdQuery : IRequest<IDataResult<IEnumerable<MlResultDto>>>
    {
        public long ProjectId { get; set; }
        public int ProductId { get; set; }

        public class GetMlResultByProjectAndProductIdQueryHandler : IRequestHandler<
            GetMlResultByProjectAndProductIdQuery, IDataResult<IEnumerable<MlResultDto>>>
        {
            private readonly IMediator _mediator;
            private readonly IMlResultRepository _mlResultRepository;

            public GetMlResultByProjectAndProductIdQueryHandler(IMlResultRepository mlResultRepository,
                IMediator mediator)
            {
                _mlResultRepository = mlResultRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<MlResultDto>>> Handle(
                GetMlResultByProjectAndProductIdQuery request, CancellationToken cancellationToken)
            {
                var mlResultList = await _mlResultRepository.GetListAsync(m =>
                    m.ProductId == request.ProductId && m.ProjectId == request.ProjectId && m.Status == true);
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