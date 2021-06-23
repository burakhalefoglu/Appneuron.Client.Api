using Business.Constants;
using Business.Handlers.LevelBaseSessionWithDifficulties.Commands;
using Business.Handlers.LevelBaseSessionWithDifficulties.Queries;
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
using static Business.Handlers.LevelBaseSessionWithDifficulties.Commands.CreateLevelBaseSessionWithDifficultyCommand;
using static Business.Handlers.LevelBaseSessionWithDifficulties.Commands.DeleteLevelBaseSessionWithDifficultyCommand;
using static Business.Handlers.LevelBaseSessionWithDifficulties.Commands.UpdateLevelBaseSessionWithDifficultyCommand;
using static Business.Handlers.LevelBaseSessionWithDifficulties.Queries.GetLevelBaseSessionWithDifficultiesQuery;
using static Business.Handlers.LevelBaseSessionWithDifficulties.Queries.GetLevelBaseSessionWithDifficultyQuery;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class LevelBaseSessionWithDifficultyHandlerTests
    {
        private Mock<ILevelBaseSessionWithDifficultyRepository> _levelBaseSessionWithDifficultyRepository;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _levelBaseSessionWithDifficultyRepository = new Mock<ILevelBaseSessionWithDifficultyRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task LevelBaseSessionWithDifficulty_GetQuery_Success()
        {
            //Arrange
            var query = new GetLevelBaseSessionWithDifficultyQuery();

            _levelBaseSessionWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new LevelBaseSessionWithDifficulty()
//propertyler buraya yazılacak
//{
//LevelBaseSessionWithDifficultyId = 1,
//LevelBaseSessionWithDifficultyName = "Test"
//}
);

            var handler = new GetLevelBaseSessionWithDifficultyQueryHandler(_levelBaseSessionWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.LevelBaseSessionWithDifficultyId.Should().Be(1);
        }

        [Test]
        public async Task LevelBaseSessionWithDifficulty_GetQueries_Success()
        {
            //Arrange
            var query = new GetLevelBaseSessionWithDifficultiesQuery();

            _levelBaseSessionWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<LevelBaseSessionWithDifficulty, bool>>>()))
                        .ReturnsAsync(new List<LevelBaseSessionWithDifficulty> { new LevelBaseSessionWithDifficulty() { /*TODO:propertyler buraya yazılacak LevelBaseSessionWithDifficultyId = 1, LevelBaseSessionWithDifficultyName = "test"*/ } }.AsQueryable());

            var handler = new GetLevelBaseSessionWithDifficultiesQueryHandler(_levelBaseSessionWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<LevelBaseSessionWithDifficulty>)x.Data).Count.Should().BeGreaterThan(1);
        }

        [Test]
        public async Task LevelBaseSessionWithDifficulty_CreateCommand_Success()
        {
            LevelBaseSessionWithDifficulty rt = null;
            //Arrange
            var command = new CreateLevelBaseSessionWithDifficultyCommand();
            //propertyler buraya yazılacak
            //command.LevelBaseSessionWithDifficultyName = "deneme";

            _levelBaseSessionWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _levelBaseSessionWithDifficultyRepository.Setup(x => x.Add(It.IsAny<LevelBaseSessionWithDifficulty>()));

            var handler = new CreateLevelBaseSessionWithDifficultyCommandHandler(_levelBaseSessionWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task LevelBaseSessionWithDifficulty_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateLevelBaseSessionWithDifficultyCommand();
            //propertyler buraya yazılacak
            //command.LevelBaseSessionWithDifficultyName = "test";

            _levelBaseSessionWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<LevelBaseSessionWithDifficulty, bool>>>()))
                                           .ReturnsAsync(new List<LevelBaseSessionWithDifficulty> { new LevelBaseSessionWithDifficulty() { /*TODO:propertyler buraya yazılacak LevelBaseSessionWithDifficultyId = 1, LevelBaseSessionWithDifficultyName = "test"*/ } }.AsQueryable());

            _levelBaseSessionWithDifficultyRepository.Setup(x => x.Add(It.IsAny<LevelBaseSessionWithDifficulty>()));

            var handler = new CreateLevelBaseSessionWithDifficultyCommandHandler(_levelBaseSessionWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task LevelBaseSessionWithDifficulty_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateLevelBaseSessionWithDifficultyCommand();
            //command.LevelBaseSessionWithDifficultyName = "test";

            _levelBaseSessionWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new LevelBaseSessionWithDifficulty() { /*TODO:propertyler buraya yazılacak LevelBaseSessionWithDifficultyId = 1, LevelBaseSessionWithDifficultyName = "deneme"*/ });

            _levelBaseSessionWithDifficultyRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<LevelBaseSessionWithDifficulty>()));

            var handler = new UpdateLevelBaseSessionWithDifficultyCommandHandler(_levelBaseSessionWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task LevelBaseSessionWithDifficulty_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteLevelBaseSessionWithDifficultyCommand();

            _levelBaseSessionWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new LevelBaseSessionWithDifficulty() { /*TODO:propertyler buraya yazılacak LevelBaseSessionWithDifficultyId = 1, LevelBaseSessionWithDifficultyName = "deneme"*/});

            _levelBaseSessionWithDifficultyRepository.Setup(x => x.Delete(It.IsAny<LevelBaseSessionWithDifficulty>()));

            var handler = new DeleteLevelBaseSessionWithDifficultyCommandHandler(_levelBaseSessionWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}