using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.DieCountWithDifficulties.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete.ChartModels;
using MediatR;
using MongoDB.Bson;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.DieCountWithDifficulties.Commands
{
    public class UpdateDieCountWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public int DifficultyLevel { get; set; }
        public long TotalDie { get; set; }
        public DateTime DateTime { get; set; }

        public class UpdateDieCountWithDifficultyCommandHandler : IRequestHandler<UpdateDieCountWithDifficultyCommand, IResult>
        {
            private readonly IDieCountWithDifficultyRepository _dieCountWithDifficultyRepository;
            private readonly IMediator _mediator;

            public UpdateDieCountWithDifficultyCommandHandler(IDieCountWithDifficultyRepository dieCountWithDifficultyRepository, IMediator mediator)
            {
                _dieCountWithDifficultyRepository = dieCountWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateDieCountWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateDieCountWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                var dieCountWithDifficulty = new DieCountWithDifficulty();
                dieCountWithDifficulty.ProjectId = request.ProjectId;
                dieCountWithDifficulty.TotalDie = request.TotalDie;
                dieCountWithDifficulty.DateTime = request.DateTime;
                dieCountWithDifficulty.DifficultyLevel = request.DifficultyLevel;

                await _dieCountWithDifficultyRepository.UpdateAsync(request.Id, dieCountWithDifficulty);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}