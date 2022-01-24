﻿
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;
using System;
using System.Linq;

namespace Business.Handlers.ChurnClientPredictionResults.Queries
{

    public class GetChurnClientCountByDateQuery : IRequest<IDataResult<int>>
    {
        public string ProjectId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }

        public class GetChurnClientCountByDateQueryHandler : IRequestHandler<GetChurnClientCountByDateQuery, IDataResult<int>>
        {
            private readonly IChurnClientPredictionResultRepository _churnClientPredictionResultRepository;
            private readonly IMediator _mediator;

            public GetChurnClientCountByDateQueryHandler(IChurnClientPredictionResultRepository churnClientPredictionResultRepository, IMediator mediator)
            {
                _churnClientPredictionResultRepository = churnClientPredictionResultRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(LogstashLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<int>> Handle(GetChurnClientCountByDateQuery request, CancellationToken cancellationToken)
            {
                var result = await _churnClientPredictionResultRepository.GetListAsync(
                        c => c.ProjectId == request.ProjectId &&
                             c.ChurnPredictionDate >= request.StartTime &&
                             c.ChurnPredictionDate <= request.FinishTime);
               

                return new SuccessDataResult<int>(
                    result.ToList().Count);
            }
        }
    }
}
