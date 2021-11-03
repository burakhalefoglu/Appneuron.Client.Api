
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;
using Entities.Dtos;
using System;
using System.Linq;

namespace Business.Handlers.ChurnClientPredictionResults.Queries
{

    public class GetChurnClientCountByOfferQuery : IRequest<IDataResult<int>>
    {
        public string ProjectId { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }

        public class GetChurnClientCountByOfferQueryHandler : IRequestHandler<GetChurnClientCountByOfferQuery, IDataResult<int>>
        {
            private readonly IChurnClientPredictionResultRepository _churnClientPredictionResultRepository;
            private readonly IMediator _mediator;

            public GetChurnClientCountByOfferQueryHandler(IChurnClientPredictionResultRepository churnClientPredictionResultRepository, IMediator mediator)
            {
                _churnClientPredictionResultRepository = churnClientPredictionResultRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<int>> Handle(GetChurnClientCountByOfferQuery request, CancellationToken cancellationToken)
            {
                var counter = 0;

                var result = await
                    _churnClientPredictionResultRepository.GetListAsync(
                        c => c.ProjectId == request.ProjectId);

                result.ToList().ForEach(x =>
                {
                    x.ClientsOfferModelDto.ToList().ForEach(c =>
                    {
                        if (c.OfferName == request.Name &&
                            c.Version == request.Version &&
                            c.StartTime >= request.StartTime &&
                            c.FinishTime <= request.FinishTime)
                        {
                            counter += 1;
                        }
                    });

                });

                return new SuccessDataResult<int>(counter);
            }
        }
    }
}
