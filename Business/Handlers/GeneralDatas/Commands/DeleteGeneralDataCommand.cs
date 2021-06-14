﻿
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Business.Handlers.GeneralDatas.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteGeneralDataCommand : IRequest<IResult>
    {
        public string ObjectId { get; set; }
        private ObjectId Id => new ObjectId(this.ObjectId);

        public class DeleteGeneralDataCommandHandler : IRequestHandler<DeleteGeneralDataCommand, IResult>
        {
            private readonly IGeneralDataRepository _generalDataRepository;
            private readonly IMediator _mediator;

            public DeleteGeneralDataCommandHandler(IGeneralDataRepository generalDataRepository, IMediator mediator)
            {
                _generalDataRepository = generalDataRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteGeneralDataCommand request, CancellationToken cancellationToken)
            {


                await _generalDataRepository.DeleteAsync(request.Id);

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

