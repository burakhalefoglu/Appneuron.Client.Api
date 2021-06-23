using Business.Constants;
using Business.Handlers.PowerUsageWithDifficulties.Commands;
using Business.Handlers.PowerUsageWithDifficulties.Queries;
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
using static Business.Handlers.PowerUsageWithDifficulties.Commands.CreatePowerUsageWithDifficultyCommand;
using static Business.Handlers.PowerUsageWithDifficulties.Commands.DeletePowerUsageWithDifficultyCommand;
using static Business.Handlers.PowerUsageWithDifficulties.Commands.UpdatePowerUsageWithDifficultyCommand;
using static Business.Handlers.PowerUsageWithDifficulties.Queries.GetPowerUsageWithDifficultiesQuery;
using static Business.Handlers.PowerUsageWithDifficulties.Queries.GetPowerUsageWithDifficultyQuery;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class PowerUsageWithDifficultyHandlerTests
    {
        private Mock<IPowerUsageWithDifficultyRepository> _powerUsageWithDifficultyRepository;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _powerUsageWithDifficultyRepository = new Mock<IPowerUsageWithDifficultyRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task PowerUsageWithDifficulty_GetQuery_Success()
        {
            //Arrange
            var query = new GetPowerUsageWithDifficultyQuery();

            _powerUsageWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new PowerUsageWithDifficulty()
//propertyler buraya yazılacak
//{
//PowerUsageWithDifficultyId = 1,
//PowerUsageWithDifficultyName = "Test"
//}
);

            var handler = new GetPowerUsageWithDifficultyQueryHandler(_powerUsageWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.PowerUsageWithDifficultyId.Should().Be(1);
        }

        [Test]
        public async Task PowerUsageWithDifficulty_GetQueries_Success()
        {
            //Arrange
            var query = new GetPowerUsageWithDifficultiesQuery();

            _powerUsageWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<PowerUsageWithDifficulty, bool>>>()))
                        .ReturnsAsync(new List<PowerUsageWithDifficulty> { new PowerUsageWithDifficulty() { /*TODO:propertyler buraya yazılacak PowerUsageWithDifficultyId = 1, PowerUsageWithDifficultyName = "test"*/ } }.AsQueryable());

            var handler = new GetPowerUsageWithDifficultiesQueryHandler(_powerUsageWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<PowerUsageWithDifficulty>)x.Data).Count.Should().BeGreaterThan(1);
        }

        [Test]
        public async Task PowerUsageWithDifficulty_CreateCommand_Success()
        {
            PowerUsageWithDifficulty rt = null;
            //Arrange
            var command = new CreatePowerUsageWithDifficultyCommand();
            //propertyler buraya yazılacak
            //command.PowerUsageWithDifficultyName = "deneme";

            _powerUsageWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _powerUsageWithDifficultyRepository.Setup(x => x.Add(It.IsAny<PowerUsageWithDifficulty>()));

            var handler = new CreatePowerUsageWithDifficultyCommandHandler(_powerUsageWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task PowerUsageWithDifficulty_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreatePowerUsageWithDifficultyCommand();
            //propertyler buraya yazılacak
            //command.PowerUsageWithDifficultyName = "test";

            _powerUsageWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<PowerUsageWithDifficulty, bool>>>()))
                                           .ReturnsAsync(new List<PowerUsageWithDifficulty> { new PowerUsageWithDifficulty() { /*TODO:propertyler buraya yazılacak PowerUsageWithDifficultyId = 1, PowerUsageWithDifficultyName = "test"*/ } }.AsQueryable());

            _powerUsageWithDifficultyRepository.Setup(x => x.Add(It.IsAny<PowerUsageWithDifficulty>()));

            var handler = new CreatePowerUsageWithDifficultyCommandHandler(_powerUsageWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task PowerUsageWithDifficulty_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdatePowerUsageWithDifficultyCommand();
            //command.PowerUsageWithDifficultyName = "test";

            _powerUsageWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new PowerUsageWithDifficulty() { /*TODO:propertyler buraya yazılacak PowerUsageWithDifficultyId = 1, PowerUsageWithDifficultyName = "deneme"*/ });

            _powerUsageWithDifficultyRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<PowerUsageWithDifficulty>()));

            var handler = new UpdatePowerUsageWithDifficultyCommandHandler(_powerUsageWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task PowerUsageWithDifficulty_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeletePowerUsageWithDifficultyCommand();

            _powerUsageWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new PowerUsageWithDifficulty() { /*TODO:propertyler buraya yazılacak PowerUsageWithDifficultyId = 1, PowerUsageWithDifficultyName = "deneme"*/});

            _powerUsageWithDifficultyRepository.Setup(x => x.Delete(It.IsAny<PowerUsageWithDifficulty>()));

            var handler = new DeletePowerUsageWithDifficultyCommandHandler(_powerUsageWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}