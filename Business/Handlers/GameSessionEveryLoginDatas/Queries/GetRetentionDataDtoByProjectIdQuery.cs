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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.GameSessionEveryLoginDatas.Queries
{
    public class GetRetentionDataDtoByProjectIdQuery : IRequest<IDataResult<RetentionDataWithSessionDto>>
    {
        public string ProjectID { get; set; }

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
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<RetentionDataWithSessionDto>> Handle(GetRetentionDataDtoByProjectIdQuery request, CancellationToken cancellationToken)
            {


                var sessionData = await _gameSessionEveryLoginDataRepository.GetListAsync(p => p.ProjectID == request.ProjectID);
                if(sessionData.Count() == 0)
                    return new ErrorDataResult<RetentionDataWithSessionDto>();

                var minSessionDate = DateTime.Parse(sessionData.ToList().Min(s => s.SessionFinishTime).ToString("dd, MM , yyyy"));
                var maxSessionDate = DateTime.Parse(sessionData.ToList().Max(s => s.SessionFinishTime).ToString("dd, MM , yyyy"));
                var retentionDataList = new List<RetentionDataDto>();

                for (DateTime date = minSessionDate; date <= maxSessionDate; date.AddDays(1))
                {
                    var retentionDataDto = new RetentionDataDto();
                    retentionDataDto.Day = date;

                    foreach (var item in sessionData)
                    {

                        if(item.SessionFinishTime.ToString("dd, MM , yyyy") == date.ToString())
                        {

                            var clientResult = await _mediator.Send(new GetClientByProjectIdInternalQuery
                            {
                                ClientId = item.ClientId,
                                ProjectId = item.ProjectID
                            });

                            _ = retentionDataDto.clientDtoList.Append(new ClientDto
                            {
                                ClientId = item.ClientId,
                                ProjectKey = item.ProjectID,
                                IsPaidClient = clientResult.Data.IsPaidClient

                            });
                        }
                    }
                    retentionDataList.Append(retentionDataDto);
                }

                var retentionDataWithSessionDto = new RetentionDataWithSessionDto();
                retentionDataWithSessionDto.MinSession = minSessionDate;
                retentionDataWithSessionDto.MaxSession = maxSessionDate;
                retentionDataWithSessionDto.RetentionDataDtoList = retentionDataList;

                return new SuccessDataResult<RetentionDataWithSessionDto>(retentionDataWithSessionDto);
            }
        }
    }
}