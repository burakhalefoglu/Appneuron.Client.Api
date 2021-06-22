
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

namespace Business.Handlers.ProjectBaseDieCountWithLevels.Queries
{
    public class GetProjectBaseDieCountWithLevelQuery : IRequest<IDataResult<DieCountWithLevel>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetProjectBaseDieCountWithLevelQueryHandler : IRequestHandler<GetProjectBaseDieCountWithLevelQuery, IDataResult<DieCountWithLevel>>
        {
            private readonly IProjectBaseDieCountWithLevelRepository _projectBaseDieCountWithLevelRepository;
            private readonly IMediator _mediator;

            public GetProjectBaseDieCountWithLevelQueryHandler(IProjectBaseDieCountWithLevelRepository projectBaseDieCountWithLevelRepository, IMediator mediator)
            {
                _projectBaseDieCountWithLevelRepository = projectBaseDieCountWithLevelRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<DieCountWithLevel>> Handle(GetProjectBaseDieCountWithLevelQuery request, CancellationToken cancellationToken)
            {
                var projectBaseDieCountWithLevel = await _projectBaseDieCountWithLevelRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<DieCountWithLevel>(projectBaseDieCountWithLevel);
            }
        }
    }
}
