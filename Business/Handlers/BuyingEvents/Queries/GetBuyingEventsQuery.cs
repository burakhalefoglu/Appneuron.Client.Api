﻿using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.BuyingEvents.Queries
{
    public class GetBuyingEventsQuery : IRequest<IDataResult<IEnumerable<BuyingEvent>>>
    {
        public class GetBuyingEventsQueryHandler : IRequestHandler<GetBuyingEventsQuery, IDataResult<IEnumerable<BuyingEvent>>>
        {
            private readonly IBuyingEventRepository _buyingEventRepository;
            private readonly IMediator _mediator;

            public GetBuyingEventsQueryHandler(IBuyingEventRepository buyingEventRepository, IMediator mediator)
            {
                _buyingEventRepository = buyingEventRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<BuyingEvent>>> Handle(GetBuyingEventsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<BuyingEvent>>(await _buyingEventRepository.GetListAsync());
            }
        }
    }
}