
using Business.Handlers.PlayersOnLevels.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.PlayersOnLevels.Queries.GetPlayersOnLevelQuery;
using Entities.Concrete;
using static Business.Handlers.PlayersOnLevels.Queries.GetPlayersOnLevelsQuery;
using static Business.Handlers.PlayersOnLevels.Commands.CreatePlayersOnLevelCommand;
using Business.Handlers.PlayersOnLevels.Commands;
using Business.Constants;
using static Business.Handlers.PlayersOnLevels.Commands.UpdatePlayersOnLevelCommand;
using static Business.Handlers.PlayersOnLevels.Commands.DeletePlayersOnLevelCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class PlayersOnLevelHandlerTests
    {
        Mock<IPlayerCountsOnLevelRepository> _playersOnLevelRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _playersOnLevelRepository = new Mock<IPlayerCountsOnLevelRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task PlayersOnLevel_GetQuery_Success()
        {
            //Arrange
            var query = new GetPlayersOnLevelQuery();

            _playersOnLevelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new PlayerCountsOnLevel()
//propertyler buraya yazılacak
//{																		
//PlayersOnLevelId = 1,
//PlayersOnLevelName = "Test"
//}
);

            var handler = new GetPlayersOnLevelQueryHandler(_playersOnLevelRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.PlayersOnLevelId.Should().Be(1);

        }

        [Test]
        public async Task PlayersOnLevel_GetQueries_Success()
        {
            //Arrange
            var query = new GetPlayersOnLevelsQuery();

            _playersOnLevelRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<PlayerCountsOnLevel, bool>>>()))
                        .ReturnsAsync(new List<PlayerCountsOnLevel> { new PlayerCountsOnLevel() { /*TODO:propertyler buraya yazılacak PlayersOnLevelId = 1, PlayersOnLevelName = "test"*/ } }.AsQueryable());

            var handler = new GetPlayersOnLevelsQueryHandler(_playersOnLevelRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<PlayerCountsOnLevel>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task PlayersOnLevel_CreateCommand_Success()
        {
            PlayerCountsOnLevel rt = null;
            //Arrange
            var command = new CreatePlayersOnLevelCommand();
            //propertyler buraya yazılacak
            //command.PlayersOnLevelName = "deneme";

            _playersOnLevelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _playersOnLevelRepository.Setup(x => x.Add(It.IsAny<PlayerCountsOnLevel>()));

            var handler = new CreatePlayersOnLevelCommandHandler(_playersOnLevelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task PlayersOnLevel_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreatePlayersOnLevelCommand();
            //propertyler buraya yazılacak 
            //command.PlayersOnLevelName = "test";

            _playersOnLevelRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<PlayerCountsOnLevel, bool>>>()))
                                           .ReturnsAsync(new List<PlayerCountsOnLevel> { new PlayerCountsOnLevel() { /*TODO:propertyler buraya yazılacak PlayersOnLevelId = 1, PlayersOnLevelName = "test"*/ } }.AsQueryable());

            _playersOnLevelRepository.Setup(x => x.Add(It.IsAny<PlayerCountsOnLevel>()));

            var handler = new CreatePlayersOnLevelCommandHandler(_playersOnLevelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task PlayersOnLevel_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdatePlayersOnLevelCommand();
            //command.PlayersOnLevelName = "test";

            _playersOnLevelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new PlayerCountsOnLevel() { /*TODO:propertyler buraya yazılacak PlayersOnLevelId = 1, PlayersOnLevelName = "deneme"*/ });

            _playersOnLevelRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<PlayerCountsOnLevel>()));

            var handler = new UpdatePlayersOnLevelCommandHandler(_playersOnLevelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task PlayersOnLevel_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeletePlayersOnLevelCommand();

            _playersOnLevelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new PlayerCountsOnLevel() { /*TODO:propertyler buraya yazılacak PlayersOnLevelId = 1, PlayersOnLevelName = "deneme"*/});

            _playersOnLevelRepository.Setup(x => x.Delete(It.IsAny<PlayerCountsOnLevel>()));

            var handler = new DeletePlayersOnLevelCommandHandler(_playersOnLevelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

