
using Business.Handlers.OfferBehaviorModels.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OfferBehaviorModels.Queries.GetOfferBehaviorModelQuery;
using Entities.Concrete;
using static Business.Handlers.OfferBehaviorModels.Queries.GetOfferBehaviorModelsQuery;
using static Business.Handlers.OfferBehaviorModels.Commands.CreateOfferBehaviorModelCommand;
using Business.Handlers.OfferBehaviorModels.Commands;
using Business.Constants;
using static Business.Handlers.OfferBehaviorModels.Commands.UpdateOfferBehaviorModelCommand;
using static Business.Handlers.OfferBehaviorModels.Commands.DeleteOfferBehaviorModelCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OfferBehaviorModelHandlerTests
    {
        Mock<IOfferBehaviorModelRepository> _offerBehaviorModelRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _offerBehaviorModelRepository = new Mock<IOfferBehaviorModelRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OfferBehaviorModel_GetQuery_Success()
        {
            //Arrange
            var query = new GetOfferBehaviorModelQuery();

            _offerBehaviorModelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new OfferBehaviorModel()
//propertyler buraya yazılacak
//{																		
//OfferBehaviorModelId = 1,
//OfferBehaviorModelName = "Test"
//}
);

            var handler = new GetOfferBehaviorModelQueryHandler(_offerBehaviorModelRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OfferBehaviorModelId.Should().Be(1);

        }

        [Test]
        public async Task OfferBehaviorModel_GetQueries_Success()
        {
            //Arrange
            var query = new GetOfferBehaviorModelsQuery();

            _offerBehaviorModelRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OfferBehaviorModel, bool>>>()))
                        .ReturnsAsync(new List<OfferBehaviorModel> { new OfferBehaviorModel() { /*TODO:propertyler buraya yazılacak OfferBehaviorModelId = 1, OfferBehaviorModelName = "test"*/ } }.AsQueryable());

            var handler = new GetOfferBehaviorModelsQueryHandler(_offerBehaviorModelRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OfferBehaviorModel>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task OfferBehaviorModel_CreateCommand_Success()
        {
            OfferBehaviorModel rt = null;
            //Arrange
            var command = new CreateOfferBehaviorModelCommand();
            //propertyler buraya yazılacak
            //command.OfferBehaviorModelName = "deneme";

            _offerBehaviorModelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _offerBehaviorModelRepository.Setup(x => x.Add(It.IsAny<OfferBehaviorModel>()));

            var handler = new CreateOfferBehaviorModelCommandHandler(_offerBehaviorModelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OfferBehaviorModel_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOfferBehaviorModelCommand();
            //propertyler buraya yazılacak 
            //command.OfferBehaviorModelName = "test";

            _offerBehaviorModelRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OfferBehaviorModel, bool>>>()))
                                           .ReturnsAsync(new List<OfferBehaviorModel> { new OfferBehaviorModel() { /*TODO:propertyler buraya yazılacak OfferBehaviorModelId = 1, OfferBehaviorModelName = "test"*/ } }.AsQueryable());

            _offerBehaviorModelRepository.Setup(x => x.Add(It.IsAny<OfferBehaviorModel>()));

            var handler = new CreateOfferBehaviorModelCommandHandler(_offerBehaviorModelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OfferBehaviorModel_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOfferBehaviorModelCommand();
            //command.OfferBehaviorModelName = "test";

            _offerBehaviorModelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new OfferBehaviorModel() { /*TODO:propertyler buraya yazılacak OfferBehaviorModelId = 1, OfferBehaviorModelName = "deneme"*/ });

            _offerBehaviorModelRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<OfferBehaviorModel>()));

            var handler = new UpdateOfferBehaviorModelCommandHandler(_offerBehaviorModelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OfferBehaviorModel_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOfferBehaviorModelCommand();

            _offerBehaviorModelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new OfferBehaviorModel() { /*TODO:propertyler buraya yazılacak OfferBehaviorModelId = 1, OfferBehaviorModelName = "deneme"*/});

            _offerBehaviorModelRepository.Setup(x => x.Delete(It.IsAny<OfferBehaviorModel>()));

            var handler = new DeleteOfferBehaviorModelCommandHandler(_offerBehaviorModelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

