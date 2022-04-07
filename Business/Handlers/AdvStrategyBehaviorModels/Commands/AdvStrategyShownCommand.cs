using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos;
using MediatR;

namespace Business.Handlers.AdvStrategyBehaviorModels.Commands;

public class AdvStrategyShownCommand : IRequest<IDataResult<AdvStrategyShownCountDto>>
{
    public long ProjectId { get; set; }
    public AdvDto[] AdvDtos { get; set; }

    public class AdvStrategyShownCommandHandler : IRequestHandler<AdvStrategyShownCommand,
        IDataResult<AdvStrategyShownCountDto>>
    {
        private readonly IAdvStrategyBehaviorModelRepository _advStrategyBehaviorModelRepository;

        public AdvStrategyShownCommandHandler(IAdvStrategyBehaviorModelRepository advStrategyBehaviorModelRepository)
        {
            _advStrategyBehaviorModelRepository = advStrategyBehaviorModelRepository;
        }

        [PerformanceAspect(5)]
        [CacheAspect(10)]
        [LogAspect(typeof(ConsoleLogger))]
        [SecuredOperation(Priority = 1)]
        public async Task<IDataResult<AdvStrategyShownCountDto>> Handle(AdvStrategyShownCommand request,
            CancellationToken cancellationToken)
        {
            var advDto = new AdvStrategyShownCountDto();

            foreach (var requestAdvDto in request.AdvDtos)
            {
                var advResult = await _advStrategyBehaviorModelRepository.GetListAsync(
                    o => o.ProjectId == request.ProjectId &&
                         o.StrategyId == requestAdvDto.Id);
                var totalOffer = advResult.Count();
                advDto.StrategyNames.Add(requestAdvDto.Name + requestAdvDto.Version);
                advDto.Counts.Add(totalOffer);
            }

            return new SuccessDataResult<AdvStrategyShownCountDto>(advDto);
        }
    }
}