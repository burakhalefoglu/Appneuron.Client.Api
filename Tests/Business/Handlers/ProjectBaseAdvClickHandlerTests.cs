
using Business.Handlers.ProjectBaseAdvClicks.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ProjectBaseAdvClicks.Queries.GetProjectBaseAdvClickQuery;
using Entities.Concrete;
using static Business.Handlers.ProjectBaseAdvClicks.Queries.GetProjectBaseAdvClicksQuery;
using static Business.Handlers.ProjectBaseAdvClicks.Commands.CreateProjectBaseAdvClickCommand;
using Business.Handlers.ProjectBaseAdvClicks.Commands;
using Business.Constants;
using static Business.Handlers.ProjectBaseAdvClicks.Commands.UpdateProjectBaseAdvClickCommand;
using static Business.Handlers.ProjectBaseAdvClicks.Commands.DeleteProjectBaseAdvClickCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ProjectBaseAdvClickHandlerTests
    {
        Mock<IProjectBaseAdvClickRepository> _projectBaseAdvClickRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _projectBaseAdvClickRepository = new Mock<IProjectBaseAdvClickRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ProjectBaseAdvClick_GetQuery_Success()
        {
            //Arrange
            var query = new GetProjectBaseAdvClickQuery();

            _projectBaseAdvClickRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new AdvClick()
//propertyler buraya yazılacak
//{																		
//ProjectBaseAdvClickId = 1,
//ProjectBaseAdvClickName = "Test"
//}
);

            var handler = new GetProjectBaseAdvClickQueryHandler(_projectBaseAdvClickRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ProjectBaseAdvClickId.Should().Be(1);

        }

        [Test]
        public async Task ProjectBaseAdvClick_GetQueries_Success()
        {
            //Arrange
            var query = new GetProjectBaseAdvClicksQuery();

            _projectBaseAdvClickRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<AdvClick, bool>>>()))
                        .ReturnsAsync(new List<AdvClick> { new AdvClick() { /*TODO:propertyler buraya yazılacak ProjectBaseAdvClickId = 1, ProjectBaseAdvClickName = "test"*/ } }.AsQueryable());

            var handler = new GetProjectBaseAdvClicksQueryHandler(_projectBaseAdvClickRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<AdvClick>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task ProjectBaseAdvClick_CreateCommand_Success()
        {
            AdvClick rt = null;
            //Arrange
            var command = new CreateProjectBaseAdvClickCommand();
            //propertyler buraya yazılacak
            //command.ProjectBaseAdvClickName = "deneme";

            _projectBaseAdvClickRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _projectBaseAdvClickRepository.Setup(x => x.Add(It.IsAny<AdvClick>()));

            var handler = new CreateProjectBaseAdvClickCommandHandler(_projectBaseAdvClickRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ProjectBaseAdvClick_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateProjectBaseAdvClickCommand();
            //propertyler buraya yazılacak 
            //command.ProjectBaseAdvClickName = "test";

            _projectBaseAdvClickRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<AdvClick, bool>>>()))
                                           .ReturnsAsync(new List<AdvClick> { new AdvClick() { /*TODO:propertyler buraya yazılacak ProjectBaseAdvClickId = 1, ProjectBaseAdvClickName = "test"*/ } }.AsQueryable());

            _projectBaseAdvClickRepository.Setup(x => x.Add(It.IsAny<AdvClick>()));

            var handler = new CreateProjectBaseAdvClickCommandHandler(_projectBaseAdvClickRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ProjectBaseAdvClick_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateProjectBaseAdvClickCommand();
            //command.ProjectBaseAdvClickName = "test";

            _projectBaseAdvClickRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new AdvClick() { /*TODO:propertyler buraya yazılacak ProjectBaseAdvClickId = 1, ProjectBaseAdvClickName = "deneme"*/ });

            _projectBaseAdvClickRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<AdvClick>()));

            var handler = new UpdateProjectBaseAdvClickCommandHandler(_projectBaseAdvClickRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ProjectBaseAdvClick_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteProjectBaseAdvClickCommand();

            _projectBaseAdvClickRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new AdvClick() { /*TODO:propertyler buraya yazılacak ProjectBaseAdvClickId = 1, ProjectBaseAdvClickName = "deneme"*/});

            _projectBaseAdvClickRepository.Setup(x => x.Delete(It.IsAny<AdvClick>()));

            var handler = new DeleteProjectBaseAdvClickCommandHandler(_projectBaseAdvClickRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

