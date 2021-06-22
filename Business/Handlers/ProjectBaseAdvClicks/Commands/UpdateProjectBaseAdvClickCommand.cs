
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
using Business.Handlers.ProjectBaseAdvClicks.ValidationRules;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;
using Entities.Concrete.ChartModels.OneToOne;

namespace Business.Handlers.ProjectBaseAdvClicks.Commands
{


    public class UpdateProjectBaseAdvClickCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public AdvClickWithDifficulty[] AdvClickWithDifficulty { get; set; }


        public class UpdateProjectBaseAdvClickCommandHandler : IRequestHandler<UpdateProjectBaseAdvClickCommand, IResult>
        {
            private readonly IProjectBaseAdvClickRepository _projectBaseAdvClickRepository;
            private readonly IMediator _mediator;

            public UpdateProjectBaseAdvClickCommandHandler(IProjectBaseAdvClickRepository projectBaseAdvClickRepository, IMediator mediator)
            {
                _projectBaseAdvClickRepository = projectBaseAdvClickRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateProjectBaseAdvClickValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateProjectBaseAdvClickCommand request, CancellationToken cancellationToken)
            {



                var projectBaseAdvClick = new AdvClick();
                projectBaseAdvClick.ProjectId = request.ProjectId;
                projectBaseAdvClick.AdvClickWithDifficulty = request.AdvClickWithDifficulty;



                await _projectBaseAdvClickRepository.UpdateAsync(request.Id, projectBaseAdvClick);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

