
using Business.Handlers.ProjectBaseTotalDieWithDifficulties.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ProjectBaseTotalDieWithDifficulties.Queries.GetProjectBaseTotalDieWithDifficultyQuery;
using Entities.Concrete;
using static Business.Handlers.ProjectBaseTotalDieWithDifficulties.Queries.GetProjectBaseTotalDieWithDifficultiesQuery;
using static Business.Handlers.ProjectBaseTotalDieWithDifficulties.Commands.CreateProjectBaseTotalDieWithDifficultyCommand;
using Business.Handlers.ProjectBaseTotalDieWithDifficulties.Commands;
using Business.Constants;
using static Business.Handlers.ProjectBaseTotalDieWithDifficulties.Commands.UpdateProjectBaseTotalDieWithDifficultyCommand;
using static Business.Handlers.ProjectBaseTotalDieWithDifficulties.Commands.DeleteProjectBaseTotalDieWithDifficultyCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ProjectBaseTotalDieWithDifficultyHandlerTests
    {
        Mock<IProjectBaseTotalDieWithDifficultyRepository> _projectBaseTotalDieWithDifficultyRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _projectBaseTotalDieWithDifficultyRepository = new Mock<IProjectBaseTotalDieWithDifficultyRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ProjectBaseTotalDieWithDifficulty_GetQuery_Success()
        {
            //Arrange
            var query = new GetProjectBaseTotalDieWithDifficultyQuery();

            _projectBaseTotalDieWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new ProjectTotalDieWithDifficulty()
//propertyler buraya yazılacak
//{																		
//ProjectBaseTotalDieWithDifficultyId = 1,
//ProjectBaseTotalDieWithDifficultyName = "Test"
//}
);

            var handler = new GetProjectBaseTotalDieWithDifficultyQueryHandler(_projectBaseTotalDieWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ProjectBaseTotalDieWithDifficultyId.Should().Be(1);

        }

        [Test]
        public async Task ProjectBaseTotalDieWithDifficulty_GetQueries_Success()
        {
            //Arrange
            var query = new GetProjectBaseTotalDieWithDifficultiesQuery();

            _projectBaseTotalDieWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ProjectTotalDieWithDifficulty, bool>>>()))
                        .ReturnsAsync(new List<ProjectTotalDieWithDifficulty> { new ProjectTotalDieWithDifficulty() { /*TODO:propertyler buraya yazılacak ProjectBaseTotalDieWithDifficultyId = 1, ProjectBaseTotalDieWithDifficultyName = "test"*/ } }.AsQueryable());

            var handler = new GetProjectBaseTotalDieWithDifficultiesQueryHandler(_projectBaseTotalDieWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ProjectTotalDieWithDifficulty>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task ProjectBaseTotalDieWithDifficulty_CreateCommand_Success()
        {
            ProjectTotalDieWithDifficulty rt = null;
            //Arrange
            var command = new CreateProjectBaseTotalDieWithDifficultyCommand();
            //propertyler buraya yazılacak
            //command.ProjectBaseTotalDieWithDifficultyName = "deneme";

            _projectBaseTotalDieWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _projectBaseTotalDieWithDifficultyRepository.Setup(x => x.Add(It.IsAny<ProjectTotalDieWithDifficulty>()));

            var handler = new CreateProjectBaseTotalDieWithDifficultyCommandHandler(_projectBaseTotalDieWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ProjectBaseTotalDieWithDifficulty_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateProjectBaseTotalDieWithDifficultyCommand();
            //propertyler buraya yazılacak 
            //command.ProjectBaseTotalDieWithDifficultyName = "test";

            _projectBaseTotalDieWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ProjectTotalDieWithDifficulty, bool>>>()))
                                           .ReturnsAsync(new List<ProjectTotalDieWithDifficulty> { new ProjectTotalDieWithDifficulty() { /*TODO:propertyler buraya yazılacak ProjectBaseTotalDieWithDifficultyId = 1, ProjectBaseTotalDieWithDifficultyName = "test"*/ } }.AsQueryable());

            _projectBaseTotalDieWithDifficultyRepository.Setup(x => x.Add(It.IsAny<ProjectTotalDieWithDifficulty>()));

            var handler = new CreateProjectBaseTotalDieWithDifficultyCommandHandler(_projectBaseTotalDieWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ProjectBaseTotalDieWithDifficulty_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateProjectBaseTotalDieWithDifficultyCommand();
            //command.ProjectBaseTotalDieWithDifficultyName = "test";

            _projectBaseTotalDieWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new ProjectTotalDieWithDifficulty() { /*TODO:propertyler buraya yazılacak ProjectBaseTotalDieWithDifficultyId = 1, ProjectBaseTotalDieWithDifficultyName = "deneme"*/ });

            _projectBaseTotalDieWithDifficultyRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<ProjectTotalDieWithDifficulty>()));

            var handler = new UpdateProjectBaseTotalDieWithDifficultyCommandHandler(_projectBaseTotalDieWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ProjectBaseTotalDieWithDifficulty_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteProjectBaseTotalDieWithDifficultyCommand();

            _projectBaseTotalDieWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new ProjectTotalDieWithDifficulty() { /*TODO:propertyler buraya yazılacak ProjectBaseTotalDieWithDifficultyId = 1, ProjectBaseTotalDieWithDifficultyName = "deneme"*/});

            _projectBaseTotalDieWithDifficultyRepository.Setup(x => x.Delete(It.IsAny<ProjectTotalDieWithDifficulty>()));

            var handler = new DeleteProjectBaseTotalDieWithDifficultyCommandHandler(_projectBaseTotalDieWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

