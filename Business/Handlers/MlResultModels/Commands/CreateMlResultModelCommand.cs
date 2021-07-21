
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
using Business.Handlers.MlResultModels.ValidationRules;

namespace Business.Handlers.MlResultModels.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateMlResultModelCommand : IRequest<IResult>
    {

        public string ProjectId { get; set; }
        public string ProductId { get; set; }
        public string ClientId { get; set; }
        public double ResultValue { get; set; }
        public System.DateTime DateTime { get; set; }


        public class CreateMlResultModelCommandHandler : IRequestHandler<CreateMlResultModelCommand, IResult>
        {
            private readonly IMlResultModelRepository _mlResultModelRepository;
            private readonly IMediator _mediator;
            public CreateMlResultModelCommandHandler(IMlResultModelRepository mlResultModelRepository, IMediator mediator)
            {
                _mlResultModelRepository = mlResultModelRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateMlResultModelValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateMlResultModelCommand request, CancellationToken cancellationToken)
            {
                var isThereMlResultModelRecord = _mlResultModelRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereMlResultModelRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedMlResultModel = new MlResultModel
                {
                    ProjectId = request.ProjectId,
                    ProductId = request.ProductId,
                    ClientId = request.ClientId,
                    ResultValue = request.ResultValue,
                    DateTime = request.DateTime,

                };

                await _mlResultModelRepository.AddAsync(addedMlResultModel);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}