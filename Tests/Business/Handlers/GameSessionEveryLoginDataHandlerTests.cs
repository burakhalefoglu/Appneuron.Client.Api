using Business.Constants;
using Business.Handlers.GameSessionEveryLoginDatas.Commands;
using Business.Handlers.GameSessionEveryLoginDatas.Queries;
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
using static Business.Handlers.GameSessionEveryLoginDatas.Commands.CreateGameSessionEveryLoginDataCommand;
using static Business.Handlers.GameSessionEveryLoginDatas.Commands.DeleteGameSessionEveryLoginDataCommand;
using static Business.Handlers.GameSessionEveryLoginDatas.Commands.UpdateGameSessionEveryLoginDataCommand;
using static Business.Handlers.GameSessionEveryLoginDatas.Queries.GetGameSessionEveryLoginDataQuery;
using static Business.Handlers.GameSessionEveryLoginDatas.Queries.GetGameSessionEveryLoginDatasQuery;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class GameSessionEveryLoginDataHandlerTests
    {
        private Mock<IGameSessionEveryLoginDataRepository> _gameSessionEveryLoginDataRepository;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _gameSessionEveryLoginDataRepository = new Mock<IGameSessionEveryLoginDataRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task GameSessionEveryLoginData_GetQuery_Success()
        {
            //Arrange
            var query = new GetGameSessionEveryLoginDataQuery();

            _gameSessionEveryLoginDataRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new GameSessionEveryLoginData()
//propertyler buraya yazılacak
//{
//GameSessionEveryLoginDataId = 1,
//GameSessionEveryLoginDataName = "Test"
//}
);

            var handler = new GetGameSessionEveryLoginDataQueryHandler(_gameSessionEveryLoginDataRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.GameSessionEveryLoginDataId.Should().Be(1);
        }

        [Test]
        public async Task GameSessionEveryLoginData_GetQueries_Success()
        {
            //Arrange
            var query = new GetGameSessionEveryLoginDatasQuery();

            _gameSessionEveryLoginDataRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<GameSessionEveryLoginData, bool>>>()))
                        .ReturnsAsync(new List<GameSessionEveryLoginData> { new GameSessionEveryLoginData() { /*TODO:propertyler buraya yazılacak GameSessionEveryLoginDataId = 1, GameSessionEveryLoginDataName = "test"*/ } }.AsQueryable());

            var handler = new GetGameSessionEveryLoginDatasQueryHandler(_gameSessionEveryLoginDataRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<GameSessionEveryLoginData>)x.Data).Count.Should().BeGreaterThan(1);
        }

        [Test]
        public async Task GameSessionEveryLoginData_CreateCommand_Success()
        {
            GameSessionEveryLoginData rt = null;
            //Arrange
            var command = new CreateGameSessionEveryLoginDataCommand();
            //propertyler buraya yazılacak
            //command.GameSessionEveryLoginDataName = "deneme";

            _gameSessionEveryLoginDataRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _gameSessionEveryLoginDataRepository.Setup(x => x.Add(It.IsAny<GameSessionEveryLoginData>()));

            var handler = new CreateGameSessionEveryLoginDataCommandHandler(_gameSessionEveryLoginDataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task GameSessionEveryLoginData_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateGameSessionEveryLoginDataCommand();
            //propertyler buraya yazılacak
            //command.GameSessionEveryLoginDataName = "test";

            _gameSessionEveryLoginDataRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<GameSessionEveryLoginData, bool>>>()))
                                           .ReturnsAsync(new List<GameSessionEveryLoginData> { new GameSessionEveryLoginData() { /*TODO:propertyler buraya yazılacak GameSessionEveryLoginDataId = 1, GameSessionEveryLoginDataName = "test"*/ } }.AsQueryable());

            _gameSessionEveryLoginDataRepository.Setup(x => x.Add(It.IsAny<GameSessionEveryLoginData>()));

            var handler = new CreateGameSessionEveryLoginDataCommandHandler(_gameSessionEveryLoginDataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task GameSessionEveryLoginData_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateGameSessionEveryLoginDataCommand();
            //command.GameSessionEveryLoginDataName = "test";

            _gameSessionEveryLoginDataRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new GameSessionEveryLoginData() { /*TODO:propertyler buraya yazılacak GameSessionEveryLoginDataId = 1, GameSessionEveryLoginDataName = "deneme"*/ });

            _gameSessionEveryLoginDataRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<GameSessionEveryLoginData>()));

            var handler = new UpdateGameSessionEveryLoginDataCommandHandler(_gameSessionEveryLoginDataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task GameSessionEveryLoginData_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteGameSessionEveryLoginDataCommand();

            _gameSessionEveryLoginDataRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new GameSessionEveryLoginData() { /*TODO:propertyler buraya yazılacak GameSessionEveryLoginDataId = 1, GameSessionEveryLoginDataName = "deneme"*/});

            _gameSessionEveryLoginDataRepository.Setup(x => x.Delete(It.IsAny<GameSessionEveryLoginData>()));

            var handler = new DeleteGameSessionEveryLoginDataCommandHandler(_gameSessionEveryLoginDataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}