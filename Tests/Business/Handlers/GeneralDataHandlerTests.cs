using Business.Constants;
using Business.Handlers.GeneralDatas.Commands;
using Business.Handlers.GeneralDatas.Queries;
using DataAccess.Abstract;
using Entities.Concrete;
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
using static Business.Handlers.GeneralDatas.Commands.CreateGeneralDataCommand;
using static Business.Handlers.GeneralDatas.Commands.DeleteGeneralDataCommand;
using static Business.Handlers.GeneralDatas.Commands.UpdateGeneralDataCommand;
using static Business.Handlers.GeneralDatas.Queries.GetGeneralDataQuery;
using static Business.Handlers.GeneralDatas.Queries.GetGeneralDatasQuery;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class GeneralDataHandlerTests
    {
        private Mock<IGeneralDataRepository> _generalDataRepository;
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _generalDataRepository = new Mock<IGeneralDataRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task GeneralData_GetQuery_Success()
        {
            //Arrange
            var query = new GetGeneralDataQuery();

            _generalDataRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new GeneralData()
//propertyler buraya yazılacak
//{
//GeneralDataId = 1,
//GeneralDataName = "Test"
//}
);

            var handler = new GetGeneralDataQueryHandler(_generalDataRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.GeneralDataId.Should().Be(1);
        }

        [Test]
        public async Task GeneralData_GetQueries_Success()
        {
            //Arrange
            var query = new GetGeneralDatasQuery();

            _generalDataRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<GeneralData, bool>>>()))
                        .ReturnsAsync(new List<GeneralData> { new GeneralData() { /*TODO:propertyler buraya yazılacak GeneralDataId = 1, GeneralDataName = "test"*/ } }.AsQueryable());

            var handler = new GetGeneralDatasQueryHandler(_generalDataRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<GeneralData>)x.Data).Count.Should().BeGreaterThan(1);
        }

        [Test]
        public async Task GeneralData_CreateCommand_Success()
        {
            GeneralData rt = null;
            //Arrange
            var command = new CreateGeneralDataCommand();
            //propertyler buraya yazılacak
            //command.GeneralDataName = "deneme";

            _generalDataRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _generalDataRepository.Setup(x => x.Add(It.IsAny<GeneralData>()));

            var handler = new CreateGeneralDataCommandHandler(_generalDataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task GeneralData_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateGeneralDataCommand();
            //propertyler buraya yazılacak
            //command.GeneralDataName = "test";

            _generalDataRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<GeneralData, bool>>>()))
                                           .ReturnsAsync(new List<GeneralData> { new GeneralData() { /*TODO:propertyler buraya yazılacak GeneralDataId = 1, GeneralDataName = "test"*/ } }.AsQueryable());

            _generalDataRepository.Setup(x => x.Add(It.IsAny<GeneralData>()));

            var handler = new CreateGeneralDataCommandHandler(_generalDataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task GeneralData_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateGeneralDataCommand();
            //command.GeneralDataName = "test";

            _generalDataRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new GeneralData() { /*TODO:propertyler buraya yazılacak GeneralDataId = 1, GeneralDataName = "deneme"*/ });

            _generalDataRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<GeneralData>()));

            var handler = new UpdateGeneralDataCommandHandler(_generalDataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task GeneralData_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteGeneralDataCommand();

            _generalDataRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new GeneralData() { /*TODO:propertyler buraya yazılacak GeneralDataId = 1, GeneralDataName = "deneme"*/});

            _generalDataRepository.Setup(x => x.Delete(It.IsAny<GeneralData>()));

            var handler = new DeleteGeneralDataCommandHandler(_generalDataRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}