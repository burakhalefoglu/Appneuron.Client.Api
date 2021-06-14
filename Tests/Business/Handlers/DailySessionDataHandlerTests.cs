
using Business.Handlers.DailySessionDatas.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.DailySessionDatas.Queries.GetDailySessionDataQuery;
using Entities.Concrete;
using static Business.Handlers.DailySessionDatas.Queries.GetDailySessionDatasQuery;
using static Business.Handlers.DailySessionDatas.Commands.CreateDailySessionDataCommand;
using Business.Handlers.DailySessionDatas.Commands;
using Business.Constants;
using static Business.Handlers.DailySessionDatas.Commands.UpdateDailySessionDataCommand;
using static Business.Handlers.DailySessionDatas.Commands.DeleteDailySessionDataCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class DailySessionDataHandlerTests
    {
        Mock<IDailySessionDataRepository> _dailySessionDataRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _dailySessionDataRepository = new Mock<IDailySessionDataRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task DailySessionData_GetQuery_Success()
        {
            //Arrange
            var query = new GetDailySessionDataQuery();

            _dailySessionDataRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new DailySessionData()
//propertyler buraya yazılacak
//{																		
//DailySessionDataId = 1,
//DailySessionDataName = "Test"
//}
);

            var handler = new GetDailySessionDataQueryHandler(_dailySessionDataRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.DailySessionDataId.Should().Be(1);

        }

        [Test]
        public async Task DailySessionData_GetQueries_Success()
        {
            //Arrange
            var query = new GetDailySessionDatasQuery();

            _dailySessionDataRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<DailySessionData, bool>>>()))
                        .ReturnsAsync(new List<DailySessionData> { new DailySessionData() { /*TODO:propertyler buraya yazılacak DailySessionDataId = 1, DailySessionDataName = "test"*/ } }.AsQueryable());

            var handler = new GetDailySessionDatasQueryHandler(_dailySessionDataRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<DailySessionData>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task DailySessionData_CreateCommand_Success()
        {
            DailySessionData rt = null;
            //Arrange
            var command = new CreateDailySessionDataCommand();
            //propertyler buraya yazılacak
            //command.DailySessionDataName = "deneme";

            _dailySessionDataRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _dailySessionDataRepository.Setup(x => x.Add(It.IsAny<DailySessionData>()));

            var handler = new CreateDailySessionDataCommandHandler(_dailySessionDataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task DailySessionData_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateDailySessionDataCommand();
            //propertyler buraya yazılacak 
            //command.DailySessionDataName = "test";

            _dailySessionDataRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<DailySessionData, bool>>>()))
                                           .ReturnsAsync(new List<DailySessionData> { new DailySessionData() { /*TODO:propertyler buraya yazılacak DailySessionDataId = 1, DailySessionDataName = "test"*/ } }.AsQueryable());

            _dailySessionDataRepository.Setup(x => x.Add(It.IsAny<DailySessionData>()));

            var handler = new CreateDailySessionDataCommandHandler(_dailySessionDataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task DailySessionData_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateDailySessionDataCommand();
            //command.DailySessionDataName = "test";

            _dailySessionDataRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new DailySessionData() { /*TODO:propertyler buraya yazılacak DailySessionDataId = 1, DailySessionDataName = "deneme"*/ });

            _dailySessionDataRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<DailySessionData>()));

            var handler = new UpdateDailySessionDataCommandHandler(_dailySessionDataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task DailySessionData_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteDailySessionDataCommand();

            _dailySessionDataRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new DailySessionData() { /*TODO:propertyler buraya yazılacak DailySessionDataId = 1, DailySessionDataName = "deneme"*/});

            _dailySessionDataRepository.Setup(x => x.Delete(It.IsAny<DailySessionData>()));

            var handler = new DeleteDailySessionDataCommandHandler(_dailySessionDataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

