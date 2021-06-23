using Business.Constants;
using Business.Handlers.LevelBaseDieCountWithDifficulties.Commands;
using Business.Handlers.LevelBaseDieCountWithDifficulties.Queries;
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
using static Business.Handlers.LevelBaseDieCountWithDifficulties.Commands.CreateLevelBaseDieCountWithDifficultyCommand;
using static Business.Handlers.LevelBaseDieCountWithDifficulties.Commands.DeleteLevelBaseDieCountWithDifficultyCommand;
using static Business.Handlers.LevelBaseDieCountWithDifficulties.Commands.UpdateLevelBaseDieCountWithDifficultyCommand;
using static Business.Handlers.LevelBaseDieCountWithDifficulties.Queries.GetLevelBaseDieCountWithDifficultiesQuery;
using static Business.Handlers.LevelBaseDieCountWithDifficulties.Queries.GetLevelBaseDieCountWithDifficultyQuery;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class LevelBaseDieCountWithDifficultyHandlerTests
    {
        private Mock<ILevelBaseDieCountWithDifficultyRepository> _levelBaseDieCountWithDifficultyRepository;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _levelBaseDieCountWithDifficultyRepository = new Mock<ILevelBaseDieCountWithDifficultyRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task LevelBaseDieCountWithDifficulty_GetQuery_Success()
        {
            //Arrange
            var query = new GetLevelBaseDieCountWithDifficultyQuery();

            _levelBaseDieCountWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new LevelBaseDieCountWithDifficulty()
//propertyler buraya yazılacak
//{
//LevelBaseDieCountWithDifficultyId = 1,
//LevelBaseDieCountWithDifficultyName = "Test"
//}
);

            var handler = new GetLevelBaseDieCountWithDifficultyQueryHandler(_levelBaseDieCountWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.LevelBaseDieCountWithDifficultyId.Should().Be(1);
        }

        [Test]
        public async Task LevelBaseDieCountWithDifficulty_GetQueries_Success()
        {
            //Arrange
            var query = new GetLevelBaseDieCountWithDifficultiesQuery();

            _levelBaseDieCountWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<LevelBaseDieCountWithDifficulty, bool>>>()))
                        .ReturnsAsync(new List<LevelBaseDieCountWithDifficulty> { new LevelBaseDieCountWithDifficulty() { /*TODO:propertyler buraya yazılacak LevelBaseDieCountWithDifficultyId = 1, LevelBaseDieCountWithDifficultyName = "test"*/ } }.AsQueryable());

            var handler = new GetLevelBaseDieCountWithDifficultiesQueryHandler(_levelBaseDieCountWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<LevelBaseDieCountWithDifficulty>)x.Data).Count.Should().BeGreaterThan(1);
        }

        [Test]
        public async Task LevelBaseDieCountWithDifficulty_CreateCommand_Success()
        {
            LevelBaseDieCountWithDifficulty rt = null;
            //Arrange
            var command = new CreateLevelBaseDieCountWithDifficultyCommand();
            //propertyler buraya yazılacak
            //command.LevelBaseDieCountWithDifficultyName = "deneme";

            _levelBaseDieCountWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _levelBaseDieCountWithDifficultyRepository.Setup(x => x.Add(It.IsAny<LevelBaseDieCountWithDifficulty>()));

            var handler = new CreateLevelBaseDieCountWithDifficultyCommandHandler(_levelBaseDieCountWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task LevelBaseDieCountWithDifficulty_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateLevelBaseDieCountWithDifficultyCommand();
            //propertyler buraya yazılacak
            //command.LevelBaseDieCountWithDifficultyName = "test";

            _levelBaseDieCountWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<LevelBaseDieCountWithDifficulty, bool>>>()))
                                           .ReturnsAsync(new List<LevelBaseDieCountWithDifficulty> { new LevelBaseDieCountWithDifficulty() { /*TODO:propertyler buraya yazılacak LevelBaseDieCountWithDifficultyId = 1, LevelBaseDieCountWithDifficultyName = "test"*/ } }.AsQueryable());

            _levelBaseDieCountWithDifficultyRepository.Setup(x => x.Add(It.IsAny<LevelBaseDieCountWithDifficulty>()));

            var handler = new CreateLevelBaseDieCountWithDifficultyCommandHandler(_levelBaseDieCountWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task LevelBaseDieCountWithDifficulty_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateLevelBaseDieCountWithDifficultyCommand();
            //command.LevelBaseDieCountWithDifficultyName = "test";

            _levelBaseDieCountWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new LevelBaseDieCountWithDifficulty() { /*TODO:propertyler buraya yazılacak LevelBaseDieCountWithDifficultyId = 1, LevelBaseDieCountWithDifficultyName = "deneme"*/ });

            _levelBaseDieCountWithDifficultyRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<LevelBaseDieCountWithDifficulty>()));

            var handler = new UpdateLevelBaseDieCountWithDifficultyCommandHandler(_levelBaseDieCountWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task LevelBaseDieCountWithDifficulty_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteLevelBaseDieCountWithDifficultyCommand();

            _levelBaseDieCountWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new LevelBaseDieCountWithDifficulty() { /*TODO:propertyler buraya yazılacak LevelBaseDieCountWithDifficultyId = 1, LevelBaseDieCountWithDifficultyName = "deneme"*/});

            _levelBaseDieCountWithDifficultyRepository.Setup(x => x.Delete(It.IsAny<LevelBaseDieCountWithDifficulty>()));

            var handler = new DeleteLevelBaseDieCountWithDifficultyCommandHandler(_levelBaseDieCountWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}