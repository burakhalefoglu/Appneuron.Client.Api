using Business.Constants;
using Business.Handlers.BuyingEvents.Commands;
using Business.Handlers.BuyingEvents.Queries;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentAssertions;
using MediatR;
using MongoDB.Bson;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.BuyingEvents.Commands.CreateBuyingEventCommand;
using static Business.Handlers.BuyingEvents.Commands.DeleteBuyingEventCommand;
using static Business.Handlers.BuyingEvents.Commands.UpdateBuyingEventCommand;
using static Business.Handlers.BuyingEvents.Queries.GetBuyingEventQuery;
using static Business.Handlers.BuyingEvents.Queries.GetBuyingEventsQuery;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class BuyingEventHandlerTests
    {
        private Mock<IBuyingEventRepository> _buyingEventRepository;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _buyingEventRepository = new Mock<IBuyingEventRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task BuyingEvent_GetQuery_Success()
        {
            //Arrange
            var query = new GetBuyingEventQuery();

            _buyingEventRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new BuyingEvent()
//propertyler buraya yazılacak
//{
//BuyingEventId = 1,
//BuyingEventName = "Test"
//}
);

            var handler = new GetBuyingEventQueryHandler(_buyingEventRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.BuyingEventId.Should().Be(1);
        }

        [Test]
        public async Task BuyingEvent_GetQueries_Success()
        {
            //Arrange
            var query = new GetBuyingEventsQuery();

            _buyingEventRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<BuyingEvent, bool>>>()))
                        .ReturnsAsync(new List<BuyingEvent> { new BuyingEvent() { /*TODO:propertyler buraya yazılacak BuyingEventId = 1, BuyingEventName = "test"*/ } }.AsQueryable());

            var handler = new GetBuyingEventsQueryHandler(_buyingEventRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<BuyingEvent>)x.Data).Count.Should().BeGreaterThan(1);
        }

        [Test]
        public async Task BuyingEvent_CreateCommand_Success()
        {
            BuyingEvent rt = null;
            //Arrange
            var command = new CreateBuyingEventCommand();
            //propertyler buraya yazılacak
            //command.BuyingEventName = "deneme";

            _buyingEventRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _buyingEventRepository.Setup(x => x.Add(It.IsAny<BuyingEvent>()));

            var handler = new CreateBuyingEventCommandHandler(_buyingEventRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task BuyingEvent_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateBuyingEventCommand();
            //propertyler buraya yazılacak
            //command.BuyingEventName = "test";

            _buyingEventRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<BuyingEvent, bool>>>()))
                                           .ReturnsAsync(new List<BuyingEvent> { new BuyingEvent() { /*TODO:propertyler buraya yazılacak BuyingEventId = 1, BuyingEventName = "test"*/ } }.AsQueryable());

            _buyingEventRepository.Setup(x => x.Add(It.IsAny<BuyingEvent>()));

            var handler = new CreateBuyingEventCommandHandler(_buyingEventRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task BuyingEvent_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateBuyingEventCommand();
            //command.BuyingEventName = "test";

            _buyingEventRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new BuyingEvent() { /*TODO:propertyler buraya yazılacak BuyingEventId = 1, BuyingEventName = "deneme"*/ });

            _buyingEventRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<BuyingEvent>()));

            var handler = new UpdateBuyingEventCommandHandler(_buyingEventRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task BuyingEvent_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteBuyingEventCommand();

            _buyingEventRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new BuyingEvent() { /*TODO:propertyler buraya yazılacak BuyingEventId = 1, BuyingEventName = "deneme"*/});

            _buyingEventRepository.Setup(x => x.Delete(It.IsAny<BuyingEvent>()));

            var handler = new DeleteBuyingEventCommandHandler(_buyingEventRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}