
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
using Business.Handlers.MlResultModels.ValidationRules;
using MongoDB.Bson;

namespace Business.Handlers.MlResultModels.Commands
{


    public class UpdateMlResultModelCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public short ProductId { get; set; }
        public string ClientId { get; set; }
        public double ResultValue { get; set; }
        public System.DateTime DateTime { get; set; }

        public class UpdateMlResultModelCommandHandler : IRequestHandler<UpdateMlResultModelCommand, IResult>
        {
            private readonly IMlResultModelRepository _mlResultModelRepository;
            private readonly IMediator _mediator;

            public UpdateMlResultModelCommandHandler(IMlResultModelRepository mlResultModelRepository, IMediator mediator)
            {
                _mlResultModelRepository = mlResultModelRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateMlResultModelValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateMlResultModelCommand request, CancellationToken cancellationToken)
            {



                var mlResultModel = new MlResultModel();
                mlResultModel.ProjectId = request.ProjectId;
                mlResultModel.ProductId = request.ProductId;
                mlResultModel.ClientId = request.ClientId;
                mlResultModel.ResultValue = request.ResultValue;
                mlResultModel.DateTime = request.DateTime;


                await _mlResultModelRepository.UpdateAsync(request.Id, mlResultModel);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

