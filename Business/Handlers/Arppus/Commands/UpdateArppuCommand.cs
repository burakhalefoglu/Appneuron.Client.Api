using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.Arppus.ValidationRules;
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

namespace Business.Handlers.Arppus.Commands
{
    public class UpdateArppuCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public DateTime DateTime { get; set; }
        public long TotalIncome { get; set; }
        public long TotalIncomePlayer { get; set; }

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
                arppu.TotalIncomePlayer = request.TotalIncomePlayer;
                arppu.TotalIncome = request.TotalIncome;
                arppu.DateTime = request.DateTime;

                await _arppuRepository.UpdateAsync(request.Id, arppu);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}