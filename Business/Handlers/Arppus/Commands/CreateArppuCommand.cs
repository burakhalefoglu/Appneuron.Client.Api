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
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Arppus.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CreateArppuCommand : IRequest<IResult>
    {
        public string ProjectId { get; set; }
        public DateTime DateTime { get; set; }
        public long TotalIncome { get; set; }
        public long TotalIncomePlayer { get; set; }

        public class CreateArppuCommandHandler : IRequestHandler<CreateArppuCommand, IResult>
        {
            private readonly IArppuRepository _arppuRepository;
            private readonly IMediator _mediator;

            public CreateArppuCommandHandler(IArppuRepository arppuRepository, IMediator mediator)
            {
                _arppuRepository = arppuRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateArppuValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateArppuCommand request, CancellationToken cancellationToken)
            {
                var isThereArppuRecord = _arppuRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereArppuRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedArppu = new Arppu
                {
                    ProjectId = request.ProjectId,
                    DateTime = request.DateTime,
                    TotalIncome = request.TotalIncome,
                    TotalIncomePlayer = request.TotalIncomePlayer
                };

                await _arppuRepository.AddAsync(addedArppu);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}