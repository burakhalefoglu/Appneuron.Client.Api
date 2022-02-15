﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.MlResults.Queries;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;

namespace Tests.Business.Handlers
{
    [TestFixture]
    public class MlResultModelHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _mlResultModelRepository = new Mock<IMlResultRepository>();
            _mediator = new Mock<IMediator>();
        }

        private Mock<IMlResultRepository> _mlResultModelRepository;
        private Mock<IMediator> _mediator;


        [Test]
        public async Task MlResultModel_GetQueries_Success()
        {
            //Arrange
            var query = new GetMlResultByProjectAndProductIdQuery
            {
                ProductId = 1,
                ProjectId = 21
            };

            _mlResultModelRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ChurnBlokerMlResult, bool>>>()))
                .ReturnsAsync(new List<ChurnBlokerMlResult>
                {
                    new()
                    {
                        DateTime = new DateTime(),
                        ClientId = 1,
                        Id = 1,
                        ProductId = 1,
                        ProjectId = 21,
                        ModelResult = 1
                    },
                    new()
                    {
                        DateTime = new DateTime(),
                        ClientId = 2,
                        Id = 3,
                        ProductId = 1,
                        ProjectId = 22,
                        ModelResult = 2
                    }
                }.AsQueryable());

            var handler =
                new GetMlResultByProjectAndProductIdQuery.GetMlResultByProjectAndProductIdQueryHandler(
                    _mlResultModelRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            x.Data.ToList().Count.Should().BeGreaterThan(1);
        }
    }
}