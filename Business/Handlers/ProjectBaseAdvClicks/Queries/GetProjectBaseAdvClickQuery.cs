
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;

namespace Business.Handlers.ProjectBaseAdvClicks.Queries
{
    public class GetProjectBaseAdvClickQuery : IRequest<IDataResult<AdvClick>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetProjectBaseAdvClickQueryHandler : IRequestHandler<GetProjectBaseAdvClickQuery, IDataResult<AdvClick>>
        {
            private readonly IProjectBaseAdvClickRepository _projectBaseAdvClickRepository;
            private readonly IMediator _mediator;

            public GetProjectBaseAdvClickQueryHandler(IProjectBaseAdvClickRepository projectBaseAdvClickRepository, IMediator mediator)
            {
                _projectBaseAdvClickRepository = projectBaseAdvClickRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<AdvClick>> Handle(GetProjectBaseAdvClickQuery request, CancellationToken cancellationToken)
            {
                var projectBaseAdvClick = await _projectBaseAdvClickRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<AdvClick>(projectBaseAdvClick);
            }
        }
    }
}
