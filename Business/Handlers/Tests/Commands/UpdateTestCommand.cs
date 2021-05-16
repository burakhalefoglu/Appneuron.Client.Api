
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Tests.ValidationRules;
using MongoDB.Bson;

namespace Business.Handlers.Tests.Commands
{


    public class UpdateTestCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string Name { get; set; }

        public class UpdateTestCommandHandler : IRequestHandler<UpdateTestCommand, IResult>
        {
            private readonly ITestRepository _testRepository;
            private readonly IMediator _mediator;

            public UpdateTestCommandHandler(ITestRepository testRepository, IMediator mediator)
            {
                _testRepository = testRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateTestValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateTestCommand request, CancellationToken cancellationToken)
            {



                var test = new Test();
                test.Name = request.Name;


                await _testRepository.UpdateAsync(request.Id, test);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

