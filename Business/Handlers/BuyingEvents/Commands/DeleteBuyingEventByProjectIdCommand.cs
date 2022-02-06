using System.Linq;
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

namespace Business.Handlers.BuyingEvents.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class DeleteBuyingEventByProjectIdCommand : IRequest<IResult>
    {
        public string ProjectId { get; set; }

        public class DeleteBuyingEventByProjectIdCommandHandler : IRequestHandler<DeleteBuyingEventByProjectIdCommand, IResult>
        {
            private readonly IBuyingEventRepository _buyingEventRepository;

            public DeleteBuyingEventByProjectIdCommandHandler(IBuyingEventRepository buyingEventRepository)
            {
                _buyingEventRepository = buyingEventRepository;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteBuyingEventByProjectIdCommand request, CancellationToken cancellationToken)
            {
                var repos =await _buyingEventRepository.GetListAsync(p => p.ProjectId == request.ProjectId);
                foreach (var buyingEvent in repos.ToList())
                {
                    buyingEvent.Status = false;
                    await _buyingEventRepository.UpdateAsync(buyingEvent, p=>p.ProjectId == request.ProjectId);
  
                }
                

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}