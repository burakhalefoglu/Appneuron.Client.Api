
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;
namespace Business.Handlers.ChurnDates.Queries
{

    public class GetChurnDateByProjectIdQuery : IRequest<IDataResult<ChurnDate>>
    {
        public string ProjectId { get; set; }
        public class GetChurnDateByProjectIdQueryHandler : IRequestHandler<GetChurnDateByProjectIdQuery, IDataResult<ChurnDate>>
        {
            private readonly IChurnDateRepository _churnDateRepository;
            private readonly IMediator _mediator;

            public GetChurnDateByProjectIdQueryHandler(IChurnDateRepository churnDateRepository, IMediator mediator)
            {
                _churnDateRepository = churnDateRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ChurnDate>> Handle(GetChurnDateByProjectIdQuery request, CancellationToken cancellationToken)
            {
                var churnDate = await _churnDateRepository
                    .GetByFilterAsync(c => c.ProjectId == request.ProjectId);

                return new SuccessDataResult<ChurnDate>(churnDate);
            }
        }
    }
}
