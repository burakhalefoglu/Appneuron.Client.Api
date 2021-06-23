using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Tests.Queries
{
    public class GetTestQuery : IRequest<IDataResult<Test>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetTestQueryHandler : IRequestHandler<GetTestQuery, IDataResult<Test>>
        {
            private readonly ITestRepository _testRepository;
            private readonly IMediator _mediator;

            public GetTestQueryHandler(ITestRepository testRepository, IMediator mediator)
            {
                _testRepository = testRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Test>> Handle(GetTestQuery request, CancellationToken cancellationToken)
            {
                var test = await _testRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<Test>(test);
            }
        }
    }
}