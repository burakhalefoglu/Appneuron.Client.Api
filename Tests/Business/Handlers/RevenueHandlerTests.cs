
using Business.Handlers.Revenues.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Revenues.Queries.GetRevenueQuery;
using Entities.Concrete;
using static Business.Handlers.Revenues.Queries.GetRevenuesQuery;
using static Business.Handlers.Revenues.Commands.CreateRevenueCommand;
using Business.Handlers.Revenues.Commands;
using Business.Constants;
using static Business.Handlers.Revenues.Commands.UpdateRevenueCommand;
using static Business.Handlers.Revenues.Commands.DeleteRevenueCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class RevenueHandlerTests
    {
        Mock<IRevenueRepository> _revenueRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _revenueRepository = new Mock<IRevenueRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Revenue_GetQuery_Success()
        {
            //Arrange
            var query = new GetRevenueQuery();

            _revenueRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new ProjectBaseRevenue()
//propertyler buraya yazılacak
//{																		
//RevenueId = 1,
//RevenueName = "Test"
//}
);

            var handler = new GetRevenueQueryHandler(_revenueRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.RevenueId.Should().Be(1);

        }

        [Test]
        public async Task Revenue_GetQueries_Success()
        {
            //Arrange
            var query = new GetRevenuesQuery();

            _revenueRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ProjectBaseRevenue, bool>>>()))
                        .ReturnsAsync(new List<ProjectBaseRevenue> { new ProjectBaseRevenue() { /*TODO:propertyler buraya yazılacak RevenueId = 1, RevenueName = "test"*/ } }.AsQueryable());

            var handler = new GetRevenuesQueryHandler(_revenueRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ProjectBaseRevenue>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Revenue_CreateCommand_Success()
        {
            ProjectBaseRevenue rt = null;
            //Arrange
            var command = new CreateRevenueCommand();
            //propertyler buraya yazılacak
            //command.RevenueName = "deneme";

            _revenueRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _revenueRepository.Setup(x => x.Add(It.IsAny<ProjectBaseRevenue>()));

            var handler = new CreateRevenueCommandHandler(_revenueRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Revenue_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateRevenueCommand();
            //propertyler buraya yazılacak 
            //command.RevenueName = "test";

            _revenueRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ProjectBaseRevenue, bool>>>()))
                                           .ReturnsAsync(new List<ProjectBaseRevenue> { new ProjectBaseRevenue() { /*TODO:propertyler buraya yazılacak RevenueId = 1, RevenueName = "test"*/ } }.AsQueryable());

            _revenueRepository.Setup(x => x.Add(It.IsAny<ProjectBaseRevenue>()));

            var handler = new CreateRevenueCommandHandler(_revenueRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Revenue_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateRevenueCommand();
            //command.RevenueName = "test";

            _revenueRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new ProjectBaseRevenue() { /*TODO:propertyler buraya yazılacak RevenueId = 1, RevenueName = "deneme"*/ });

            _revenueRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<ProjectBaseRevenue>()));

            var handler = new UpdateRevenueCommandHandler(_revenueRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Revenue_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteRevenueCommand();

            _revenueRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new ProjectBaseRevenue() { /*TODO:propertyler buraya yazılacak RevenueId = 1, RevenueName = "deneme"*/});

            _revenueRepository.Setup(x => x.Delete(It.IsAny<ProjectBaseRevenue>()));

            var handler = new DeleteRevenueCommandHandler(_revenueRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

