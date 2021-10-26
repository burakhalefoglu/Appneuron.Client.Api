
using Business.Handlers.AdvStrategyBehaviorModels.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.AdvStrategyBehaviorModels.Queries.GetAdvStrategyBehaviorModelQuery;
using Entities.Concrete;
using static Business.Handlers.AdvStrategyBehaviorModels.Queries.GetAdvStrategyBehaviorModelsQuery;
using static Business.Handlers.AdvStrategyBehaviorModels.Commands.CreateAdvStrategyBehaviorModelCommand;
using Business.Handlers.AdvStrategyBehaviorModels.Commands;
using Business.Constants;
using static Business.Handlers.AdvStrategyBehaviorModels.Commands.UpdateAdvStrategyBehaviorModelCommand;
using static Business.Handlers.AdvStrategyBehaviorModels.Commands.DeleteAdvStrategyBehaviorModelCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class AdvStrategyBehaviorModelHandlerTests
    {
        Mock<IAdvStrategyBehaviorModelRepository> _advStrategyBehaviorModelRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _advStrategyBehaviorModelRepository = new Mock<IAdvStrategyBehaviorModelRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task AdvStrategyBehaviorModel_GetQuery_Success()
        {
            //Arrange
            var query = new GetAdvStrategyBehaviorModelQuery();

            _advStrategyBehaviorModelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new AdvStrategyBehaviorModel()
//propertyler buraya yazılacak
//{																		
//AdvStrategyBehaviorModelId = 1,
//AdvStrategyBehaviorModelName = "Test"
//}
);

            var handler = new GetAdvStrategyBehaviorModelQueryHandler(_advStrategyBehaviorModelRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.AdvStrategyBehaviorModelId.Should().Be(1);

        }

        [Test]
        public async Task AdvStrategyBehaviorModel_GetQueries_Success()
        {
            //Arrange
            var query = new GetAdvStrategyBehaviorModelsQuery();

            _advStrategyBehaviorModelRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<AdvStrategyBehaviorModel, bool>>>()))
                        .ReturnsAsync(new List<AdvStrategyBehaviorModel> { new AdvStrategyBehaviorModel() { /*TODO:propertyler buraya yazılacak AdvStrategyBehaviorModelId = 1, AdvStrategyBehaviorModelName = "test"*/ } }.AsQueryable());

            var handler = new GetAdvStrategyBehaviorModelsQueryHandler(_advStrategyBehaviorModelRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<AdvStrategyBehaviorModel>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task AdvStrategyBehaviorModel_CreateCommand_Success()
        {
            AdvStrategyBehaviorModel rt = null;
            //Arrange
            var command = new CreateAdvStrategyBehaviorModelCommand();
            //propertyler buraya yazılacak
            //command.AdvStrategyBehaviorModelName = "deneme";

            _advStrategyBehaviorModelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _advStrategyBehaviorModelRepository.Setup(x => x.Add(It.IsAny<AdvStrategyBehaviorModel>()));

            var handler = new CreateAdvStrategyBehaviorModelCommandHandler(_advStrategyBehaviorModelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task AdvStrategyBehaviorModel_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateAdvStrategyBehaviorModelCommand();
            //propertyler buraya yazılacak 
            //command.AdvStrategyBehaviorModelName = "test";

            _advStrategyBehaviorModelRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<AdvStrategyBehaviorModel, bool>>>()))
                                           .ReturnsAsync(new List<AdvStrategyBehaviorModel> { new AdvStrategyBehaviorModel() { /*TODO:propertyler buraya yazılacak AdvStrategyBehaviorModelId = 1, AdvStrategyBehaviorModelName = "test"*/ } }.AsQueryable());

            _advStrategyBehaviorModelRepository.Setup(x => x.Add(It.IsAny<AdvStrategyBehaviorModel>()));

            var handler = new CreateAdvStrategyBehaviorModelCommandHandler(_advStrategyBehaviorModelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task AdvStrategyBehaviorModel_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateAdvStrategyBehaviorModelCommand();
            //command.AdvStrategyBehaviorModelName = "test";

            _advStrategyBehaviorModelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new AdvStrategyBehaviorModel() { /*TODO:propertyler buraya yazılacak AdvStrategyBehaviorModelId = 1, AdvStrategyBehaviorModelName = "deneme"*/ });

            _advStrategyBehaviorModelRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<AdvStrategyBehaviorModel>()));

            var handler = new UpdateAdvStrategyBehaviorModelCommandHandler(_advStrategyBehaviorModelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task AdvStrategyBehaviorModel_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteAdvStrategyBehaviorModelCommand();

            _advStrategyBehaviorModelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new AdvStrategyBehaviorModel() { /*TODO:propertyler buraya yazılacak AdvStrategyBehaviorModelId = 1, AdvStrategyBehaviorModelName = "deneme"*/});

            _advStrategyBehaviorModelRepository.Setup(x => x.Delete(It.IsAny<AdvStrategyBehaviorModel>()));

            var handler = new DeleteAdvStrategyBehaviorModelCommandHandler(_advStrategyBehaviorModelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

