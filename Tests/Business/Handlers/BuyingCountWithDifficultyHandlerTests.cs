using Business.Constants;
using Business.Handlers.BuyingCountWithDifficulties.Commands;
using Business.Handlers.BuyingCountWithDifficulties.Queries;
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
using static Business.Handlers.BuyingCountWithDifficulties.Commands.CreateBuyingCountWithDifficultyCommand;
using static Business.Handlers.BuyingCountWithDifficulties.Commands.DeleteBuyingCountWithDifficultyCommand;
using static Business.Handlers.BuyingCountWithDifficulties.Commands.UpdateBuyingCountWithDifficultyCommand;
using static Business.Handlers.BuyingCountWithDifficulties.Queries.GetBuyingCountWithDifficultiesQuery;
using static Business.Handlers.BuyingCountWithDifficulties.Queries.GetBuyingCountWithDifficultyQuery;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class BuyingCountWithDifficultyHandlerTests
    {
        private Mock<IBuyingCountWithDifficultyRepository> _buyingCountWithDifficultyRepository;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _buyingCountWithDifficultyRepository = new Mock<IBuyingCountWithDifficultyRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task BuyingCountWithDifficulty_GetQuery_Success()
        {
            //Arrange
            var query = new GetBuyingCountWithDifficultyQuery();

            _buyingCountWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new BuyingCountWithDifficulty()
//propertyler buraya yazılacak
//{
//BuyingCountWithDifficultyId = 1,
//BuyingCountWithDifficultyName = "Test"
//}
);

            var handler = new GetBuyingCountWithDifficultyQueryHandler(_buyingCountWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.BuyingCountWithDifficultyId.Should().Be(1);
        }

        [Test]
        public async Task BuyingCountWithDifficulty_GetQueries_Success()
        {
            //Arrange
            var query = new GetBuyingCountWithDifficultiesQuery();

            _buyingCountWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<BuyingCountWithDifficulty, bool>>>()))
                        .ReturnsAsync(new List<BuyingCountWithDifficulty> { new BuyingCountWithDifficulty() { /*TODO:propertyler buraya yazılacak BuyingCountWithDifficultyId = 1, BuyingCountWithDifficultyName = "test"*/ } }.AsQueryable());

            var handler = new GetBuyingCountWithDifficultiesQueryHandler(_buyingCountWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<BuyingCountWithDifficulty>)x.Data).Count.Should().BeGreaterThan(1);
        }

        [Test]
        public async Task BuyingCountWithDifficulty_CreateCommand_Success()
        {
            BuyingCountWithDifficulty rt = null;
            //Arrange
            var command = new CreateBuyingCountWithDifficultyCommand();
            //propertyler buraya yazılacak
            //command.BuyingCountWithDifficultyName = "deneme";

            _buyingCountWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _buyingCountWithDifficultyRepository.Setup(x => x.Add(It.IsAny<BuyingCountWithDifficulty>()));

            var handler = new CreateBuyingCountWithDifficultyCommandHandler(_buyingCountWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task BuyingCountWithDifficulty_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateBuyingCountWithDifficultyCommand();
            //propertyler buraya yazılacak
            //command.BuyingCountWithDifficultyName = "test";

            _buyingCountWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<BuyingCountWithDifficulty, bool>>>()))
                                           .ReturnsAsync(new List<BuyingCountWithDifficulty> { new BuyingCountWithDifficulty() { /*TODO:propertyler buraya yazılacak BuyingCountWithDifficultyId = 1, BuyingCountWithDifficultyName = "test"*/ } }.AsQueryable());

            _buyingCountWithDifficultyRepository.Setup(x => x.Add(It.IsAny<BuyingCountWithDifficulty>()));

            var handler = new CreateBuyingCountWithDifficultyCommandHandler(_buyingCountWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task BuyingCountWithDifficulty_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateBuyingCountWithDifficultyCommand();
            //command.BuyingCountWithDifficultyName = "test";

            _buyingCountWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new BuyingCountWithDifficulty() { /*TODO:propertyler buraya yazılacak BuyingCountWithDifficultyId = 1, BuyingCountWithDifficultyName = "deneme"*/ });

            _buyingCountWithDifficultyRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<BuyingCountWithDifficulty>()));

            var handler = new UpdateBuyingCountWithDifficultyCommandHandler(_buyingCountWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task BuyingCountWithDifficulty_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteBuyingCountWithDifficultyCommand();

            _buyingCountWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new BuyingCountWithDifficulty() { /*TODO:propertyler buraya yazılacak BuyingCountWithDifficultyId = 1, BuyingCountWithDifficultyName = "deneme"*/});

            _buyingCountWithDifficultyRepository.Setup(x => x.Delete(It.IsAny<BuyingCountWithDifficulty>()));

            var handler = new DeleteBuyingCountWithDifficultyCommandHandler(_buyingCountWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}