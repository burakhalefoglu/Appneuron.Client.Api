
using Business.Handlers.AdvStrategyBehaviorModels.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.AdvStrategyBehaviorModels.Queries.GetAdvStrategyBehaviorCountByAdvStrategyQuery;
using Entities.Concrete;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class AdvStrategyBehaviorModelHandlerTests
    {
        Mock<IAdvStrategyBehaviorModelRepository> _advStrategyBehaviorModelRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _advStrategyBehaviorModelRepository = new Mock<IAdvStrategyBehaviorModelRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task AdvStrategyBehaviorModel_GetQueries_Success()
        {
            //Arrange
            var query = new GetAdvStrategyBehaviorCountByAdvStrategyQuery();

            _advStrategyBehaviorModelRepository.Setup(x => 
                    x.GetListAsync(It.IsAny<Expression<Func<AdvStrategyBehaviorModel, bool>>>()))
                        .ReturnsAsync(new List<AdvStrategyBehaviorModel>
                        {
                            new AdvStrategyBehaviorModel()
                            {
                                DateTime = DateTime.Now,
                                ProjectId = "afasfdasd",
                                Version = 1,
                                ClientId = "ascdsad",
                                Id = new ObjectId(),
                                Name = "dfsd"
                            }
                        }.AsQueryable());

            var handler = new GetAdvStrategyBehaviorCountByAdvStrategyQueryHandler(_advStrategyBehaviorModelRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            x.Data.Should().Be(1);

        }
    }
}

