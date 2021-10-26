
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
using Business.Handlers.ChurnClientPredictionResults.ValidationRules;
using MongoDB.Bson;

namespace Business.Handlers.ChurnClientPredictionResults.Commands
{


    public class UpdateChurnClientPredictionResultCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ClientId { get; set; }
        public string ProjectId { get; set; }
        public System.DateTime ChurnPredictionDate { get; set; }

        public class UpdateChurnClientPredictionResultCommandHandler : IRequestHandler<UpdateChurnClientPredictionResultCommand, IResult>
        {
            private readonly IChurnClientPredictionResultRepository _churnClientPredictionResultRepository;
            private readonly IMediator _mediator;

            public UpdateChurnClientPredictionResultCommandHandler(IChurnClientPredictionResultRepository churnClientPredictionResultRepository, IMediator mediator)
            {
                _churnClientPredictionResultRepository = churnClientPredictionResultRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateChurnClientPredictionResultValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateChurnClientPredictionResultCommand request, CancellationToken cancellationToken)
            {



                var churnClientPredictionResult = new ChurnClientPredictionResult();
                churnClientPredictionResult.ClientId = request.ClientId;
                churnClientPredictionResult.ProjectId = request.ProjectId;
                churnClientPredictionResult.ChurnPredictionDate = request.ChurnPredictionDate;


                await _churnClientPredictionResultRepository.UpdateAsync(request.Id, churnClientPredictionResult);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

