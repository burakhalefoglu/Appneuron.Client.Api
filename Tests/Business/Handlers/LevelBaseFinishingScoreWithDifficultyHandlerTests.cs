using Business.Constants;
using Business.Handlers.LevelBaseFinishingScoreWithDifficulties.Commands;
using Business.Handlers.LevelBaseFinishingScoreWithDifficulties.Queries;
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
using static Business.Handlers.LevelBaseFinishingScoreWithDifficulties.Commands.CreateLevelBaseFinishingScoreWithDifficultyCommand;
using static Business.Handlers.LevelBaseFinishingScoreWithDifficulties.Commands.DeleteLevelBaseFinishingScoreWithDifficultyCommand;
using static Business.Handlers.LevelBaseFinishingScoreWithDifficulties.Commands.UpdateLevelBaseFinishingScoreWithDifficultyCommand;
using static Business.Handlers.LevelBaseFinishingScoreWithDifficulties.Queries.GetLevelBaseFinishingScoreWithDifficultiesQuery;
using static Business.Handlers.LevelBaseFinishingScoreWithDifficulties.Queries.GetLevelBaseFinishingScoreWithDifficultyQuery;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class LevelBaseFinishingScoreWithDifficultyHandlerTests
    {
        private Mock<ILevelBaseFinishingScoreWithDifficultyRepository> _levelBaseFinishingScoreWithDifficultyRepository;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _levelBaseFinishingScoreWithDifficultyRepository = new Mock<ILevelBaseFinishingScoreWithDifficultyRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task LevelBaseFinishingScoreWithDifficulty_GetQuery_Success()
        {
            //Arrange
            var query = new GetLevelBaseFinishingScoreWithDifficultyQuery();

            _levelBaseFinishingScoreWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new LevelBaseFinishingScoreWithDifficulty()
//propertyler buraya yazılacak
//{
//LevelBaseFinishingScoreWithDifficultyId = 1,
//LevelBaseFinishingScoreWithDifficultyName = "Test"
//}
);

            var handler = new GetLevelBaseFinishingScoreWithDifficultyQueryHandler(_levelBaseFinishingScoreWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.LevelBaseFinishingScoreWithDifficultyId.Should().Be(1);
        }

        [Test]
        public async Task LevelBaseFinishingScoreWithDifficulty_GetQueries_Success()
        {
            //Arrange
            var query = new GetLevelBaseFinishingScoreWithDifficultiesQuery();

            _levelBaseFinishingScoreWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<LevelBaseFinishingScoreWithDifficulty, bool>>>()))
                        .ReturnsAsync(new List<LevelBaseFinishingScoreWithDifficulty> { new LevelBaseFinishingScoreWithDifficulty() { /*TODO:propertyler buraya yazılacak LevelBaseFinishingScoreWithDifficultyId = 1, LevelBaseFinishingScoreWithDifficultyName = "test"*/ } }.AsQueryable());

            var handler = new GetLevelBaseFinishingScoreWithDifficultiesQueryHandler(_levelBaseFinishingScoreWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<LevelBaseFinishingScoreWithDifficulty>)x.Data).Count.Should().BeGreaterThan(1);
        }

        [Test]
        public async Task LevelBaseFinishingScoreWithDifficulty_CreateCommand_Success()
        {
            LevelBaseFinishingScoreWithDifficulty rt = null;
            //Arrange
            var command = new CreateLevelBaseFinishingScoreWithDifficultyCommand();
            //propertyler buraya yazılacak
            //command.LevelBaseFinishingScoreWithDifficultyName = "deneme";

            _levelBaseFinishingScoreWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _levelBaseFinishingScoreWithDifficultyRepository.Setup(x => x.Add(It.IsAny<LevelBaseFinishingScoreWithDifficulty>()));

            var handler = new CreateLevelBaseFinishingScoreWithDifficultyCommandHandler(_levelBaseFinishingScoreWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task LevelBaseFinishingScoreWithDifficulty_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateLevelBaseFinishingScoreWithDifficultyCommand();
            //propertyler buraya yazılacak
            //command.LevelBaseFinishingScoreWithDifficultyName = "test";

            _levelBaseFinishingScoreWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<LevelBaseFinishingScoreWithDifficulty, bool>>>()))
                                           .ReturnsAsync(new List<LevelBaseFinishingScoreWithDifficulty> { new LevelBaseFinishingScoreWithDifficulty() { /*TODO:propertyler buraya yazılacak LevelBaseFinishingScoreWithDifficultyId = 1, LevelBaseFinishingScoreWithDifficultyName = "test"*/ } }.AsQueryable());

            _levelBaseFinishingScoreWithDifficultyRepository.Setup(x => x.Add(It.IsAny<LevelBaseFinishingScoreWithDifficulty>()));

            var handler = new CreateLevelBaseFinishingScoreWithDifficultyCommandHandler(_levelBaseFinishingScoreWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task LevelBaseFinishingScoreWithDifficulty_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateLevelBaseFinishingScoreWithDifficultyCommand();
            //command.LevelBaseFinishingScoreWithDifficultyName = "test";

            _levelBaseFinishingScoreWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new LevelBaseFinishingScoreWithDifficulty() { /*TODO:propertyler buraya yazılacak LevelBaseFinishingScoreWithDifficultyId = 1, LevelBaseFinishingScoreWithDifficultyName = "deneme"*/ });

            _levelBaseFinishingScoreWithDifficultyRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<LevelBaseFinishingScoreWithDifficulty>()));

            var handler = new UpdateLevelBaseFinishingScoreWithDifficultyCommandHandler(_levelBaseFinishingScoreWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task LevelBaseFinishingScoreWithDifficulty_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteLevelBaseFinishingScoreWithDifficultyCommand();

            _levelBaseFinishingScoreWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new LevelBaseFinishingScoreWithDifficulty() { /*TODO:propertyler buraya yazılacak LevelBaseFinishingScoreWithDifficultyId = 1, LevelBaseFinishingScoreWithDifficultyName = "deneme"*/});

            _levelBaseFinishingScoreWithDifficultyRepository.Setup(x => x.Delete(It.IsAny<LevelBaseFinishingScoreWithDifficulty>()));

            var handler = new DeleteLevelBaseFinishingScoreWithDifficultyCommandHandler(_levelBaseFinishingScoreWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}