using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.AdvClickWithDifficulties.ValidationRules;
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

namespace Business.Handlers.AdvClickWithDifficulties.Commands
{
    public class UpdateAdvClickWithDifficultyCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);
        public string ProjectId { get; set; }
        public int DifficultyLevel { get; set; }
        public long AdvClick { get; set; }
        public DateTime DateTime { get; set; }

        public class UpdateAdvClickWithDifficultyCommandHandler : IRequestHandler<UpdateAdvClickWithDifficultyCommand, IResult>
        {
            private readonly IAdvClickWithDifficultyRepository _advClickWithDifficultyRepository;
            private readonly IMediator _mediator;

            public UpdateAdvClickWithDifficultyCommandHandler(IAdvClickWithDifficultyRepository advClickWithDifficultyRepository, IMediator mediator)
            {
                _advClickWithDifficultyRepository = advClickWithDifficultyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateAdvClickWithDifficultyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateAdvClickWithDifficultyCommand request, CancellationToken cancellationToken)
            {
                var advClickWithDifficulty = new AdvClickWithDifficulty();
                advClickWithDifficulty.ProjectId = request.ProjectId;
                advClickWithDifficulty.AdvClick = request.AdvClick;
                advClickWithDifficulty.DateTime = request.DateTime;
                advClickWithDifficulty.DifficultyLevel = request.DifficultyLevel;

                await _advClickWithDifficultyRepository.UpdateAsync(request.Id, advClickWithDifficulty);

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}