using Business.Constants;
using Business.Handlers.DailySessionWithDifficulties.Commands;
using Business.Handlers.DailySessionWithDifficulties.Queries;
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
using static Business.Handlers.DailySessionWithDifficulties.Commands.CreateDailySessionWithDifficultyCommand;
using static Business.Handlers.DailySessionWithDifficulties.Commands.DeleteDailySessionWithDifficultyCommand;
using static Business.Handlers.DailySessionWithDifficulties.Commands.UpdateDailySessionWithDifficultyCommand;
using static Business.Handlers.DailySessionWithDifficulties.Queries.GetDailySessionWithDifficultiesQuery;
using static Business.Handlers.DailySessionWithDifficulties.Queries.GetDailySessionWithDifficultyQuery;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class DailySessionWithDifficultyHandlerTests
    {
        private Mock<IDailySessionWithDifficultyRepository> _dailySessionWithDifficultyRepository;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _dailySessionWithDifficultyRepository = new Mock<IDailySessionWithDifficultyRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task DailySessionWithDifficulty_GetQuery_Success()
        {
            //Arrange
            var query = new GetDailySessionWithDifficultyQuery();

            _dailySessionWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new DailySessionWithDifficulty()
//propertyler buraya yazılacak
//{
//DailySessionWithDifficultyId = 1,
//DailySessionWithDifficultyName = "Test"
//}
);

            var handler = new GetDailySessionWithDifficultyQueryHandler(_dailySessionWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.DailySessionWithDifficultyId.Should().Be(1);
        }

        [Test]
        public async Task DailySessionWithDifficulty_GetQueries_Success()
        {
            //Arrange
            var query = new GetDailySessionWithDifficultiesQuery();

            _dailySessionWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<DailySessionWithDifficulty, bool>>>()))
                        .ReturnsAsync(new List<DailySessionWithDifficulty> { new DailySessionWithDifficulty() { /*TODO:propertyler buraya yazılacak DailySessionWithDifficultyId = 1, DailySessionWithDifficultyName = "test"*/ } }.AsQueryable());

            var handler = new GetDailySessionWithDifficultiesQueryHandler(_dailySessionWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<DailySessionWithDifficulty>)x.Data).Count.Should().BeGreaterThan(1);
        }

        [Test]
        public async Task DailySessionWithDifficulty_CreateCommand_Success()
        {
            DailySessionWithDifficulty rt = null;
            //Arrange
            var command = new CreateDailySessionWithDifficultyCommand();
            //propertyler buraya yazılacak
            //command.DailySessionWithDifficultyName = "deneme";

            _dailySessionWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _dailySessionWithDifficultyRepository.Setup(x => x.Add(It.IsAny<DailySessionWithDifficulty>()));

            var handler = new CreateDailySessionWithDifficultyCommandHandler(_dailySessionWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task DailySessionWithDifficulty_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateDailySessionWithDifficultyCommand();
            //propertyler buraya yazılacak
            //command.DailySessionWithDifficultyName = "test";

            _dailySessionWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<DailySessionWithDifficulty, bool>>>()))
                                           .ReturnsAsync(new List<DailySessionWithDifficulty> { new DailySessionWithDifficulty() { /*TODO:propertyler buraya yazılacak DailySessionWithDifficultyId = 1, DailySessionWithDifficultyName = "test"*/ } }.AsQueryable());

            _dailySessionWithDifficultyRepository.Setup(x => x.Add(It.IsAny<DailySessionWithDifficulty>()));

            var handler = new CreateDailySessionWithDifficultyCommandHandler(_dailySessionWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task DailySessionWithDifficulty_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateDailySessionWithDifficultyCommand();
            //command.DailySessionWithDifficultyName = "test";

            _dailySessionWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new DailySessionWithDifficulty() { /*TODO:propertyler buraya yazılacak DailySessionWithDifficultyId = 1, DailySessionWithDifficultyName = "deneme"*/ });

            _dailySessionWithDifficultyRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<DailySessionWithDifficulty>()));

            var handler = new UpdateDailySessionWithDifficultyCommandHandler(_dailySessionWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task DailySessionWithDifficulty_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteDailySessionWithDifficultyCommand();

            _dailySessionWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new DailySessionWithDifficulty() { /*TODO:propertyler buraya yazılacak DailySessionWithDifficultyId = 1, DailySessionWithDifficultyName = "deneme"*/});

            _dailySessionWithDifficultyRepository.Setup(x => x.Delete(It.IsAny<DailySessionWithDifficulty>()));

            var handler = new DeleteDailySessionWithDifficultyCommandHandler(_dailySessionWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}