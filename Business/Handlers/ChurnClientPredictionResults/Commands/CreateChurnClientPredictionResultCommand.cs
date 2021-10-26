
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
using Business.Handlers.ChurnClientPredictionResults.ValidationRules;

namespace Business.Handlers.ChurnClientPredictionResults.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateChurnClientPredictionResultCommand : IRequest<IResult>
    {

        public string ClientId { get; set; }
        public string ProjectId { get; set; }
        public System.DateTime ChurnPredictionDate { get; set; }


        public class CreateChurnClientPredictionResultCommandHandler : IRequestHandler<CreateChurnClientPredictionResultCommand, IResult>
        {
            private readonly IChurnClientPredictionResultRepository _churnClientPredictionResultRepository;
            private readonly IMediator _mediator;
            public CreateChurnClientPredictionResultCommandHandler(IChurnClientPredictionResultRepository churnClientPredictionResultRepository, IMediator mediator)
            {
                _churnClientPredictionResultRepository = churnClientPredictionResultRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateChurnClientPredictionResultValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateChurnClientPredictionResultCommand request, CancellationToken cancellationToken)
            {
                var isThereChurnClientPredictionResultRecord = _churnClientPredictionResultRepository.Any(u => u.ClientId == request.ClientId);

                if (isThereChurnClientPredictionResultRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedChurnClientPredictionResult = new ChurnClientPredictionResult
                {
                    ClientId = request.ClientId,
                    ProjectId = request.ProjectId,
                    ChurnPredictionDate = request.ChurnPredictionDate,

                };

                await _churnClientPredictionResultRepository.AddAsync(addedChurnClientPredictionResult);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}