
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
using Business.Handlers.ChurnDates.ValidationRules;

namespace Business.Handlers.ChurnDates.Commands
{
    public class CreateChurnDateCommand : IRequest<IResult>
    {
        public string ProjectId { get; set; }
        public long churnDateMinutes { get; set; }
        public string DateTypeOnGui { get; set; }

        public class CreateChurnDateCommandHandler : IRequestHandler<CreateChurnDateCommand, IResult>
        {
            private readonly IChurnDateRepository _churnDateRepository;
            private readonly IMediator _mediator;
            public CreateChurnDateCommandHandler(IChurnDateRepository churnDateRepository, IMediator mediator)
            {
                _churnDateRepository = churnDateRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateChurnDateValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(LogstashLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateChurnDateCommand request, CancellationToken cancellationToken)
            {
                var isThereChurnDateRecord = _churnDateRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereChurnDateRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedChurnDate = new ChurnDate
                {
                    ProjectId = request.ProjectId,
                    ChurnDateMinutes = request.churnDateMinutes,
                    DateTypeOnGui = request.DateTypeOnGui
                };

                await _churnDateRepository.AddAsync(addedChurnDate);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}