using Business.Constants;
using Business.Handlers.EveryLoginLevelDatas.Commands;
using Business.Handlers.EveryLoginLevelDatas.Queries;
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
using static Business.Handlers.EveryLoginLevelDatas.Commands.CreateEveryLoginLevelDataCommand;
using static Business.Handlers.EveryLoginLevelDatas.Commands.DeleteEveryLoginLevelDataCommand;
using static Business.Handlers.EveryLoginLevelDatas.Commands.UpdateEveryLoginLevelDataCommand;
using static Business.Handlers.EveryLoginLevelDatas.Queries.GetEveryLoginLevelDataQuery;
using static Business.Handlers.EveryLoginLevelDatas.Queries.GetEveryLoginLevelDatasQuery;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class EveryLoginLevelDataHandlerTests
    {
        private Mock<IEveryLoginLevelDataRepository> _everyLoginLevelDataRepository;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _everyLoginLevelDataRepository = new Mock<IEveryLoginLevelDataRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task EveryLoginLevelData_GetQuery_Success()
        {
            //Arrange
            var query = new GetEveryLoginLevelDataQuery();

            _everyLoginLevelDataRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new EveryLoginLevelData()
//propertyler buraya yazılacak
//{
//EveryLoginLevelDataId = 1,
//EveryLoginLevelDataName = "Test"
//}
);

            var handler = new GetEveryLoginLevelDataQueryHandler(_everyLoginLevelDataRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.EveryLoginLevelDataId.Should().Be(1);
        }

        [Test]
        public async Task EveryLoginLevelData_GetQueries_Success()
        {
            //Arrange
            var query = new GetEveryLoginLevelDatasQuery();

            _everyLoginLevelDataRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<EveryLoginLevelData, bool>>>()))
                        .ReturnsAsync(new List<EveryLoginLevelData> { new EveryLoginLevelData() { /*TODO:propertyler buraya yazılacak EveryLoginLevelDataId = 1, EveryLoginLevelDataName = "test"*/ } }.AsQueryable());

            var handler = new GetEveryLoginLevelDatasQueryHandler(_everyLoginLevelDataRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<EveryLoginLevelData>)x.Data).Count.Should().BeGreaterThan(1);
        }

        [Test]
        public async Task EveryLoginLevelData_CreateCommand_Success()
        {
            EveryLoginLevelData rt = null;
            //Arrange
            var command = new CreateEveryLoginLevelDataCommand();
            //propertyler buraya yazılacak
            //command.EveryLoginLevelDataName = "deneme";

            _everyLoginLevelDataRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _everyLoginLevelDataRepository.Setup(x => x.Add(It.IsAny<EveryLoginLevelData>()));

            var handler = new CreateEveryLoginLevelDataCommandHandler(_everyLoginLevelDataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task EveryLoginLevelData_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateEveryLoginLevelDataCommand();
            //propertyler buraya yazılacak
            //command.EveryLoginLevelDataName = "test";

            _everyLoginLevelDataRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<EveryLoginLevelData, bool>>>()))
                                           .ReturnsAsync(new List<EveryLoginLevelData> { new EveryLoginLevelData() { /*TODO:propertyler buraya yazılacak EveryLoginLevelDataId = 1, EveryLoginLevelDataName = "test"*/ } }.AsQueryable());

            _everyLoginLevelDataRepository.Setup(x => x.Add(It.IsAny<EveryLoginLevelData>()));

            var handler = new CreateEveryLoginLevelDataCommandHandler(_everyLoginLevelDataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task EveryLoginLevelData_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateEveryLoginLevelDataCommand();
            //command.EveryLoginLevelDataName = "test";

            _everyLoginLevelDataRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new EveryLoginLevelData() { /*TODO:propertyler buraya yazılacak EveryLoginLevelDataId = 1, EveryLoginLevelDataName = "deneme"*/ });

            _everyLoginLevelDataRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<EveryLoginLevelData>()));

            var handler = new UpdateEveryLoginLevelDataCommandHandler(_everyLoginLevelDataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task EveryLoginLevelData_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteEveryLoginLevelDataCommand();

            _everyLoginLevelDataRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new EveryLoginLevelData() { /*TODO:propertyler buraya yazılacak EveryLoginLevelDataId = 1, EveryLoginLevelDataName = "deneme"*/});

            _everyLoginLevelDataRepository.Setup(x => x.Delete(It.IsAny<EveryLoginLevelData>()));

            var handler = new DeleteEveryLoginLevelDataCommandHandler(_everyLoginLevelDataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}