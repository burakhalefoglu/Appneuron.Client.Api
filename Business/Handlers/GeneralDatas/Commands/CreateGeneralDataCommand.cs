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
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.GeneralDatas.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CreateGeneralDataCommand : IRequest<IResult>
    {
        public string ClientId { get; set; }
        public string ProjectID { get; set; }
        public string CustomerID { get; set; }
        public int PlayersDifficultylevel { get; set; }

        public class CreateGeneralDataCommandHandler : IRequestHandler<CreateGeneralDataCommand, IResult>
        {
            private readonly IGeneralDataRepository _generalDataRepository;
            private readonly IMediator _mediator;

            public CreateGeneralDataCommandHandler(IGeneralDataRepository generalDataRepository, IMediator mediator)
            {
                _generalDataRepository = generalDataRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateGeneralDataValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateGeneralDataCommand request, CancellationToken cancellationToken)
            {
                var addedGeneralData = new GeneralData
                {
                    ClientId = request.ClientId,
                    ProjectID = request.ProjectID,
                    CustomerID = request.CustomerID,
                    PlayersDifficultylevel = request.PlayersDifficultylevel,
                };

                await _generalDataRepository.AddAsync(addedGeneralData);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}