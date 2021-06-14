
using Business.Handlers.AdvEvents.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.AdvEvents.Queries.GetAdvEventQuery;
using Entities.Concrete;
using static Business.Handlers.AdvEvents.Queries.GetAdvEventsQuery;
using static Business.Handlers.AdvEvents.Commands.CreateAdvEventCommand;
using Business.Handlers.AdvEvents.Commands;
using Business.Constants;
using static Business.Handlers.AdvEvents.Commands.UpdateAdvEventCommand;
using static Business.Handlers.AdvEvents.Commands.DeleteAdvEventCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class AdvEventHandlerTests
    {
        Mock<IAdvEventRepository> _advEventRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _advEventRepository = new Mock<IAdvEventRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task AdvEvent_GetQuery_Success()
        {
            //Arrange
            var query = new GetAdvEventQuery();

            _advEventRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new AdvEvent()
//propertyler buraya yazılacak
//{																		
//AdvEventId = 1,
//AdvEventName = "Test"
//}
);

            var handler = new GetAdvEventQueryHandler(_advEventRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.AdvEventId.Should().Be(1);

        }

        [Test]
        public async Task AdvEvent_GetQueries_Success()
        {
            //Arrange
            var query = new GetAdvEventsQuery();

            _advEventRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<AdvEvent, bool>>>()))
                        .ReturnsAsync(new List<AdvEvent> { new AdvEvent() { /*TODO:propertyler buraya yazılacak AdvEventId = 1, AdvEventName = "test"*/ } }.AsQueryable());

            var handler = new GetAdvEventsQueryHandler(_advEventRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<AdvEvent>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task AdvEvent_CreateCommand_Success()
        {
            AdvEvent rt = null;
            //Arrange
            var command = new CreateAdvEventCommand();
            //propertyler buraya yazılacak
            //command.AdvEventName = "deneme";

            _advEventRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _advEventRepository.Setup(x => x.Add(It.IsAny<AdvEvent>()));

            var handler = new CreateAdvEventCommandHandler(_advEventRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task AdvEvent_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateAdvEventCommand();
            //propertyler buraya yazılacak 
            //command.AdvEventName = "test";

            _advEventRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<AdvEvent, bool>>>()))
                                           .ReturnsAsync(new List<AdvEvent> { new AdvEvent() { /*TODO:propertyler buraya yazılacak AdvEventId = 1, AdvEventName = "test"*/ } }.AsQueryable());

            _advEventRepository.Setup(x => x.Add(It.IsAny<AdvEvent>()));

            var handler = new CreateAdvEventCommandHandler(_advEventRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task AdvEvent_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateAdvEventCommand();
            //command.AdvEventName = "test";

            _advEventRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new AdvEvent() { /*TODO:propertyler buraya yazılacak AdvEventId = 1, AdvEventName = "deneme"*/ });

            _advEventRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<AdvEvent>()));

            var handler = new UpdateAdvEventCommandHandler(_advEventRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task AdvEvent_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteAdvEventCommand();

            _advEventRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new AdvEvent() { /*TODO:propertyler buraya yazılacak AdvEventId = 1, AdvEventName = "deneme"*/});

            _advEventRepository.Setup(x => x.Delete(It.IsAny<AdvEvent>()));

            var handler = new DeleteAdvEventCommandHandler(_advEventRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

