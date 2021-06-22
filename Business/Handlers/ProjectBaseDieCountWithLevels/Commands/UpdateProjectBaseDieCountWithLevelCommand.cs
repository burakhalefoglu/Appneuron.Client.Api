
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
using Business.Handlers.ProjectBaseDieCountWithLevels.ValidationRules;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.ProjectBaseDieCountWithLevels.Commands
{


    public class UpdateProjectBaseDieCountWithLevelCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public LevelBaseDieCount[] LevelBaseDieCount { get; set; }

        public class UpdateProjectBaseDieCountWithLevelCommandHandler : IRequestHandler<UpdateProjectBaseDieCountWithLevelCommand, IResult>
        {
            private readonly IProjectBaseDieCountWithLevelRepository _projectBaseDieCountWithLevelRepository;
            private readonly IMediator _mediator;

            public UpdateProjectBaseDieCountWithLevelCommandHandler(IProjectBaseDieCountWithLevelRepository projectBaseDieCountWithLevelRepository, IMediator mediator)
            {
                _projectBaseDieCountWithLevelRepository = projectBaseDieCountWithLevelRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateProjectBaseDieCountWithLevelValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateProjectBaseDieCountWithLevelCommand request, CancellationToken cancellationToken)
            {



                var projectBaseDieCountWithLevel = new DieCountWithLevel();
                projectBaseDieCountWithLevel.ProjectId = request.ProjectId;
                projectBaseDieCountWithLevel.LevelBaseDieCount = request.LevelBaseDieCount;

                await _projectBaseDieCountWithLevelRepository.UpdateAsync(request.Id, projectBaseDieCountWithLevel);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

