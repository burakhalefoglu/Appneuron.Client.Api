using Business.Constants;
using Business.Handlers.AdvClickWithDifficulties.Commands;
using Business.Handlers.AdvClickWithDifficulties.Queries;
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
using static Business.Handlers.AdvClickWithDifficulties.Commands.CreateAdvClickWithDifficultyCommand;
using static Business.Handlers.AdvClickWithDifficulties.Commands.DeleteAdvClickWithDifficultyCommand;
using static Business.Handlers.AdvClickWithDifficulties.Commands.UpdateAdvClickWithDifficultyCommand;
using static Business.Handlers.AdvClickWithDifficulties.Queries.GetAdvClickWithDifficultiesQuery;
using static Business.Handlers.AdvClickWithDifficulties.Queries.GetAdvClickWithDifficultyQuery;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class AdvClickWithDifficultyHandlerTests
    {
        private Mock<IAdvClickWithDifficultyRepository> _advClickWithDifficultyRepository;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _advClickWithDifficultyRepository = new Mock<IAdvClickWithDifficultyRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task AdvClickWithDifficulty_GetQuery_Success()
        {
            //Arrange
            var query = new GetAdvClickWithDifficultyQuery();

            _advClickWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new AdvClickWithDifficulty()
//propertyler buraya yazılacak
//{
//AdvClickWithDifficultyId = 1,
//AdvClickWithDifficultyName = "Test"
//}
);

            var handler = new GetAdvClickWithDifficultyQueryHandler(_advClickWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.AdvClickWithDifficultyId.Should().Be(1);
        }

        [Test]
        public async Task AdvClickWithDifficulty_GetQueries_Success()
        {
            //Arrange
            var query = new GetAdvClickWithDifficultiesQuery();

            _advClickWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<AdvClickWithDifficulty, bool>>>()))
                        .ReturnsAsync(new List<AdvClickWithDifficulty> { new AdvClickWithDifficulty() { /*TODO:propertyler buraya yazılacak AdvClickWithDifficultyId = 1, AdvClickWithDifficultyName = "test"*/ } }.AsQueryable());

            var handler = new GetAdvClickWithDifficultiesQueryHandler(_advClickWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<AdvClickWithDifficulty>)x.Data).Count.Should().BeGreaterThan(1);
        }

        [Test]
        public async Task AdvClickWithDifficulty_CreateCommand_Success()
        {
            AdvClickWithDifficulty rt = null;
            //Arrange
            var command = new CreateAdvClickWithDifficultyCommand();
            //propertyler buraya yazılacak
            //command.AdvClickWithDifficultyName = "deneme";

            _advClickWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _advClickWithDifficultyRepository.Setup(x => x.Add(It.IsAny<AdvClickWithDifficulty>()));

            var handler = new CreateAdvClickWithDifficultyCommandHandler(_advClickWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task AdvClickWithDifficulty_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateAdvClickWithDifficultyCommand();
            //propertyler buraya yazılacak
            //command.AdvClickWithDifficultyName = "test";

            _advClickWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<AdvClickWithDifficulty, bool>>>()))
                                           .ReturnsAsync(new List<AdvClickWithDifficulty> { new AdvClickWithDifficulty() { /*TODO:propertyler buraya yazılacak AdvClickWithDifficultyId = 1, AdvClickWithDifficultyName = "test"*/ } }.AsQueryable());

            _advClickWithDifficultyRepository.Setup(x => x.Add(It.IsAny<AdvClickWithDifficulty>()));

            var handler = new CreateAdvClickWithDifficultyCommandHandler(_advClickWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task AdvClickWithDifficulty_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateAdvClickWithDifficultyCommand();
            //command.AdvClickWithDifficultyName = "test";

            _advClickWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new AdvClickWithDifficulty() { /*TODO:propertyler buraya yazılacak AdvClickWithDifficultyId = 1, AdvClickWithDifficultyName = "deneme"*/ });

            _advClickWithDifficultyRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<AdvClickWithDifficulty>()));

            var handler = new UpdateAdvClickWithDifficultyCommandHandler(_advClickWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task AdvClickWithDifficulty_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteAdvClickWithDifficultyCommand();

            _advClickWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new AdvClickWithDifficulty() { /*TODO:propertyler buraya yazılacak AdvClickWithDifficultyId = 1, AdvClickWithDifficultyName = "deneme"*/});

            _advClickWithDifficultyRepository.Setup(x => x.Delete(It.IsAny<AdvClickWithDifficulty>()));

            var handler = new DeleteAdvClickWithDifficultyCommandHandler(_advClickWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}