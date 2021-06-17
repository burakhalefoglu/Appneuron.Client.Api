
using Business.Handlers.PlayerListByDays.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.PlayerListByDays.Queries.GetPlayerListByDayQuery;
using Entities.Concrete;
using static Business.Handlers.PlayerListByDays.Queries.GetPlayerListByDaysQuery;
using static Business.Handlers.PlayerListByDays.Commands.CreatePlayerListByDayCommand;
using Business.Handlers.PlayerListByDays.Commands;
using Business.Constants;
using static Business.Handlers.PlayerListByDays.Commands.UpdatePlayerListByDayCommand;
using static Business.Handlers.PlayerListByDays.Commands.DeletePlayerListByDayCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class PlayerListByDayHandlerTests
    {
        Mock<IPlayerListByDayRepository> _playerListByDayRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _playerListByDayRepository = new Mock<IPlayerListByDayRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task PlayerListByDay_GetQuery_Success()
        {
            //Arrange
            var query = new GetPlayerListByDayQuery();

            _playerListByDayRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new ProjectBasePlayerListByDayWithDifficulty()
//propertyler buraya yazılacak
//{																		
//PlayerListByDayId = 1,
//PlayerListByDayName = "Test"
//}
);

            var handler = new GetPlayerListByDayQueryHandler(_playerListByDayRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.PlayerListByDayId.Should().Be(1);

        }

        [Test]
        public async Task PlayerListByDay_GetQueries_Success()
        {
            //Arrange
            var query = new GetPlayerListByDaysQuery();

            _playerListByDayRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ProjectBasePlayerListByDayWithDifficulty, bool>>>()))
                        .ReturnsAsync(new List<ProjectBasePlayerListByDayWithDifficulty> { new ProjectBasePlayerListByDayWithDifficulty() { /*TODO:propertyler buraya yazılacak PlayerListByDayId = 1, PlayerListByDayName = "test"*/ } }.AsQueryable());

            var handler = new GetPlayerListByDaysQueryHandler(_playerListByDayRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ProjectBasePlayerListByDayWithDifficulty>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task PlayerListByDay_CreateCommand_Success()
        {
            ProjectBasePlayerListByDayWithDifficulty rt = null;
            //Arrange
            var command = new CreatePlayerListByDayCommand();
            //propertyler buraya yazılacak
            //command.PlayerListByDayName = "deneme";

            _playerListByDayRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _playerListByDayRepository.Setup(x => x.Add(It.IsAny<ProjectBasePlayerListByDayWithDifficulty>()));

            var handler = new CreatePlayerListByDayCommandHandler(_playerListByDayRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task PlayerListByDay_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreatePlayerListByDayCommand();
            //propertyler buraya yazılacak 
            //command.PlayerListByDayName = "test";

            _playerListByDayRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ProjectBasePlayerListByDayWithDifficulty, bool>>>()))
                                           .ReturnsAsync(new List<ProjectBasePlayerListByDayWithDifficulty> { new ProjectBasePlayerListByDayWithDifficulty() { /*TODO:propertyler buraya yazılacak PlayerListByDayId = 1, PlayerListByDayName = "test"*/ } }.AsQueryable());

            _playerListByDayRepository.Setup(x => x.Add(It.IsAny<ProjectBasePlayerListByDayWithDifficulty>()));

            var handler = new CreatePlayerListByDayCommandHandler(_playerListByDayRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task PlayerListByDay_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdatePlayerListByDayCommand();
            //command.PlayerListByDayName = "test";

            _playerListByDayRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new ProjectBasePlayerListByDayWithDifficulty() { /*TODO:propertyler buraya yazılacak PlayerListByDayId = 1, PlayerListByDayName = "deneme"*/ });

            _playerListByDayRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<ProjectBasePlayerListByDayWithDifficulty>()));

            var handler = new UpdatePlayerListByDayCommandHandler(_playerListByDayRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task PlayerListByDay_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeletePlayerListByDayCommand();

            _playerListByDayRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new ProjectBasePlayerListByDayWithDifficulty() { /*TODO:propertyler buraya yazılacak PlayerListByDayId = 1, PlayerListByDayName = "deneme"*/});

            _playerListByDayRepository.Setup(x => x.Delete(It.IsAny<ProjectBasePlayerListByDayWithDifficulty>()));

            var handler = new DeletePlayerListByDayCommandHandler(_playerListByDayRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

