
using Business.Handlers.ProjectBaseFinishingScoreWithLevels.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ProjectBaseFinishingScoreWithLevels.Queries.GetProjectBaseFinishingScoreWithLevelQuery;
using Entities.Concrete;
using static Business.Handlers.ProjectBaseFinishingScoreWithLevels.Queries.GetProjectBaseFinishingScoreWithLevelsQuery;
using static Business.Handlers.ProjectBaseFinishingScoreWithLevels.Commands.CreateProjectBaseFinishingScoreWithLevelCommand;
using Business.Handlers.ProjectBaseFinishingScoreWithLevels.Commands;
using Business.Constants;
using static Business.Handlers.ProjectBaseFinishingScoreWithLevels.Commands.UpdateProjectBaseFinishingScoreWithLevelCommand;
using static Business.Handlers.ProjectBaseFinishingScoreWithLevels.Commands.DeleteProjectBaseFinishingScoreWithLevelCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ProjectBaseFinishingScoreWithLevelHandlerTests
    {
        Mock<IProjectBaseFinishingScoreWithLevelRepository> _projectBaseFinishingScoreWithLevelRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _projectBaseFinishingScoreWithLevelRepository = new Mock<IProjectBaseFinishingScoreWithLevelRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ProjectBaseFinishingScoreWithLevel_GetQuery_Success()
        {
            //Arrange
            var query = new GetProjectBaseFinishingScoreWithLevelQuery();

            _projectBaseFinishingScoreWithLevelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new FinishingScoreWithLevel()
//propertyler buraya yazılacak
//{																		
//ProjectBaseFinishingScoreWithLevelId = 1,
//ProjectBaseFinishingScoreWithLevelName = "Test"
//}
);

            var handler = new GetProjectBaseFinishingScoreWithLevelQueryHandler(_projectBaseFinishingScoreWithLevelRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ProjectBaseFinishingScoreWithLevelId.Should().Be(1);

        }

        [Test]
        public async Task ProjectBaseFinishingScoreWithLevel_GetQueries_Success()
        {
            //Arrange
            var query = new GetProjectBaseFinishingScoreWithLevelsQuery();

            _projectBaseFinishingScoreWithLevelRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<FinishingScoreWithLevel, bool>>>()))
                        .ReturnsAsync(new List<FinishingScoreWithLevel> { new FinishingScoreWithLevel() { /*TODO:propertyler buraya yazılacak ProjectBaseFinishingScoreWithLevelId = 1, ProjectBaseFinishingScoreWithLevelName = "test"*/ } }.AsQueryable());

            var handler = new GetProjectBaseFinishingScoreWithLevelsQueryHandler(_projectBaseFinishingScoreWithLevelRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<FinishingScoreWithLevel>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task ProjectBaseFinishingScoreWithLevel_CreateCommand_Success()
        {
            FinishingScoreWithLevel rt = null;
            //Arrange
            var command = new CreateProjectBaseFinishingScoreWithLevelCommand();
            //propertyler buraya yazılacak
            //command.ProjectBaseFinishingScoreWithLevelName = "deneme";

            _projectBaseFinishingScoreWithLevelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _projectBaseFinishingScoreWithLevelRepository.Setup(x => x.Add(It.IsAny<FinishingScoreWithLevel>()));

            var handler = new CreateProjectBaseFinishingScoreWithLevelCommandHandler(_projectBaseFinishingScoreWithLevelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ProjectBaseFinishingScoreWithLevel_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateProjectBaseFinishingScoreWithLevelCommand();
            //propertyler buraya yazılacak 
            //command.ProjectBaseFinishingScoreWithLevelName = "test";

            _projectBaseFinishingScoreWithLevelRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<FinishingScoreWithLevel, bool>>>()))
                                           .ReturnsAsync(new List<FinishingScoreWithLevel> { new FinishingScoreWithLevel() { /*TODO:propertyler buraya yazılacak ProjectBaseFinishingScoreWithLevelId = 1, ProjectBaseFinishingScoreWithLevelName = "test"*/ } }.AsQueryable());

            _projectBaseFinishingScoreWithLevelRepository.Setup(x => x.Add(It.IsAny<FinishingScoreWithLevel>()));

            var handler = new CreateProjectBaseFinishingScoreWithLevelCommandHandler(_projectBaseFinishingScoreWithLevelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ProjectBaseFinishingScoreWithLevel_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateProjectBaseFinishingScoreWithLevelCommand();
            //command.ProjectBaseFinishingScoreWithLevelName = "test";

            _projectBaseFinishingScoreWithLevelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new FinishingScoreWithLevel() { /*TODO:propertyler buraya yazılacak ProjectBaseFinishingScoreWithLevelId = 1, ProjectBaseFinishingScoreWithLevelName = "deneme"*/ });

            _projectBaseFinishingScoreWithLevelRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<FinishingScoreWithLevel>()));

            var handler = new UpdateProjectBaseFinishingScoreWithLevelCommandHandler(_projectBaseFinishingScoreWithLevelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ProjectBaseFinishingScoreWithLevel_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteProjectBaseFinishingScoreWithLevelCommand();

            _projectBaseFinishingScoreWithLevelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new FinishingScoreWithLevel() { /*TODO:propertyler buraya yazılacak ProjectBaseFinishingScoreWithLevelId = 1, ProjectBaseFinishingScoreWithLevelName = "deneme"*/});

            _projectBaseFinishingScoreWithLevelRepository.Setup(x => x.Delete(It.IsAny<FinishingScoreWithLevel>()));

            var handler = new DeleteProjectBaseFinishingScoreWithLevelCommandHandler(_projectBaseFinishingScoreWithLevelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

