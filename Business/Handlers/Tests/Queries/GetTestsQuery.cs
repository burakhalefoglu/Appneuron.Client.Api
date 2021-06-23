using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Tests.Queries
{
    public class GetTestsQuery : IRequest<IDataResult<IEnumerable<Test>>>
    {
        public class GetTestsQueryHandler : IRequestHandler<GetTestsQuery, IDataResult<IEnumerable<Test>>>
        {
            private readonly ITestRepository _testRepository;
            private readonly IMediator _mediator;

            public GetTestsQueryHandler(ITestRepository testRepository, IMediator mediator)
            {
                _testRepository = testRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Test>>> Handle(GetTestsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Test>>(await _testRepository.GetListAsync());
            }
        }
    }
}