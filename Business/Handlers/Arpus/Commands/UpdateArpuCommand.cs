using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.Arpus.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.ChartModels;
using MediatR;
using MongoDB.Bson;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Arpus.Commands
{
    public class UpdateArpuCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public DateTime DateTime { get; set; }
        public long TotalPlayer { get; set; }
        public long TotalRevenue { get; set; }

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
                arpu.DateTime = request.DateTime;
                arpu.TotalPlayer = request.TotalPlayer;
                arpu.TotalRevenue = request.TotalRevenue;

                await _arpuRepository.UpdateAsync(request.Id, arpu);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}