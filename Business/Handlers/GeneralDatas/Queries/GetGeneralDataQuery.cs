
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using MongoDB.Bson;

namespace Business.Handlers.GeneralDatas.Queries
{
    public class GetGeneralDataQuery : IRequest<IDataResult<GeneralData>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetGeneralDataQueryHandler : IRequestHandler<GetGeneralDataQuery, IDataResult<GeneralData>>
        {
            private readonly IGeneralDataRepository _generalDataRepository;
            private readonly IMediator _mediator;

            public GetGeneralDataQueryHandler(IGeneralDataRepository generalDataRepository, IMediator mediator)
            {
                _generalDataRepository = generalDataRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<GeneralData>> Handle(GetGeneralDataQuery request, CancellationToken cancellationToken)
            {
                var generalData = await _generalDataRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<GeneralData>(generalData);
            }
        }
    }
}
