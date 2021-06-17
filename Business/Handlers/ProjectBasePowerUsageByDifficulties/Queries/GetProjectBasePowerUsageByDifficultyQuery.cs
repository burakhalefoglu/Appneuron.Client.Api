﻿
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

namespace Business.Handlers.ProjectBasePowerUsageByDifficulties.Queries
{
    public class GetProjectBasePowerUsageByDifficultyQuery : IRequest<IDataResult<ProjectBasePowerUsageByDifficulty>>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class GetProjectBasePowerUsageByDifficultyQueryHandler : IRequestHandler<GetProjectBasePowerUsageByDifficultyQuery, IDataResult<ProjectBasePowerUsageByDifficulty>>
        {
            private readonly IProjectBasePowerUsageByDifficultyRepository _projectBasePowerUsageByDifficultyRepository;
            private readonly IMediator _mediator;

            public GetProjectBasePowerUsageByDifficultyQueryHandler(IProjectBasePowerUsageByDifficultyRepository projectBasePowerUsageByDifficultyRepository, IMediator mediator)
            {
                _projectBasePowerUsageByDifficultyRepository = projectBasePowerUsageByDifficultyRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ProjectBasePowerUsageByDifficulty>> Handle(GetProjectBasePowerUsageByDifficultyQuery request, CancellationToken cancellationToken)
            {
                var projectBasePowerUsageByDifficulty = await _projectBasePowerUsageByDifficultyRepository.GetByIdAsync(request.Id);
                return new SuccessDataResult<ProjectBasePowerUsageByDifficulty>(projectBasePowerUsageByDifficulty);
            }
        }
    }
}
