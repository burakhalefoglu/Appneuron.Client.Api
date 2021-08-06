
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers.ApacheKafka;

namespace Business.Handlers.MlResultModels.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteMlResultModelCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteMlResultModelCommandHandler : IRequestHandler<DeleteMlResultModelCommand, IResult>
        {
            private readonly IMlResultModelRepository _mlResultModelRepository;
            private readonly IMediator _mediator;

            public DeleteMlResultModelCommandHandler(IMlResultModelRepository mlResultModelRepository, IMediator mediator)
            {
                _mlResultModelRepository = mlResultModelRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(ApacheKafkaDatabaseActionLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteMlResultModelCommand request, CancellationToken cancellationToken)
            {


                await _mlResultModelRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

