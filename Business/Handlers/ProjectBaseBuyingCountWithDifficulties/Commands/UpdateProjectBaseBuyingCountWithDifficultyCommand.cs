
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
using Business.Handlers.ProjectBaseBuyingCountWithDifficulties.ValidationRules;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.ProjectBaseBuyingCountWithDifficulties.Commands
{


    public class UpdateProjectBaseBuyingCountWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public BuyingCountWithDifficulty[] BuyingCountWithDifficulty { get; set; }

        public class UpdateProjectBaseBuyingCountWithDifficultyCommandHandler : IRequestHandler<UpdateProjectBaseBuyingCountWithDifficultyCommand, IResult>
        {
            private readonly IProjectBaseBuyingCountWithDifficultyRepository _projectBaseBuyingCountWithDifficultyRepository;
            private readonly IMediator _mediator;

            public UpdateProjectBaseBuyingCountWithDifficultyCommandHandler(IProjectBaseBuyingCountWithDifficultyRepository projectBaseBuyingCountWithDifficultyRepository, IMediator mediator)
            {
                _projectBaseBuyingCountWithDifficultyRepository = projectBaseBuyingCountWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateProjectBaseBuyingCountWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateProjectBaseBuyingCountWithDifficultyCommand request, CancellationToken cancellationToken)
            {



                var projectBaseBuyingCountWithDifficulty = new ProjectBaseBuyingCountWithDifficulty();
                projectBaseBuyingCountWithDifficulty.ProjectId = request.ProjectId;
                projectBaseBuyingCountWithDifficulty.BuyingCountWithDifficulty = request.BuyingCountWithDifficulty;

                await _projectBaseBuyingCountWithDifficultyRepository.UpdateAsync(request.Id, projectBaseBuyingCountWithDifficulty);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

