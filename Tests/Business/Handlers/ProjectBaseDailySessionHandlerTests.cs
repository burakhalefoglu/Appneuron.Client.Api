
using Business.Handlers.ProjectBaseDailySessions.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ProjectBaseDailySessions.Queries.GetProjectBaseDailySessionQuery;
using Entities.Concrete;
using static Business.Handlers.ProjectBaseDailySessions.Queries.GetProjectBaseDailySessionsQuery;
using static Business.Handlers.ProjectBaseDailySessions.Commands.CreateProjectBaseDailySessionCommand;
using Business.Handlers.ProjectBaseDailySessions.Commands;
using Business.Constants;
using static Business.Handlers.ProjectBaseDailySessions.Commands.UpdateProjectBaseDailySessionCommand;
using static Business.Handlers.ProjectBaseDailySessions.Commands.DeleteProjectBaseDailySessionCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ProjectBaseDailySessionHandlerTests
    {
        Mock<IProjectBaseDailySessionRepository> _projectBaseDailySessionRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _projectBaseDailySessionRepository = new Mock<IProjectBaseDailySessionRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ProjectBaseDailySession_GetQuery_Success()
        {
            //Arrange
            var query = new GetProjectBaseDailySessionQuery();

            _projectBaseDailySessionRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new ProjectDailySession()
//propertyler buraya yazılacak
//{																		
//ProjectBaseDailySessionId = 1,
//ProjectBaseDailySessionName = "Test"
//}
);

            var handler = new GetProjectBaseDailySessionQueryHandler(_projectBaseDailySessionRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ProjectBaseDailySessionId.Should().Be(1);

        }

        [Test]
        public async Task ProjectBaseDailySession_GetQueries_Success()
        {
            //Arrange
            var query = new GetProjectBaseDailySessionsQuery();

            _projectBaseDailySessionRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ProjectDailySession, bool>>>()))
                        .ReturnsAsync(new List<ProjectDailySession> { new ProjectDailySession() { /*TODO:propertyler buraya yazılacak ProjectBaseDailySessionId = 1, ProjectBaseDailySessionName = "test"*/ } }.AsQueryable());

            var handler = new GetProjectBaseDailySessionsQueryHandler(_projectBaseDailySessionRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ProjectDailySession>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task ProjectBaseDailySession_CreateCommand_Success()
        {
            ProjectDailySession rt = null;
            //Arrange
            var command = new CreateProjectBaseDailySessionCommand();
            //propertyler buraya yazılacak
            //command.ProjectBaseDailySessionName = "deneme";

            _projectBaseDailySessionRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _projectBaseDailySessionRepository.Setup(x => x.Add(It.IsAny<ProjectDailySession>()));

            var handler = new CreateProjectBaseDailySessionCommandHandler(_projectBaseDailySessionRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ProjectBaseDailySession_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateProjectBaseDailySessionCommand();
            //propertyler buraya yazılacak 
            //command.ProjectBaseDailySessionName = "test";

            _projectBaseDailySessionRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ProjectDailySession, bool>>>()))
                                           .ReturnsAsync(new List<ProjectDailySession> { new ProjectDailySession() { /*TODO:propertyler buraya yazılacak ProjectBaseDailySessionId = 1, ProjectBaseDailySessionName = "test"*/ } }.AsQueryable());

            _projectBaseDailySessionRepository.Setup(x => x.Add(It.IsAny<ProjectDailySession>()));

            var handler = new CreateProjectBaseDailySessionCommandHandler(_projectBaseDailySessionRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ProjectBaseDailySession_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateProjectBaseDailySessionCommand();
            //command.ProjectBaseDailySessionName = "test";

            _projectBaseDailySessionRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new ProjectDailySession() { /*TODO:propertyler buraya yazılacak ProjectBaseDailySessionId = 1, ProjectBaseDailySessionName = "deneme"*/ });

            _projectBaseDailySessionRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<ProjectDailySession>()));

            var handler = new UpdateProjectBaseDailySessionCommandHandler(_projectBaseDailySessionRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ProjectBaseDailySession_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteProjectBaseDailySessionCommand();

            _projectBaseDailySessionRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new ProjectDailySession() { /*TODO:propertyler buraya yazılacak ProjectBaseDailySessionId = 1, ProjectBaseDailySessionName = "deneme"*/});

            _projectBaseDailySessionRepository.Setup(x => x.Delete(It.IsAny<ProjectDailySession>()));

            var handler = new DeleteProjectBaseDailySessionCommandHandler(_projectBaseDailySessionRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

