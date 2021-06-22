
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
using Business.Handlers.Arppus.ValidationRules;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.Arppus.Commands
{


    public class UpdateArppuCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public TotalIncomeAndTotalPaidPlayer[] TotalIncomeAndTotalPaidPlayer { get; set; }

        public class UpdateArppuCommandHandler : IRequestHandler<UpdateArppuCommand, IResult>
        {
            private readonly IArppuRepository _arppuRepository;
            private readonly IMediator _mediator;

            public UpdateArppuCommandHandler(IArppuRepository arppuRepository, IMediator mediator)
            {
                _arppuRepository = arppuRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateArppuValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateArppuCommand request, CancellationToken cancellationToken)
            {



                var arppu = new Arppu();
                arppu.ProjectId = request.ProjectId;
                arppu.TotalIncomeAndTotalPaidPlayer = request.TotalIncomeAndTotalPaidPlayer;

                await _arppuRepository.UpdateAsync(request.Id, arppu);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

