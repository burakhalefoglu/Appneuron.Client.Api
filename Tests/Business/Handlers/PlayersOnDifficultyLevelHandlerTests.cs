
using Business.Handlers.PlayersOnDifficultyLevels.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.PlayersOnDifficultyLevels.Queries.GetPlayersOnDifficultyLevelQuery;
using Entities.Concrete;
using static Business.Handlers.PlayersOnDifficultyLevels.Queries.GetPlayersOnDifficultyLevelsQuery;
using static Business.Handlers.PlayersOnDifficultyLevels.Commands.CreatePlayersOnDifficultyLevelCommand;
using Business.Handlers.PlayersOnDifficultyLevels.Commands;
using Business.Constants;
using static Business.Handlers.PlayersOnDifficultyLevels.Commands.UpdatePlayersOnDifficultyLevelCommand;
using static Business.Handlers.PlayersOnDifficultyLevels.Commands.DeletePlayersOnDifficultyLevelCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class PlayersOnDifficultyLevelHandlerTests
    {
        Mock<IPlayerCountOnDifficultyLevelRepository> _playersOnDifficultyLevelRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _playersOnDifficultyLevelRepository = new Mock<IPlayerCountOnDifficultyLevelRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task PlayersOnDifficultyLevel_GetQuery_Success()
        {
            //Arrange
            var query = new GetPlayersOnDifficultyLevelQuery();

            _playersOnDifficultyLevelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new PlayerCountOnDifficultyLevel()
//propertyler buraya yazılacak
//{																		
//PlayersOnDifficultyLevelId = 1,
//PlayersOnDifficultyLevelName = "Test"
//}
);

            var handler = new GetPlayersOnDifficultyLevelQueryHandler(_playersOnDifficultyLevelRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.PlayersOnDifficultyLevelId.Should().Be(1);

        }

        [Test]
        public async Task PlayersOnDifficultyLevel_GetQueries_Success()
        {
            //Arrange
            var query = new GetPlayersOnDifficultyLevelsQuery();

            _playersOnDifficultyLevelRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<PlayerCountOnDifficultyLevel, bool>>>()))
                        .ReturnsAsync(new List<PlayerCountOnDifficultyLevel> { new PlayerCountOnDifficultyLevel() { /*TODO:propertyler buraya yazılacak PlayersOnDifficultyLevelId = 1, PlayersOnDifficultyLevelName = "test"*/ } }.AsQueryable());

            var handler = new GetPlayersOnDifficultyLevelsQueryHandler(_playersOnDifficultyLevelRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<PlayerCountOnDifficultyLevel>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task PlayersOnDifficultyLevel_CreateCommand_Success()
        {
            PlayerCountOnDifficultyLevel rt = null;
            //Arrange
            var command = new CreatePlayersOnDifficultyLevelCommand();
            //propertyler buraya yazılacak
            //command.PlayersOnDifficultyLevelName = "deneme";

            _playersOnDifficultyLevelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _playersOnDifficultyLevelRepository.Setup(x => x.Add(It.IsAny<PlayerCountOnDifficultyLevel>()));

            var handler = new CreatePlayersOnDifficultyLevelCommandHandler(_playersOnDifficultyLevelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task PlayersOnDifficultyLevel_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreatePlayersOnDifficultyLevelCommand();
            //propertyler buraya yazılacak 
            //command.PlayersOnDifficultyLevelName = "test";

            _playersOnDifficultyLevelRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<PlayerCountOnDifficultyLevel, bool>>>()))
                                           .ReturnsAsync(new List<PlayerCountOnDifficultyLevel> { new PlayerCountOnDifficultyLevel() { /*TODO:propertyler buraya yazılacak PlayersOnDifficultyLevelId = 1, PlayersOnDifficultyLevelName = "test"*/ } }.AsQueryable());

            _playersOnDifficultyLevelRepository.Setup(x => x.Add(It.IsAny<PlayerCountOnDifficultyLevel>()));

            var handler = new CreatePlayersOnDifficultyLevelCommandHandler(_playersOnDifficultyLevelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task PlayersOnDifficultyLevel_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdatePlayersOnDifficultyLevelCommand();
            //command.PlayersOnDifficultyLevelName = "test";

            _playersOnDifficultyLevelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new PlayerCountOnDifficultyLevel() { /*TODO:propertyler buraya yazılacak PlayersOnDifficultyLevelId = 1, PlayersOnDifficultyLevelName = "deneme"*/ });

            _playersOnDifficultyLevelRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<PlayerCountOnDifficultyLevel>()));

            var handler = new UpdatePlayersOnDifficultyLevelCommandHandler(_playersOnDifficultyLevelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task PlayersOnDifficultyLevel_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeletePlayersOnDifficultyLevelCommand();

            _playersOnDifficultyLevelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new PlayerCountOnDifficultyLevel() { /*TODO:propertyler buraya yazılacak PlayersOnDifficultyLevelId = 1, PlayersOnDifficultyLevelName = "deneme"*/});

            _playersOnDifficultyLevelRepository.Setup(x => x.Delete(It.IsAny<PlayerCountOnDifficultyLevel>()));

            var handler = new DeletePlayersOnDifficultyLevelCommandHandler(_playersOnDifficultyLevelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

