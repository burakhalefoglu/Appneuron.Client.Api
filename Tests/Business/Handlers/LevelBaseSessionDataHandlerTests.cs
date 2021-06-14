
using Business.Handlers.LevelBaseSessionDatas.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.LevelBaseSessionDatas.Queries.GetLevelBaseSessionDataQuery;
using Entities.Concrete;
using static Business.Handlers.LevelBaseSessionDatas.Queries.GetLevelBaseSessionDatasQuery;
using static Business.Handlers.LevelBaseSessionDatas.Commands.CreateLevelBaseSessionDataCommand;
using Business.Handlers.LevelBaseSessionDatas.Commands;
using Business.Constants;
using static Business.Handlers.LevelBaseSessionDatas.Commands.UpdateLevelBaseSessionDataCommand;
using static Business.Handlers.LevelBaseSessionDatas.Commands.DeleteLevelBaseSessionDataCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class LevelBaseSessionDataHandlerTests
    {
        Mock<ILevelBaseSessionDataRepository> _levelBaseSessionDataRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _levelBaseSessionDataRepository = new Mock<ILevelBaseSessionDataRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task LevelBaseSessionData_GetQuery_Success()
        {
            //Arrange
            var query = new GetLevelBaseSessionDataQuery();

            _levelBaseSessionDataRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new LevelBaseSessionData()
//propertyler buraya yazılacak
//{																		
//LevelBaseSessionDataId = 1,
//LevelBaseSessionDataName = "Test"
//}
);

            var handler = new GetLevelBaseSessionDataQueryHandler(_levelBaseSessionDataRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.LevelBaseSessionDataId.Should().Be(1);

        }

        [Test]
        public async Task LevelBaseSessionData_GetQueries_Success()
        {
            //Arrange
            var query = new GetLevelBaseSessionDatasQuery();

            _levelBaseSessionDataRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<LevelBaseSessionData, bool>>>()))
                        .ReturnsAsync(new List<LevelBaseSessionData> { new LevelBaseSessionData() { /*TODO:propertyler buraya yazılacak LevelBaseSessionDataId = 1, LevelBaseSessionDataName = "test"*/ } }.AsQueryable());

            var handler = new GetLevelBaseSessionDatasQueryHandler(_levelBaseSessionDataRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<LevelBaseSessionData>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task LevelBaseSessionData_CreateCommand_Success()
        {
            LevelBaseSessionData rt = null;
            //Arrange
            var command = new CreateLevelBaseSessionDataCommand();
            //propertyler buraya yazılacak
            //command.LevelBaseSessionDataName = "deneme";

            _levelBaseSessionDataRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _levelBaseSessionDataRepository.Setup(x => x.Add(It.IsAny<LevelBaseSessionData>()));

            var handler = new CreateLevelBaseSessionDataCommandHandler(_levelBaseSessionDataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task LevelBaseSessionData_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateLevelBaseSessionDataCommand();
            //propertyler buraya yazılacak 
            //command.LevelBaseSessionDataName = "test";

            _levelBaseSessionDataRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<LevelBaseSessionData, bool>>>()))
                                           .ReturnsAsync(new List<LevelBaseSessionData> { new LevelBaseSessionData() { /*TODO:propertyler buraya yazılacak LevelBaseSessionDataId = 1, LevelBaseSessionDataName = "test"*/ } }.AsQueryable());

            _levelBaseSessionDataRepository.Setup(x => x.Add(It.IsAny<LevelBaseSessionData>()));

            var handler = new CreateLevelBaseSessionDataCommandHandler(_levelBaseSessionDataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task LevelBaseSessionData_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateLevelBaseSessionDataCommand();
            //command.LevelBaseSessionDataName = "test";

            _levelBaseSessionDataRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new LevelBaseSessionData() { /*TODO:propertyler buraya yazılacak LevelBaseSessionDataId = 1, LevelBaseSessionDataName = "deneme"*/ });

            _levelBaseSessionDataRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<LevelBaseSessionData>()));

            var handler = new UpdateLevelBaseSessionDataCommandHandler(_levelBaseSessionDataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task LevelBaseSessionData_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteLevelBaseSessionDataCommand();

            _levelBaseSessionDataRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new LevelBaseSessionData() { /*TODO:propertyler buraya yazılacak LevelBaseSessionDataId = 1, LevelBaseSessionDataName = "deneme"*/});

            _levelBaseSessionDataRepository.Setup(x => x.Delete(It.IsAny<LevelBaseSessionData>()));

            var handler = new DeleteLevelBaseSessionDataCommandHandler(_levelBaseSessionDataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

