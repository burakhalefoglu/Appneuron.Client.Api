
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
using Business.Handlers.Arpus.ValidationRules;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.Arpus.Commands
{


    public class UpdateArpuCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public DaliyTotalIncomeAndClientCount[] DaliyTotalIncomeAndClientCount { get; set; }

        public class UpdateArpuCommandHandler : IRequestHandler<UpdateArpuCommand, IResult>
        {
            private readonly IArpuRepository _arpuRepository;
            private readonly IMediator _mediator;

            public UpdateArpuCommandHandler(IArpuRepository arpuRepository, IMediator mediator)
            {
                _arpuRepository = arpuRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateArpuValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateArpuCommand request, CancellationToken cancellationToken)
            {



                var arpu = new Arpu();
                arpu.ProjectId = request.ProjectId;
                arpu.DaliyTotalIncomeAndClientCount = request.DaliyTotalIncomeAndClientCount;

                await _arpuRepository.UpdateAsync(request.Id, arpu);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

