
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

namespace Business.Handlers.ChurnDates.Queries
{
    public class GetChurnDateQuery : IRequest<IDataResult<ChurnDate>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetChurnDateQueryHandler : IRequestHandler<GetChurnDateQuery, IDataResult<ChurnDate>>
        {
            private readonly IChurnDateRepository _churnDateRepository;
            private readonly IMediator _mediator;

            public GetChurnDateQueryHandler(IChurnDateRepository churnDateRepository, IMediator mediator)
            {
                _churnDateRepository = churnDateRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ChurnDate>> Handle(GetChurnDateQuery request, CancellationToken cancellationToken)
            {
                var churnDate = await _churnDateRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<ChurnDate>(churnDate);
            }
        }
    }
}
