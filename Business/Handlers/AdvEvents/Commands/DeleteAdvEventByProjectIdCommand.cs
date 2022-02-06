﻿using System.Linq;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.AdvEvents.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class DeleteAdvEventByProjectIdCommand : IRequest<IResult>
    {
        public string ProjectId { get; set; }

        public class DeleteAdvEventByProjectIdCommandHandler : IRequestHandler<DeleteAdvEventByProjectIdCommand, IResult>
        {
            private readonly IAdvEventRepository _advEventRepository;
            private readonly IMediator _mediator;

            public DeleteAdvEventByProjectIdCommandHandler(IAdvEventRepository advEventRepository, IMediator mediator)
            {
                _advEventRepository = advEventRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteAdvEventByProjectIdCommand request, CancellationToken cancellationToken)
            {
                var repos =await _advEventRepository.GetListAsync(p => p.ProjectId == request.ProjectId);
                foreach (var advEvent in repos.ToList())
                {
                    advEvent.Status = false;
                  await _advEventRepository.UpdateAsync(advEvent, a=>a.ObjectId == advEvent.ObjectId);
  
                }

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}