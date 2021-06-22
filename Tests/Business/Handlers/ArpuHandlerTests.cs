
using Business.Handlers.Arpus.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Arpus.Queries.GetArpuQuery;
using Entities.Concrete;
using static Business.Handlers.Arpus.Queries.GetArpusQuery;
using static Business.Handlers.Arpus.Commands.CreateArpuCommand;
using Business.Handlers.Arpus.Commands;
using Business.Constants;
using static Business.Handlers.Arpus.Commands.UpdateArpuCommand;
using static Business.Handlers.Arpus.Commands.DeleteArpuCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ArpuHandlerTests
    {
        Mock<IArpuRepository> _arpuRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _arpuRepository = new Mock<IArpuRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Arpu_GetQuery_Success()
        {
            //Arrange
            var query = new GetArpuQuery();

            _arpuRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new Arpu()
//propertyler buraya yazılacak
//{																		
//ArpuId = 1,
//ArpuName = "Test"
//}
);

            var handler = new GetArpuQueryHandler(_arpuRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ArpuId.Should().Be(1);

        }

        [Test]
        public async Task Arpu_GetQueries_Success()
        {
            //Arrange
            var query = new GetArpusQuery();

            _arpuRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Arpu, bool>>>()))
                        .ReturnsAsync(new List<Arpu> { new Arpu() { /*TODO:propertyler buraya yazılacak ArpuId = 1, ArpuName = "test"*/ } }.AsQueryable());

            var handler = new GetArpusQueryHandler(_arpuRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Arpu>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Arpu_CreateCommand_Success()
        {
            Arpu rt = null;
            //Arrange
            var command = new CreateArpuCommand();
            //propertyler buraya yazılacak
            //command.ArpuName = "deneme";

            _arpuRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _arpuRepository.Setup(x => x.Add(It.IsAny<Arpu>()));

            var handler = new CreateArpuCommandHandler(_arpuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Arpu_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateArpuCommand();
            //propertyler buraya yazılacak 
            //command.ArpuName = "test";

            _arpuRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Arpu, bool>>>()))
                                           .ReturnsAsync(new List<Arpu> { new Arpu() { /*TODO:propertyler buraya yazılacak ArpuId = 1, ArpuName = "test"*/ } }.AsQueryable());

            _arpuRepository.Setup(x => x.Add(It.IsAny<Arpu>()));

            var handler = new CreateArpuCommandHandler(_arpuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Arpu_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateArpuCommand();
            //command.ArpuName = "test";

            _arpuRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new Arpu() { /*TODO:propertyler buraya yazılacak ArpuId = 1, ArpuName = "deneme"*/ });

            _arpuRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<Arpu>()));

            var handler = new UpdateArpuCommandHandler(_arpuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Arpu_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteArpuCommand();

            _arpuRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new Arpu() { /*TODO:propertyler buraya yazılacak ArpuId = 1, ArpuName = "deneme"*/});

            _arpuRepository.Setup(x => x.Delete(It.IsAny<Arpu>()));

            var handler = new DeleteArpuCommandHandler(_arpuRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

