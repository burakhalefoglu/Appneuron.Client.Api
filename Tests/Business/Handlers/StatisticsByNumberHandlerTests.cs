using Business.Constants;
using Business.Handlers.StatisticsByNumbers.Commands;
using Business.Handlers.StatisticsByNumbers.Queries;
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
using static Business.Handlers.StatisticsByNumbers.Commands.CreateStatisticsByNumberCommand;
using static Business.Handlers.StatisticsByNumbers.Commands.DeleteStatisticsByNumberCommand;
using static Business.Handlers.StatisticsByNumbers.Commands.UpdateStatisticsByNumberCommand;
using static Business.Handlers.StatisticsByNumbers.Queries.GetStatisticsByNumberQuery;
using static Business.Handlers.StatisticsByNumbers.Queries.GetStatisticsByNumbersQuery;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class StatisticsByNumberHandlerTests
    {
        private Mock<IStatisticsByNumberRepository> _statisticsByNumberRepository;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _statisticsByNumberRepository = new Mock<IStatisticsByNumberRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task StatisticsByNumber_GetQuery_Success()
        {
            //Arrange
            var query = new GetStatisticsByNumberQuery();

            _statisticsByNumberRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new StatisticsByNumber()
//propertyler buraya yazılacak
//{
//StatisticsByNumberId = 1,
//StatisticsByNumberName = "Test"
//}
);

            var handler = new GetStatisticsByNumberQueryHandler(_statisticsByNumberRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.StatisticsByNumberId.Should().Be(1);
        }

        [Test]
        public async Task StatisticsByNumber_GetQueries_Success()
        {
            //Arrange
            var query = new GetStatisticsByNumbersQuery();

            _statisticsByNumberRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<StatisticsByNumber, bool>>>()))
                        .ReturnsAsync(new List<StatisticsByNumber> { new StatisticsByNumber() { /*TODO:propertyler buraya yazılacak StatisticsByNumberId = 1, StatisticsByNumberName = "test"*/ } }.AsQueryable());

            var handler = new GetStatisticsByNumbersQueryHandler(_statisticsByNumberRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<StatisticsByNumber>)x.Data).Count.Should().BeGreaterThan(1);
        }

        [Test]
        public async Task StatisticsByNumber_CreateCommand_Success()
        {
            StatisticsByNumber rt = null;
            //Arrange
            var command = new CreateStatisticsByNumberCommand();
            //propertyler buraya yazılacak
            //command.StatisticsByNumberName = "deneme";

            _statisticsByNumberRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _statisticsByNumberRepository.Setup(x => x.Add(It.IsAny<StatisticsByNumber>()));

            var handler = new CreateStatisticsByNumberCommandHandler(_statisticsByNumberRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task StatisticsByNumber_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateStatisticsByNumberCommand();
            //propertyler buraya yazılacak
            //command.StatisticsByNumberName = "test";

            _statisticsByNumberRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<StatisticsByNumber, bool>>>()))
                                           .ReturnsAsync(new List<StatisticsByNumber> { new StatisticsByNumber() { /*TODO:propertyler buraya yazılacak StatisticsByNumberId = 1, StatisticsByNumberName = "test"*/ } }.AsQueryable());

            _statisticsByNumberRepository.Setup(x => x.Add(It.IsAny<StatisticsByNumber>()));

            var handler = new CreateStatisticsByNumberCommandHandler(_statisticsByNumberRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task StatisticsByNumber_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateStatisticsByNumberCommand();
            //command.StatisticsByNumberName = "test";

            _statisticsByNumberRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new StatisticsByNumber() { /*TODO:propertyler buraya yazılacak StatisticsByNumberId = 1, StatisticsByNumberName = "deneme"*/ });

            _statisticsByNumberRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<StatisticsByNumber>()));

            var handler = new UpdateStatisticsByNumberCommandHandler(_statisticsByNumberRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task StatisticsByNumber_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteStatisticsByNumberCommand();

            _statisticsByNumberRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new StatisticsByNumber() { /*TODO:propertyler buraya yazılacak StatisticsByNumberId = 1, StatisticsByNumberName = "deneme"*/});

            _statisticsByNumberRepository.Setup(x => x.Delete(It.IsAny<StatisticsByNumber>()));

            var handler = new DeleteStatisticsByNumberCommandHandler(_statisticsByNumberRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}