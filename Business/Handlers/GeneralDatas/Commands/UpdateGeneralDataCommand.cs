using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.GeneralDatas.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.GeneralDatas.Commands
{
    public class UpdateGeneralDataCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ClientId { get; set; }
        public string ProjectID { get; set; }
        public string CustomerID { get; set; }
        public int PlayersDifficultylevel { get; set; }

        public class UpdateGeneralDataCommandHandler : IRequestHandler<UpdateGeneralDataCommand, IResult>
        {
            private readonly IGeneralDataRepository _generalDataRepository;
            private readonly IMediator _mediator;

            public UpdateGeneralDataCommandHandler(IGeneralDataRepository generalDataRepository, IMediator mediator)
            {
                _generalDataRepository = generalDataRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateGeneralDataValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateGeneralDataCommand request, CancellationToken cancellationToken)
            {
                var generalData = new GeneralData();
                generalData.ClientId = request.ClientId;
                generalData.ProjectID = request.ProjectID;
                generalData.CustomerID = request.CustomerID;
                generalData.PlayersDifficultylevel = request.PlayersDifficultylevel;

                await _generalDataRepository.UpdateAsync(request.Id, generalData);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}