
using Business.Handlers.ProjectBaseDetaildSessions.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ProjectBaseDetaildSessions.Queries.GetProjectBaseDetaildSessionQuery;
using Entities.Concrete;
using static Business.Handlers.ProjectBaseDetaildSessions.Queries.GetProjectBaseDetaildSessionsQuery;
using static Business.Handlers.ProjectBaseDetaildSessions.Commands.CreateProjectBaseDetaildSessionCommand;
using Business.Handlers.ProjectBaseDetaildSessions.Commands;
using Business.Constants;
using static Business.Handlers.ProjectBaseDetaildSessions.Commands.UpdateProjectBaseDetaildSessionCommand;
using static Business.Handlers.ProjectBaseDetaildSessions.Commands.DeleteProjectBaseDetaildSessionCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ProjectBaseDetaildSessionHandlerTests
    {
        Mock<IProjectBaseDetaildSessionRepository> _projectBaseDetaildSessionRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _projectBaseDetaildSessionRepository = new Mock<IProjectBaseDetaildSessionRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ProjectBaseDetaildSession_GetQuery_Success()
        {
            //Arrange
            var query = new GetProjectBaseDetaildSessionQuery();

            _projectBaseDetaildSessionRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new DetaildSession()
//propertyler buraya yazılacak
//{																		
//ProjectBaseDetaildSessionId = 1,
//ProjectBaseDetaildSessionName = "Test"
//}
);

            var handler = new GetProjectBaseDetaildSessionQueryHandler(_projectBaseDetaildSessionRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ProjectBaseDetaildSessionId.Should().Be(1);

        }

        [Test]
        public async Task ProjectBaseDetaildSession_GetQueries_Success()
        {
            //Arrange
            var query = new GetProjectBaseDetaildSessionsQuery();

            _projectBaseDetaildSessionRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<DetaildSession, bool>>>()))
                        .ReturnsAsync(new List<DetaildSession> { new DetaildSession() { /*TODO:propertyler buraya yazılacak ProjectBaseDetaildSessionId = 1, ProjectBaseDetaildSessionName = "test"*/ } }.AsQueryable());

            var handler = new GetProjectBaseDetaildSessionsQueryHandler(_projectBaseDetaildSessionRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<DetaildSession>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task ProjectBaseDetaildSession_CreateCommand_Success()
        {
            DetaildSession rt = null;
            //Arrange
            var command = new CreateProjectBaseDetaildSessionCommand();
            //propertyler buraya yazılacak
            //command.ProjectBaseDetaildSessionName = "deneme";

            _projectBaseDetaildSessionRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _projectBaseDetaildSessionRepository.Setup(x => x.Add(It.IsAny<DetaildSession>()));

            var handler = new CreateProjectBaseDetaildSessionCommandHandler(_projectBaseDetaildSessionRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ProjectBaseDetaildSession_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateProjectBaseDetaildSessionCommand();
            //propertyler buraya yazılacak 
            //command.ProjectBaseDetaildSessionName = "test";

            _projectBaseDetaildSessionRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<DetaildSession, bool>>>()))
                                           .ReturnsAsync(new List<DetaildSession> { new DetaildSession() { /*TODO:propertyler buraya yazılacak ProjectBaseDetaildSessionId = 1, ProjectBaseDetaildSessionName = "test"*/ } }.AsQueryable());

            _projectBaseDetaildSessionRepository.Setup(x => x.Add(It.IsAny<DetaildSession>()));

            var handler = new CreateProjectBaseDetaildSessionCommandHandler(_projectBaseDetaildSessionRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ProjectBaseDetaildSession_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateProjectBaseDetaildSessionCommand();
            //command.ProjectBaseDetaildSessionName = "test";

            _projectBaseDetaildSessionRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new DetaildSession() { /*TODO:propertyler buraya yazılacak ProjectBaseDetaildSessionId = 1, ProjectBaseDetaildSessionName = "deneme"*/ });

            _projectBaseDetaildSessionRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<DetaildSession>()));

            var handler = new UpdateProjectBaseDetaildSessionCommandHandler(_projectBaseDetaildSessionRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ProjectBaseDetaildSession_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteProjectBaseDetaildSessionCommand();

            _projectBaseDetaildSessionRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new DetaildSession() { /*TODO:propertyler buraya yazılacak ProjectBaseDetaildSessionId = 1, ProjectBaseDetaildSessionName = "deneme"*/});

            _projectBaseDetaildSessionRepository.Setup(x => x.Delete(It.IsAny<DetaildSession>()));

            var handler = new DeleteProjectBaseDetaildSessionCommandHandler(_projectBaseDetaildSessionRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

