using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.ChurnDates.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.ChurnDates.Commands
{
    public class CreateChurnDateCommand : IRequest<IResult>
    {
        public long ProjectId { get; set; }
        public long ChurnDateMinutes { get; set; }
        public string DateTypeOnGui { get; set; }

        public class CreateChurnDateCommandHandler : IRequestHandler<CreateChurnDateCommand, IResult>
        {
            private readonly IChurnDateRepository _churnDateRepository;

            public CreateChurnDateCommandHandler(IChurnDateRepository churnDateRepository)
            {
                _churnDateRepository = churnDateRepository;
            }

            [ValidationAspect(typeof(CreateChurnDateValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateChurnDateCommand request, CancellationToken cancellationToken)
            {
                var isThereChurnDateRecord =
                    await _churnDateRepository.AnyAsync(u => u.ProjectId == request.ProjectId && u.Status == true);

                if (isThereChurnDateRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedChurnDate = new ChurnDate
                {
                    ProjectId = request.ProjectId,
                    ChurnDateMinutes = request.ChurnDateMinutes,
                    DateTypeOnGui = request.DateTypeOnGui
                };

                await _churnDateRepository.AddAsync(addedChurnDate);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}