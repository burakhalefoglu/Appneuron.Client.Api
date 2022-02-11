
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.ChurnDates.ValidationRules;

namespace Business.Handlers.ChurnDates.Commands
{


    public class UpdateChurnDateCommand : IRequest<IResult>
    {
        public long ProjectId { get; set; }
        public int ChurnDateMinutes { get; set; }
        public string DateTypeOnGui { get; set; }

        public class UpdateChurnDateCommandHandler : IRequestHandler<UpdateChurnDateCommand, IResult>
        {
            private readonly IChurnDateRepository _churnDateRepository;
            private readonly IMediator _mediator;

            public UpdateChurnDateCommandHandler(IChurnDateRepository churnDateRepository, IMediator mediator)
            {
                _churnDateRepository = churnDateRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateChurnDateValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateChurnDateCommand request, CancellationToken cancellationToken)
            {
                var result = await _churnDateRepository.GetAsync(c => c.ProjectId == request.ProjectId && c.Status == true);
                if (result is null)
                {
                    await _mediator.Send(new CreateChurnDateCommand {
                    
                        ChurnDateMinutes = request.ChurnDateMinutes,
                        DateTypeOnGui = request.DateTypeOnGui,
                        ProjectId = request.ProjectId
                    }, cancellationToken);
                    return new SuccessResult(Messages.Added);
                }

                result.ChurnDateMinutes = request.ChurnDateMinutes;
                result.DateTypeOnGui = request.DateTypeOnGui;

                await _churnDateRepository.UpdateAsync(result);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

