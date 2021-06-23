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
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Arpus.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CreateArpuCommand : IRequest<IResult>
    {
        public string ProjectId { get; set; }
        public DateTime DateTime { get; set; }
        public long TotalPlayer { get; set; }
        public long TotalRevenue { get; set; }

        public class CreateArpuCommandHandler : IRequestHandler<CreateArpuCommand, IResult>
        {
            private readonly IArpuRepository _arpuRepository;
            private readonly IMediator _mediator;

            public CreateArpuCommandHandler(IArpuRepository arpuRepository, IMediator mediator)
            {
                _arpuRepository = arpuRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateArpuValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateArpuCommand request, CancellationToken cancellationToken)
            {
                var isThereArpuRecord = _arpuRepository.Any(u => u.ProjectId == request.ProjectId);

                if (isThereArpuRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedArpu = new Arpu
                {
                    ProjectId = request.ProjectId,
                    DateTime = request.DateTime,
                    TotalPlayer = request.TotalPlayer,
                    TotalRevenue = request.TotalRevenue
                };

                await _arpuRepository.AddAsync(addedArpu);

                return new SuccessResult(Messages.Added);
            }
        }
    }
}