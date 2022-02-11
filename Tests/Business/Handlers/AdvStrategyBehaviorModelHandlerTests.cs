using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Business.Handlers.AdvStrategyBehaviorModels.Queries;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using static Business.Handlers.AdvStrategyBehaviorModels.Queries.GetAdvStrategyBehaviorCountByAdvStrategyQuery;

namespace Tests.Business.Handlers
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
                                ProjectId = 1,
                                Version = 1,
                                ClientId = 12,
                                Id = 2,
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

