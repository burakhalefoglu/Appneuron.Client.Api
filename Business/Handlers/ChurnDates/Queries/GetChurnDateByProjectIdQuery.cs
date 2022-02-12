using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.ChurnDates.Queries
{
    public class GetChurnDateByProjectIdQuery : IRequest<IDataResult<ChurnDate>>
    {
        public long ProjectId { get; set; }

        public class
            GetChurnDateByProjectIdQueryHandler : IRequestHandler<GetChurnDateByProjectIdQuery, IDataResult<ChurnDate>>
        {
            private readonly IChurnDateRepository _churnDateRepository;
            private readonly IMediator _mediator;

            public GetChurnDateByProjectIdQueryHandler(IChurnDateRepository churnDateRepository, IMediator mediator)
            {
                _churnDateRepository = churnDateRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ChurnDate>> Handle(GetChurnDateByProjectIdQuery request,
                CancellationToken cancellationToken)
            {
                var churnDate = await _churnDateRepository
                    .GetAsync(c => c.ProjectId == request.ProjectId && c.Status == true);

                return new SuccessDataResult<ChurnDate>(churnDate);
            }
        }
    }
}