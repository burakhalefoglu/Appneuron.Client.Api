using Business.Constants;
using Business.Handlers.SuccessAttemptRateWithDifficulties.Commands;
using Business.Handlers.SuccessAttemptRateWithDifficulties.Queries;
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
using static Business.Handlers.SuccessAttemptRateWithDifficulties.Commands.CreateSuccessAttemptRateWithDifficultyCommand;
using static Business.Handlers.SuccessAttemptRateWithDifficulties.Commands.DeleteSuccessAttemptRateWithDifficultyCommand;
using static Business.Handlers.SuccessAttemptRateWithDifficulties.Commands.UpdateSuccessAttemptRateWithDifficultyCommand;
using static Business.Handlers.SuccessAttemptRateWithDifficulties.Queries.GetSuccessAttemptRateWithDifficultiesQuery;
using static Business.Handlers.SuccessAttemptRateWithDifficulties.Queries.GetSuccessAttemptRateWithDifficultyQuery;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class SuccessAttemptRateWithDifficultyHandlerTests
    {
        private Mock<ISuccessAttemptRateWithDifficultyRepository> _successAttemptRateWithDifficultyRepository;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _successAttemptRateWithDifficultyRepository = new Mock<ISuccessAttemptRateWithDifficultyRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task SuccessAttemptRateWithDifficulty_GetQuery_Success()
        {
            //Arrange
            var query = new GetSuccessAttemptRateWithDifficultyQuery();

            _successAttemptRateWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new SuccessAttemptRateWithDifficulty()
//propertyler buraya yazılacak
//{
//SuccessAttemptRateWithDifficultyId = 1,
//SuccessAttemptRateWithDifficultyName = "Test"
//}
);

            var handler = new GetSuccessAttemptRateWithDifficultyQueryHandler(_successAttemptRateWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.SuccessAttemptRateWithDifficultyId.Should().Be(1);
        }

        [Test]
        public async Task SuccessAttemptRateWithDifficulty_GetQueries_Success()
        {
            //Arrange
            var query = new GetSuccessAttemptRateWithDifficultiesQuery();

            _successAttemptRateWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<SuccessAttemptRateWithDifficulty, bool>>>()))
                        .ReturnsAsync(new List<SuccessAttemptRateWithDifficulty> { new SuccessAttemptRateWithDifficulty() { /*TODO:propertyler buraya yazılacak SuccessAttemptRateWithDifficultyId = 1, SuccessAttemptRateWithDifficultyName = "test"*/ } }.AsQueryable());

            var handler = new GetSuccessAttemptRateWithDifficultiesQueryHandler(_successAttemptRateWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<SuccessAttemptRateWithDifficulty>)x.Data).Count.Should().BeGreaterThan(1);
        }

        [Test]
        public async Task SuccessAttemptRateWithDifficulty_CreateCommand_Success()
        {
            SuccessAttemptRateWithDifficulty rt = null;
            //Arrange
            var command = new CreateSuccessAttemptRateWithDifficultyCommand();
            //propertyler buraya yazılacak
            //command.SuccessAttemptRateWithDifficultyName = "deneme";

            _successAttemptRateWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _successAttemptRateWithDifficultyRepository.Setup(x => x.Add(It.IsAny<SuccessAttemptRateWithDifficulty>()));

            var handler = new CreateSuccessAttemptRateWithDifficultyCommandHandler(_successAttemptRateWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task SuccessAttemptRateWithDifficulty_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateSuccessAttemptRateWithDifficultyCommand();
            //propertyler buraya yazılacak
            //command.SuccessAttemptRateWithDifficultyName = "test";

            _successAttemptRateWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<SuccessAttemptRateWithDifficulty, bool>>>()))
                                           .ReturnsAsync(new List<SuccessAttemptRateWithDifficulty> { new SuccessAttemptRateWithDifficulty() { /*TODO:propertyler buraya yazılacak SuccessAttemptRateWithDifficultyId = 1, SuccessAttemptRateWithDifficultyName = "test"*/ } }.AsQueryable());

            _successAttemptRateWithDifficultyRepository.Setup(x => x.Add(It.IsAny<SuccessAttemptRateWithDifficulty>()));

            var handler = new CreateSuccessAttemptRateWithDifficultyCommandHandler(_successAttemptRateWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task SuccessAttemptRateWithDifficulty_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateSuccessAttemptRateWithDifficultyCommand();
            //command.SuccessAttemptRateWithDifficultyName = "test";

            _successAttemptRateWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new SuccessAttemptRateWithDifficulty() { /*TODO:propertyler buraya yazılacak SuccessAttemptRateWithDifficultyId = 1, SuccessAttemptRateWithDifficultyName = "deneme"*/ });

            _successAttemptRateWithDifficultyRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<SuccessAttemptRateWithDifficulty>()));

            var handler = new UpdateSuccessAttemptRateWithDifficultyCommandHandler(_successAttemptRateWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task SuccessAttemptRateWithDifficulty_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteSuccessAttemptRateWithDifficultyCommand();

            _successAttemptRateWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new SuccessAttemptRateWithDifficulty() { /*TODO:propertyler buraya yazılacak SuccessAttemptRateWithDifficultyId = 1, SuccessAttemptRateWithDifficultyName = "deneme"*/});

            _successAttemptRateWithDifficultyRepository.Setup(x => x.Delete(It.IsAny<SuccessAttemptRateWithDifficulty>()));

            var handler = new DeleteSuccessAttemptRateWithDifficultyCommandHandler(_successAttemptRateWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}