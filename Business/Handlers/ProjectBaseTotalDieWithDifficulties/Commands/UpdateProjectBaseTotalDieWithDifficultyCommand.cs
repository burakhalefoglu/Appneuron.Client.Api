
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
using Business.Handlers.ProjectBaseTotalDieWithDifficulties.ValidationRules;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.ProjectBaseTotalDieWithDifficulties.Commands
{


    public class UpdateProjectBaseTotalDieWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public TotalDieWithDifficulty[] TotalDieWithDifficulty { get; set; }


        public class UpdateProjectBaseTotalDieWithDifficultyCommandHandler : IRequestHandler<UpdateProjectBaseTotalDieWithDifficultyCommand, IResult>
        {
            private readonly IProjectBaseTotalDieWithDifficultyRepository _projectBaseTotalDieWithDifficultyRepository;
            private readonly IMediator _mediator;

            public UpdateProjectBaseTotalDieWithDifficultyCommandHandler(IProjectBaseTotalDieWithDifficultyRepository projectBaseTotalDieWithDifficultyRepository, IMediator mediator)
            {
                _projectBaseTotalDieWithDifficultyRepository = projectBaseTotalDieWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateProjectBaseTotalDieWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateProjectBaseTotalDieWithDifficultyCommand request, CancellationToken cancellationToken)
            {



                var projectBaseTotalDieWithDifficulty = new ProjectBaseTotalDieWithDifficulty();
                projectBaseTotalDieWithDifficulty.ProjectId = request.ProjectId;
                projectBaseTotalDieWithDifficulty.TotalDieWithDifficulty = request.TotalDieWithDifficulty;


                await _projectBaseTotalDieWithDifficultyRepository.UpdateAsync(request.Id, projectBaseTotalDieWithDifficulty);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

