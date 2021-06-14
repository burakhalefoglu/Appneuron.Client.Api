
using Business.Handlers.LevelBaseDieDatas.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.LevelBaseDieDatas.Queries.GetLevelBaseDieDataQuery;
using Entities.Concrete;
using static Business.Handlers.LevelBaseDieDatas.Queries.GetLevelBaseDieDatasQuery;
using static Business.Handlers.LevelBaseDieDatas.Commands.CreateLevelBaseDieDataCommand;
using Business.Handlers.LevelBaseDieDatas.Commands;
using Business.Constants;
using static Business.Handlers.LevelBaseDieDatas.Commands.UpdateLevelBaseDieDataCommand;
using static Business.Handlers.LevelBaseDieDatas.Commands.DeleteLevelBaseDieDataCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class LevelBaseDieDataHandlerTests
    {
        Mock<ILevelBaseDieDataRepository> _levelBaseDieDataRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _levelBaseDieDataRepository = new Mock<ILevelBaseDieDataRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task LevelBaseDieData_GetQuery_Success()
        {
            //Arrange
            var query = new GetLevelBaseDieDataQuery();

            _levelBaseDieDataRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new LevelBaseDieData()
//propertyler buraya yazılacak
//{																		
//LevelBaseDieDataId = 1,
//LevelBaseDieDataName = "Test"
//}
);

            var handler = new GetLevelBaseDieDataQueryHandler(_levelBaseDieDataRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.LevelBaseDieDataId.Should().Be(1);

        }

        [Test]
        public async Task LevelBaseDieData_GetQueries_Success()
        {
            //Arrange
            var query = new GetLevelBaseDieDatasQuery();

            _levelBaseDieDataRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<LevelBaseDieData, bool>>>()))
                        .ReturnsAsync(new List<LevelBaseDieData> { new LevelBaseDieData() { /*TODO:propertyler buraya yazılacak LevelBaseDieDataId = 1, LevelBaseDieDataName = "test"*/ } }.AsQueryable());

            var handler = new GetLevelBaseDieDatasQueryHandler(_levelBaseDieDataRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<LevelBaseDieData>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task LevelBaseDieData_CreateCommand_Success()
        {
            LevelBaseDieData rt = null;
            //Arrange
            var command = new CreateLevelBaseDieDataCommand();
            //propertyler buraya yazılacak
            //command.LevelBaseDieDataName = "deneme";

            _levelBaseDieDataRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _levelBaseDieDataRepository.Setup(x => x.Add(It.IsAny<LevelBaseDieData>()));

            var handler = new CreateLevelBaseDieDataCommandHandler(_levelBaseDieDataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task LevelBaseDieData_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateLevelBaseDieDataCommand();
            //propertyler buraya yazılacak 
            //command.LevelBaseDieDataName = "test";

            _levelBaseDieDataRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<LevelBaseDieData, bool>>>()))
                                           .ReturnsAsync(new List<LevelBaseDieData> { new LevelBaseDieData() { /*TODO:propertyler buraya yazılacak LevelBaseDieDataId = 1, LevelBaseDieDataName = "test"*/ } }.AsQueryable());

            _levelBaseDieDataRepository.Setup(x => x.Add(It.IsAny<LevelBaseDieData>()));

            var handler = new CreateLevelBaseDieDataCommandHandler(_levelBaseDieDataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task LevelBaseDieData_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateLevelBaseDieDataCommand();
            //command.LevelBaseDieDataName = "test";

            _levelBaseDieDataRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new LevelBaseDieData() { /*TODO:propertyler buraya yazılacak LevelBaseDieDataId = 1, LevelBaseDieDataName = "deneme"*/ });

            _levelBaseDieDataRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<LevelBaseDieData>()));

            var handler = new UpdateLevelBaseDieDataCommandHandler(_levelBaseDieDataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task LevelBaseDieData_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteLevelBaseDieDataCommand();

            _levelBaseDieDataRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new LevelBaseDieData() { /*TODO:propertyler buraya yazılacak LevelBaseDieDataId = 1, LevelBaseDieDataName = "deneme"*/});

            _levelBaseDieDataRepository.Setup(x => x.Delete(It.IsAny<LevelBaseDieData>()));

            var handler = new DeleteLevelBaseDieDataCommandHandler(_levelBaseDieDataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

