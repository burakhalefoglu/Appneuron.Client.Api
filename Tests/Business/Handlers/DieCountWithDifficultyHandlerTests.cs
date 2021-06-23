using Business.Constants;
using Business.Handlers.DieCountWithDifficulties.Commands;
using Business.Handlers.DieCountWithDifficulties.Queries;
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
using static Business.Handlers.DieCountWithDifficulties.Commands.CreateDieCountWithDifficultyCommand;
using static Business.Handlers.DieCountWithDifficulties.Commands.DeleteDieCountWithDifficultyCommand;
using static Business.Handlers.DieCountWithDifficulties.Commands.UpdateDieCountWithDifficultyCommand;
using static Business.Handlers.DieCountWithDifficulties.Queries.GetDieCountWithDifficultiesQuery;
using static Business.Handlers.DieCountWithDifficulties.Queries.GetDieCountWithDifficultyQuery;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class DieCountWithDifficultyHandlerTests
    {
        private Mock<IDieCountWithDifficultyRepository> _dieCountWithDifficultyRepository;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _dieCountWithDifficultyRepository = new Mock<IDieCountWithDifficultyRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task DieCountWithDifficulty_GetQuery_Success()
        {
            //Arrange
            var query = new GetDieCountWithDifficultyQuery();

            _dieCountWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new DieCountWithDifficulty()
//propertyler buraya yazılacak
//{
//DieCountWithDifficultyId = 1,
//DieCountWithDifficultyName = "Test"
//}
);

            var handler = new GetDieCountWithDifficultyQueryHandler(_dieCountWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.DieCountWithDifficultyId.Should().Be(1);
        }

        [Test]
        public async Task DieCountWithDifficulty_GetQueries_Success()
        {
            //Arrange
            var query = new GetDieCountWithDifficultiesQuery();

            _dieCountWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<DieCountWithDifficulty, bool>>>()))
                        .ReturnsAsync(new List<DieCountWithDifficulty> { new DieCountWithDifficulty() { /*TODO:propertyler buraya yazılacak DieCountWithDifficultyId = 1, DieCountWithDifficultyName = "test"*/ } }.AsQueryable());

            var handler = new GetDieCountWithDifficultiesQueryHandler(_dieCountWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<DieCountWithDifficulty>)x.Data).Count.Should().BeGreaterThan(1);
        }

        [Test]
        public async Task DieCountWithDifficulty_CreateCommand_Success()
        {
            DieCountWithDifficulty rt = null;
            //Arrange
            var command = new CreateDieCountWithDifficultyCommand();
            //propertyler buraya yazılacak
            //command.DieCountWithDifficultyName = "deneme";

            _dieCountWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _dieCountWithDifficultyRepository.Setup(x => x.Add(It.IsAny<DieCountWithDifficulty>()));

            var handler = new CreateDieCountWithDifficultyCommandHandler(_dieCountWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task DieCountWithDifficulty_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateDieCountWithDifficultyCommand();
            //propertyler buraya yazılacak
            //command.DieCountWithDifficultyName = "test";

            _dieCountWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<DieCountWithDifficulty, bool>>>()))
                                           .ReturnsAsync(new List<DieCountWithDifficulty> { new DieCountWithDifficulty() { /*TODO:propertyler buraya yazılacak DieCountWithDifficultyId = 1, DieCountWithDifficultyName = "test"*/ } }.AsQueryable());

            _dieCountWithDifficultyRepository.Setup(x => x.Add(It.IsAny<DieCountWithDifficulty>()));

            var handler = new CreateDieCountWithDifficultyCommandHandler(_dieCountWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task DieCountWithDifficulty_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateDieCountWithDifficultyCommand();
            //command.DieCountWithDifficultyName = "test";

            _dieCountWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new DieCountWithDifficulty() { /*TODO:propertyler buraya yazılacak DieCountWithDifficultyId = 1, DieCountWithDifficultyName = "deneme"*/ });

            _dieCountWithDifficultyRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<DieCountWithDifficulty>()));

            var handler = new UpdateDieCountWithDifficultyCommandHandler(_dieCountWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task DieCountWithDifficulty_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteDieCountWithDifficultyCommand();

            _dieCountWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new DieCountWithDifficulty() { /*TODO:propertyler buraya yazılacak DieCountWithDifficultyId = 1, DieCountWithDifficultyName = "deneme"*/});

            _dieCountWithDifficultyRepository.Setup(x => x.Delete(It.IsAny<DieCountWithDifficulty>()));

            var handler = new DeleteDieCountWithDifficultyCommandHandler(_dieCountWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}