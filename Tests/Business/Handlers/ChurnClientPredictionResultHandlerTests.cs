using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Business.Handlers.ChurnClientPredictionResults.Queries;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using static Business.Handlers.ChurnClientPredictionResults.Queries.GetChurnClientCountByDateQuery;

namespace Tests.Business.Handlers
{
    [TestFixture]
    public class ChurnClientPredictionResultHandlerTests
    {
        Mock<IChurnClientPredictionResultRepository> _churnClientPredictionResultRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _churnClientPredictionResultRepository = new Mock<IChurnClientPredictionResultRepository>();
            _mediator = new Mock<IMediator>();
        }

    }
}

