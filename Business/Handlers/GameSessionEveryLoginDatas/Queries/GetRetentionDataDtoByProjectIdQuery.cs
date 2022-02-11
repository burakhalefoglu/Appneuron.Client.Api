using Business.BusinessAspects;
using Business.Handlers.Clients.Queries;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Internals.Handlers.Clients;

namespace Business.Handlers.GameSessionEveryLoginDatas.Queries
{
    public class GetRetentionDataDtoByProjectIdQuery : IRequest<IDataResult<RetentionDataWithSessionDto>>
    {
        public long ProjectId { get; set; }

        public class GetRetentionDataDtoByProjectIdQueryHandler : IRequestHandler<GetRetentionDataDtoByProjectIdQuery, IDataResult<RetentionDataWithSessionDto>>
        {
            private readonly IGameSessionEveryLoginDataRepository _gameSessionEveryLoginDataRepository;
            private readonly IMediator _mediator;

            public GetRetentionDataDtoByProjectIdQueryHandler(IGameSessionEveryLoginDataRepository gameSessionEveryLoginDataRepository, IMediator mediator)
            {
                _gameSessionEveryLoginDataRepository = gameSessionEveryLoginDataRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(ConsoleLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<RetentionDataWithSessionDto>> Handle(GetRetentionDataDtoByProjectIdQuery request, CancellationToken cancellationToken)
            {


                var sessionData = await _gameSessionEveryLoginDataRepository.GetListAsync(
                    p => p.ProjectId == request.ProjectId && p.Status == true);
                if(!sessionData.Any())
                    return new ErrorDataResult<RetentionDataWithSessionDto>();

                var minSessionDate = DateTime.Parse(sessionData.ToList().Min(s => s.SessionFinishTime).ToString("dd, MM , yyyy"));
                var maxSessionDate = DateTime.Parse(sessionData.ToList().Max(s => s.SessionFinishTime).ToString("dd, MM , yyyy"));
                var retentionDataList = new List<RetentionDataDto>();

                for (DateTime date = minSessionDate; date <= maxSessionDate; date.AddDays(1))
                {
                    var retentionDataDto = new RetentionDataDto
                    {
                        Day = date
                    };

                    foreach (var item in sessionData)
                    {
                        if (item.SessionFinishTime.ToString("dd, MM , yyyy") !=
                            date.ToString(CultureInfo.InvariantCulture)) continue;
                        var clientResult = await _mediator.Send(new GetClientByProjectIdInternalQuery
                        {
                            ClientId = item.ClientId,
                            ProjectId = item.ProjectId
                        }, cancellationToken);

                        _ = retentionDataDto.ClientDtoList.Append(new ChurnClientDto
                        {
                            ClientId = item.ClientId,
                            ProjectId = item.ProjectId,
                            IsPaidClient = clientResult.Data.IsPaidClient

                        });
                    }

                    _ = retentionDataList.Append(retentionDataDto);
                }

                var retentionDataWithSessionDto = new RetentionDataWithSessionDto
                {
                    MinSession = minSessionDate,
                    MaxSession = maxSessionDate,
                    RetentionDataDtoList = retentionDataList
                };

                return new SuccessDataResult<RetentionDataWithSessionDto>(retentionDataWithSessionDto);
            }
        }
    }
}