using Business.Constants;
using Business.Handlers.LevelBasePlayerCountWithDifficulties.Commands;
using Business.Handlers.LevelBasePlayerCountWithDifficulties.Queries;
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
using static Business.Handlers.LevelBasePlayerCountWithDifficulties.Commands.CreateLevelBasePlayerCountWithDifficultyCommand;
using static Business.Handlers.LevelBasePlayerCountWithDifficulties.Commands.DeleteLevelBasePlayerCountWithDifficultyCommand;
using static Business.Handlers.LevelBasePlayerCountWithDifficulties.Commands.UpdateLevelBasePlayerCountWithDifficultyCommand;
using static Business.Handlers.LevelBasePlayerCountWithDifficulties.Queries.GetLevelBasePlayerCountWithDifficultiesQuery;
using static Business.Handlers.LevelBasePlayerCountWithDifficulties.Queries.GetLevelBasePlayerCountWithDifficultyQuery;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class LevelBasePlayerCountWithDifficultyHandlerTests
    {
        private Mock<ILevelBasePlayerCountWithDifficultyRepository> _levelBasePlayerCountWithDifficultyRepository;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _levelBasePlayerCountWithDifficultyRepository = new Mock<ILevelBasePlayerCountWithDifficultyRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task LevelBasePlayerCountWithDifficulty_GetQuery_Success()
        {
            //Arrange
            var query = new GetLevelBasePlayerCountWithDifficultyQuery();

            _levelBasePlayerCountWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new LevelBasePlayerCountWithDifficulty()
//propertyler buraya yazılacak
//{
//LevelBasePlayerCountWithDifficultyId = 1,
//LevelBasePlayerCountWithDifficultyName = "Test"
//}
);

            var handler = new GetLevelBasePlayerCountWithDifficultyQueryHandler(_levelBasePlayerCountWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.LevelBasePlayerCountWithDifficultyId.Should().Be(1);
        }

        [Test]
        public async Task LevelBasePlayerCountWithDifficulty_GetQueries_Success()
        {
            //Arrange
            var query = new GetLevelBasePlayerCountWithDifficultiesQuery();

            _levelBasePlayerCountWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<LevelBasePlayerCountWithDifficulty, bool>>>()))
                        .ReturnsAsync(new List<LevelBasePlayerCountWithDifficulty> { new LevelBasePlayerCountWithDifficulty() { /*TODO:propertyler buraya yazılacak LevelBasePlayerCountWithDifficultyId = 1, LevelBasePlayerCountWithDifficultyName = "test"*/ } }.AsQueryable());

            var handler = new GetLevelBasePlayerCountWithDifficultiesQueryHandler(_levelBasePlayerCountWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<LevelBasePlayerCountWithDifficulty>)x.Data).Count.Should().BeGreaterThan(1);
        }

        [Test]
        public async Task LevelBasePlayerCountWithDifficulty_CreateCommand_Success()
        {
            LevelBasePlayerCountWithDifficulty rt = null;
            //Arrange
            var command = new CreateLevelBasePlayerCountWithDifficultyCommand();
            //propertyler buraya yazılacak
            //command.LevelBasePlayerCountWithDifficultyName = "deneme";

            _levelBasePlayerCountWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _levelBasePlayerCountWithDifficultyRepository.Setup(x => x.Add(It.IsAny<LevelBasePlayerCountWithDifficulty>()));

            var handler = new CreateLevelBasePlayerCountWithDifficultyCommandHandler(_levelBasePlayerCountWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task LevelBasePlayerCountWithDifficulty_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateLevelBasePlayerCountWithDifficultyCommand();
            //propertyler buraya yazılacak
            //command.LevelBasePlayerCountWithDifficultyName = "test";

            _levelBasePlayerCountWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<LevelBasePlayerCountWithDifficulty, bool>>>()))
                                           .ReturnsAsync(new List<LevelBasePlayerCountWithDifficulty> { new LevelBasePlayerCountWithDifficulty() { /*TODO:propertyler buraya yazılacak LevelBasePlayerCountWithDifficultyId = 1, LevelBasePlayerCountWithDifficultyName = "test"*/ } }.AsQueryable());

            _levelBasePlayerCountWithDifficultyRepository.Setup(x => x.Add(It.IsAny<LevelBasePlayerCountWithDifficulty>()));

            var handler = new CreateLevelBasePlayerCountWithDifficultyCommandHandler(_levelBasePlayerCountWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task LevelBasePlayerCountWithDifficulty_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateLevelBasePlayerCountWithDifficultyCommand();
            //command.LevelBasePlayerCountWithDifficultyName = "test";

            _levelBasePlayerCountWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new LevelBasePlayerCountWithDifficulty() { /*TODO:propertyler buraya yazılacak LevelBasePlayerCountWithDifficultyId = 1, LevelBasePlayerCountWithDifficultyName = "deneme"*/ });

            _levelBasePlayerCountWithDifficultyRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<LevelBasePlayerCountWithDifficulty>()));

            var handler = new UpdateLevelBasePlayerCountWithDifficultyCommandHandler(_levelBasePlayerCountWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task LevelBasePlayerCountWithDifficulty_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteLevelBasePlayerCountWithDifficultyCommand();

            _levelBasePlayerCountWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new LevelBasePlayerCountWithDifficulty() { /*TODO:propertyler buraya yazılacak LevelBasePlayerCountWithDifficultyId = 1, LevelBasePlayerCountWithDifficultyName = "deneme"*/});

            _levelBasePlayerCountWithDifficultyRepository.Setup(x => x.Delete(It.IsAny<LevelBasePlayerCountWithDifficulty>()));

            var handler = new DeleteLevelBasePlayerCountWithDifficultyCommandHandler(_levelBasePlayerCountWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}