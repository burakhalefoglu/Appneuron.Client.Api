
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Tests.ValidationRules;

namespace Business.Handlers.Tests.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateTestCommand : IRequest<IResult>
    {

        public string Name { get; set; }


        public class CreateTestCommandHandler : IRequestHandler<CreateTestCommand, IResult>
        {
            private readonly ITestRepository _testRepository;
            private readonly IMediator _mediator;
            public CreateTestCommandHandler(ITestRepository testRepository, IMediator mediator)
            {
                _testRepository = testRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateTestValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            // [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateTestCommand request, CancellationToken cancellationToken)
            {
                var isThereTestRecord = _testRepository.Any(u => u.Name == request.Name);

                if (isThereTestRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedTest = new Test
                {
                    Name = request.Name,

                };

                await _testRepository.AddAsync(addedTest);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}