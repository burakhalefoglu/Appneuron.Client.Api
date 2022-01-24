
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
using Business.Handlers.ChurnDates.ValidationRules;
using MongoDB.Bson;

namespace Business.Handlers.ChurnDates.Commands
{


    public class UpdateChurnDateCommand : IRequest<IResult>
    {
        public string ProjectId { get; set; }
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
            [LogAspect(typeof(LogstashLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateChurnDateCommand request, CancellationToken cancellationToken)
            {
                var result = await _churnDateRepository.GetByFilterAsync(c => c.ProjectId == request.ProjectId);
                if (result.ProjectId == null)
                {
                    await _mediator.Send(new CreateChurnDateCommand {
                    
                        churnDateMinutes = request.ChurnDateMinutes,
                        DateTypeOnGui = request.DateTypeOnGui,
                        ProjectId = request.ProjectId
                    });
                    return new SuccessResult(Messages.Added);
                }

                result.ChurnDateMinutes = request.ChurnDateMinutes;
                result.DateTypeOnGui = request.DateTypeOnGui;

                await _churnDateRepository.UpdateAsync(result.Id, result);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

