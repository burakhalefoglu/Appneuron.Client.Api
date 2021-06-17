﻿
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;
using Entities.Concrete.ChartModels;

namespace Business.Handlers.PlayersOnLevels.Queries
{

    public class GetPlayersOnLevelsQuery : IRequest<IDataResult<IEnumerable<ProjectBasePlayerCountsOnLevel>>>
    {
        public class GetPlayersOnLevelsQueryHandler : IRequestHandler<GetPlayersOnLevelsQuery, IDataResult<IEnumerable<ProjectBasePlayerCountsOnLevel>>>
        {
            private readonly IPlayersOnLevelRepository _playersOnLevelRepository;
            private readonly IMediator _mediator;

            public GetPlayersOnLevelsQueryHandler(IPlayersOnLevelRepository playersOnLevelRepository, IMediator mediator)
            {
                _playersOnLevelRepository = playersOnLevelRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ProjectBasePlayerCountsOnLevel>>> Handle(GetPlayersOnLevelsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ProjectBasePlayerCountsOnLevel>>(await _playersOnLevelRepository.GetListAsync());
            }
        }
    }
}