using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Tests.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class DeleteTestCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteTestCommandHandler : IRequestHandler<DeleteTestCommand, IResult>
        {
            private readonly ITestRepository _testRepository;
            private readonly IMediator _mediator;

            public DeleteTestCommandHandler(ITestRepository testRepository, IMediator mediator)
            {
                _testRepository = testRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteTestCommand request, CancellationToken cancellationToken)
            {
                await _testRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}