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
        public string ProjectID { get; set; }

        public class DeleteBuyingEventByProjectIdCommandHandler : IRequestHandler<DeleteBuyingEventByProjectIdCommand, IResult>
        {
            private readonly IBuyingEventRepository _buyingEventRepository;

            public DeleteBuyingEventByProjectIdCommandHandler(IBuyingEventRepository buyingEventRepository)
            {
                _buyingEventRepository = buyingEventRepository;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteBuyingEventByProjectIdCommand request, CancellationToken cancellationToken)
            {
                await _buyingEventRepository.DeleteAsync(p=>p.ProjectID == request.ProjectID);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}