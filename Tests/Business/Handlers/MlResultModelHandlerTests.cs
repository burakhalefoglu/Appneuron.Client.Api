
using Business.Handlers.MlResultModels.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.MlResultModels.Queries.GetMlResultModelQuery;
using Entities.Concrete;
using static Business.Handlers.MlResultModels.Queries.GetMlResultModelsQuery;
using static Business.Handlers.MlResultModels.Commands.CreateMlResultModelCommand;
using Business.Handlers.MlResultModels.Commands;
using Business.Constants;
using static Business.Handlers.MlResultModels.Commands.UpdateMlResultModelCommand;
using static Business.Handlers.MlResultModels.Commands.DeleteMlResultModelCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class MlResultModelHandlerTests
    {
        Mock<IMlResultModelRepository> _mlResultModelRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _mlResultModelRepository = new Mock<IMlResultModelRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task MlResultModel_GetQuery_Success()
        {
            //Arrange
            var query = new GetMlResultModelQuery();

            _mlResultModelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new MlResultModel()
//propertyler buraya yazılacak
//{																		
//MlResultModelId = 1,
//MlResultModelName = "Test"
//}
);

            var handler = new GetMlResultModelQueryHandler(_mlResultModelRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.MlResultModelId.Should().Be(1);

        }

        [Test]
        public async Task MlResultModel_GetQueries_Success()
        {
            //Arrange
            var query = new GetMlResultModelsQuery();

            _mlResultModelRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<MlResultModel, bool>>>()))
                        .ReturnsAsync(new List<MlResultModel> { new MlResultModel() { /*TODO:propertyler buraya yazılacak MlResultModelId = 1, MlResultModelName = "test"*/ } }.AsQueryable());

            var handler = new GetMlResultModelsQueryHandler(_mlResultModelRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<MlResultModel>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task MlResultModel_CreateCommand_Success()
        {
            MlResultModel rt = null;
            //Arrange
            var command = new CreateMlResultModelCommand();
            //propertyler buraya yazılacak
            //command.MlResultModelName = "deneme";

            _mlResultModelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _mlResultModelRepository.Setup(x => x.Add(It.IsAny<MlResultModel>()));

            var handler = new CreateMlResultModelCommandHandler(_mlResultModelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task MlResultModel_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateMlResultModelCommand();
            //propertyler buraya yazılacak 
            //command.MlResultModelName = "test";

            _mlResultModelRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<MlResultModel, bool>>>()))
                                           .ReturnsAsync(new List<MlResultModel> { new MlResultModel() { /*TODO:propertyler buraya yazılacak MlResultModelId = 1, MlResultModelName = "test"*/ } }.AsQueryable());

            _mlResultModelRepository.Setup(x => x.Add(It.IsAny<MlResultModel>()));

            var handler = new CreateMlResultModelCommandHandler(_mlResultModelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task MlResultModel_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateMlResultModelCommand();
            //command.MlResultModelName = "test";

            _mlResultModelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new MlResultModel() { /*TODO:propertyler buraya yazılacak MlResultModelId = 1, MlResultModelName = "deneme"*/ });

            _mlResultModelRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<MlResultModel>()));

            var handler = new UpdateMlResultModelCommandHandler(_mlResultModelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task MlResultModel_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteMlResultModelCommand();

            _mlResultModelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new MlResultModel() { /*TODO:propertyler buraya yazılacak MlResultModelId = 1, MlResultModelName = "deneme"*/});

            _mlResultModelRepository.Setup(x => x.Delete(It.IsAny<MlResultModel>()));

            var handler = new DeleteMlResultModelCommandHandler(_mlResultModelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

