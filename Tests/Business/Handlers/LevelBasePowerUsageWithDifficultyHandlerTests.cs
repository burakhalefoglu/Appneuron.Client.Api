using Business.Constants;
using Business.Handlers.LevelBasePowerUsageWithDifficulties.Commands;
using Business.Handlers.LevelBasePowerUsageWithDifficulties.Queries;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.ChartModels;
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
using static Business.Handlers.LevelBasePowerUsageWithDifficulties.Commands.CreateLevelBasePowerUsageWithDifficultyCommand;
using static Business.Handlers.LevelBasePowerUsageWithDifficulties.Commands.DeleteLevelBasePowerUsageWithDifficultyCommand;
using static Business.Handlers.LevelBasePowerUsageWithDifficulties.Commands.UpdateLevelBasePowerUsageWithDifficultyCommand;
using static Business.Handlers.LevelBasePowerUsageWithDifficulties.Queries.GetLevelBasePowerUsageWithDifficultiesQuery;
using static Business.Handlers.LevelBasePowerUsageWithDifficulties.Queries.GetLevelBasePowerUsageWithDifficultyQuery;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class LevelBasePowerUsageWithDifficultyHandlerTests
    {
        private Mock<ILevelBasePowerUsageWithDifficultyRepository> _levelBasePowerUsageWithDifficultyRepository;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _levelBasePowerUsageWithDifficultyRepository = new Mock<ILevelBasePowerUsageWithDifficultyRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task LevelBasePowerUsageWithDifficulty_GetQuery_Success()
        {
            //Arrange
            var query = new GetLevelBasePowerUsageWithDifficultyQuery();

            _levelBasePowerUsageWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new LevelBasePowerUsageWithDifficulty()
//propertyler buraya yazılacak
//{
//LevelBasePowerUsageWithDifficultyId = 1,
//LevelBasePowerUsageWithDifficultyName = "Test"
//}
);

            var handler = new GetLevelBasePowerUsageWithDifficultyQueryHandler(_levelBasePowerUsageWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.LevelBasePowerUsageWithDifficultyId.Should().Be(1);
        }

        [Test]
        public async Task LevelBasePowerUsageWithDifficulty_GetQueries_Success()
        {
            //Arrange
            var query = new GetLevelBasePowerUsageWithDifficultiesQuery();

            _levelBasePowerUsageWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<LevelBasePowerUsageWithDifficulty, bool>>>()))
                        .ReturnsAsync(new List<LevelBasePowerUsageWithDifficulty> { new LevelBasePowerUsageWithDifficulty() { /*TODO:propertyler buraya yazılacak LevelBasePowerUsageWithDifficultyId = 1, LevelBasePowerUsageWithDifficultyName = "test"*/ } }.AsQueryable());

            var handler = new GetLevelBasePowerUsageWithDifficultiesQueryHandler(_levelBasePowerUsageWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<LevelBasePowerUsageWithDifficulty>)x.Data).Count.Should().BeGreaterThan(1);
        }

        [Test]
        public async Task LevelBasePowerUsageWithDifficulty_CreateCommand_Success()
        {
            LevelBasePowerUsageWithDifficulty rt = null;
            //Arrange
            var command = new CreateLevelBasePowerUsageWithDifficultyCommand();
            //propertyler buraya yazılacak
            //command.LevelBasePowerUsageWithDifficultyName = "deneme";

            _levelBasePowerUsageWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _levelBasePowerUsageWithDifficultyRepository.Setup(x => x.Add(It.IsAny<LevelBasePowerUsageWithDifficulty>()));

            var handler = new CreateLevelBasePowerUsageWithDifficultyCommandHandler(_levelBasePowerUsageWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task LevelBasePowerUsageWithDifficulty_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateLevelBasePowerUsageWithDifficultyCommand();
            //propertyler buraya yazılacak
            //command.LevelBasePowerUsageWithDifficultyName = "test";

            _levelBasePowerUsageWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<LevelBasePowerUsageWithDifficulty, bool>>>()))
                                           .ReturnsAsync(new List<LevelBasePowerUsageWithDifficulty> { new LevelBasePowerUsageWithDifficulty() { /*TODO:propertyler buraya yazılacak LevelBasePowerUsageWithDifficultyId = 1, LevelBasePowerUsageWithDifficultyName = "test"*/ } }.AsQueryable());

            _levelBasePowerUsageWithDifficultyRepository.Setup(x => x.Add(It.IsAny<LevelBasePowerUsageWithDifficulty>()));

            var handler = new CreateLevelBasePowerUsageWithDifficultyCommandHandler(_levelBasePowerUsageWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task LevelBasePowerUsageWithDifficulty_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateLevelBasePowerUsageWithDifficultyCommand();
            //command.LevelBasePowerUsageWithDifficultyName = "test";

            _levelBasePowerUsageWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new LevelBasePowerUsageWithDifficulty() { /*TODO:propertyler buraya yazılacak LevelBasePowerUsageWithDifficultyId = 1, LevelBasePowerUsageWithDifficultyName = "deneme"*/ });

            _levelBasePowerUsageWithDifficultyRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<LevelBasePowerUsageWithDifficulty>()));

            var handler = new UpdateLevelBasePowerUsageWithDifficultyCommandHandler(_levelBasePowerUsageWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task LevelBasePowerUsageWithDifficulty_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteLevelBasePowerUsageWithDifficultyCommand();

            _levelBasePowerUsageWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new LevelBasePowerUsageWithDifficulty() { /*TODO:propertyler buraya yazılacak LevelBasePowerUsageWithDifficultyId = 1, LevelBasePowerUsageWithDifficultyName = "deneme"*/});

            _levelBasePowerUsageWithDifficultyRepository.Setup(x => x.Delete(It.IsAny<LevelBasePowerUsageWithDifficulty>()));

            var handler = new DeleteLevelBasePowerUsageWithDifficultyCommandHandler(_levelBasePowerUsageWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}