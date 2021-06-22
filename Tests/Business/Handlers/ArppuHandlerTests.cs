
using Business.Handlers.Arppus.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Arppus.Queries.GetArppuQuery;
using Entities.Concrete;
using static Business.Handlers.Arppus.Queries.GetArppusQuery;
using static Business.Handlers.Arppus.Commands.CreateArppuCommand;
using Business.Handlers.Arppus.Commands;
using Business.Constants;
using static Business.Handlers.Arppus.Commands.UpdateArppuCommand;
using static Business.Handlers.Arppus.Commands.DeleteArppuCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ArppuHandlerTests
    {
        Mock<IArppuRepository> _arppuRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _arppuRepository = new Mock<IArppuRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Arppu_GetQuery_Success()
        {
            //Arrange
            var query = new GetArppuQuery();

            _arppuRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new Arppu()
//propertyler buraya yazılacak
//{																		
//ArppuId = 1,
//ArppuName = "Test"
//}
);

            var handler = new GetArppuQueryHandler(_arppuRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ArppuId.Should().Be(1);

        }

        [Test]
        public async Task Arppu_GetQueries_Success()
        {
            //Arrange
            var query = new GetArppusQuery();

            _arppuRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Arppu, bool>>>()))
                        .ReturnsAsync(new List<Arppu> { new Arppu() { /*TODO:propertyler buraya yazılacak ArppuId = 1, ArppuName = "test"*/ } }.AsQueryable());

            var handler = new GetArppusQueryHandler(_arppuRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Arppu>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Arppu_CreateCommand_Success()
        {
            Arppu rt = null;
            //Arrange
            var command = new CreateArppuCommand();
            //propertyler buraya yazılacak
            //command.ArppuName = "deneme";

            _arppuRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _arppuRepository.Setup(x => x.Add(It.IsAny<Arppu>()));

            var handler = new CreateArppuCommandHandler(_arppuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Arppu_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateArppuCommand();
            //propertyler buraya yazılacak 
            //command.ArppuName = "test";

            _arppuRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Arppu, bool>>>()))
                                           .ReturnsAsync(new List<Arppu> { new Arppu() { /*TODO:propertyler buraya yazılacak ArppuId = 1, ArppuName = "test"*/ } }.AsQueryable());

            _arppuRepository.Setup(x => x.Add(It.IsAny<Arppu>()));

            var handler = new CreateArppuCommandHandler(_arppuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Arppu_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateArppuCommand();
            //command.ArppuName = "test";

            _arppuRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new Arppu() { /*TODO:propertyler buraya yazılacak ArppuId = 1, ArppuName = "deneme"*/ });

            _arppuRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<Arppu>()));

            var handler = new UpdateArppuCommandHandler(_arppuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Arppu_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteArppuCommand();

            _arppuRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new Arppu() { /*TODO:propertyler buraya yazılacak ArppuId = 1, ArppuName = "deneme"*/});

            _arppuRepository.Setup(x => x.Delete(It.IsAny<Arppu>()));

            var handler = new DeleteArppuCommandHandler(_arppuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

