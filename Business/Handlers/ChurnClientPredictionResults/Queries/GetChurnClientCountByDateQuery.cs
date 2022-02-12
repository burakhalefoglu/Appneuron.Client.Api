using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.ChurnClientPredictionResults.Queries
{
    public class GetChurnClientCountByDateQuery : IRequest<IDataResult<int>>
    {
        public long ProjectId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }

        public class
            GetChurnClientCountByDateQueryHandler : IRequestHandler<GetChurnClientCountByDateQuery, IDataResult<int>>
        {
            private readonly IChurnClientPredictionResultRepository _churnClientPredictionResultRepository;

            public GetChurnClientCountByDateQueryHandler(
                IChurnClientPredictionResultRepository churnClientPredictionResultRepository)
            {
                _churnClientPredictionResultRepository = churnClientPredictionResultRepository;
            }

            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<int>> Handle(GetChurnClientCountByDateQuery request,
                CancellationToken cancellationToken)
            {
                var result = await _churnClientPredictionResultRepository.GetListAsync(
                    c => c.ProjectId == request.ProjectId &&
                         c.ChurnPredictionDate >= request.StartTime &&
                         c.ChurnPredictionDate <= request.FinishTime &&
                         c.Status == true);


                return new SuccessDataResult<int>(
                    result.ToList().Count);
            }
        }
    }
}