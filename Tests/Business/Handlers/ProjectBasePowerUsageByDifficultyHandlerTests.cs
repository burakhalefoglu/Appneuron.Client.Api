
using Business.Handlers.ProjectBasePowerUsageByDifficulties.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ProjectBasePowerUsageByDifficulties.Queries.GetProjectBasePowerUsageByDifficultyQuery;
using Entities.Concrete;
using static Business.Handlers.ProjectBasePowerUsageByDifficulties.Queries.GetProjectBasePowerUsageByDifficultiesQuery;
using static Business.Handlers.ProjectBasePowerUsageByDifficulties.Commands.CreateProjectBasePowerUsageByDifficultyCommand;
using Business.Handlers.ProjectBasePowerUsageByDifficulties.Commands;
using Business.Constants;
using static Business.Handlers.ProjectBasePowerUsageByDifficulties.Commands.UpdateProjectBasePowerUsageByDifficultyCommand;
using static Business.Handlers.ProjectBasePowerUsageByDifficulties.Commands.DeleteProjectBasePowerUsageByDifficultyCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ProjectBasePowerUsageByDifficultyHandlerTests
    {
        Mock<IProjectBasePowerUsageByDifficultyRepository> _projectBasePowerUsageByDifficultyRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _projectBasePowerUsageByDifficultyRepository = new Mock<IProjectBasePowerUsageByDifficultyRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ProjectBasePowerUsageByDifficulty_GetQuery_Success()
        {
            //Arrange
            var query = new GetProjectBasePowerUsageByDifficultyQuery();

            _projectBasePowerUsageByDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new ProjectBasePowerUsageByDifficulty()
//propertyler buraya yazılacak
//{																		
//ProjectBasePowerUsageByDifficultyId = 1,
//ProjectBasePowerUsageByDifficultyName = "Test"
//}
);

            var handler = new GetProjectBasePowerUsageByDifficultyQueryHandler(_projectBasePowerUsageByDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ProjectBasePowerUsageByDifficultyId.Should().Be(1);

        }

        [Test]
        public async Task ProjectBasePowerUsageByDifficulty_GetQueries_Success()
        {
            //Arrange
            var query = new GetProjectBasePowerUsageByDifficultiesQuery();

            _projectBasePowerUsageByDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ProjectBasePowerUsageByDifficulty, bool>>>()))
                        .ReturnsAsync(new List<ProjectBasePowerUsageByDifficulty> { new ProjectBasePowerUsageByDifficulty() { /*TODO:propertyler buraya yazılacak ProjectBasePowerUsageByDifficultyId = 1, ProjectBasePowerUsageByDifficultyName = "test"*/ } }.AsQueryable());

            var handler = new GetProjectBasePowerUsageByDifficultiesQueryHandler(_projectBasePowerUsageByDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ProjectBasePowerUsageByDifficulty>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task ProjectBasePowerUsageByDifficulty_CreateCommand_Success()
        {
            ProjectBasePowerUsageByDifficulty rt = null;
            //Arrange
            var command = new CreateProjectBasePowerUsageByDifficultyCommand();
            //propertyler buraya yazılacak
            //command.ProjectBasePowerUsageByDifficultyName = "deneme";

            _projectBasePowerUsageByDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _projectBasePowerUsageByDifficultyRepository.Setup(x => x.Add(It.IsAny<ProjectBasePowerUsageByDifficulty>()));

            var handler = new CreateProjectBasePowerUsageByDifficultyCommandHandler(_projectBasePowerUsageByDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ProjectBasePowerUsageByDifficulty_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateProjectBasePowerUsageByDifficultyCommand();
            //propertyler buraya yazılacak 
            //command.ProjectBasePowerUsageByDifficultyName = "test";

            _projectBasePowerUsageByDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ProjectBasePowerUsageByDifficulty, bool>>>()))
                                           .ReturnsAsync(new List<ProjectBasePowerUsageByDifficulty> { new ProjectBasePowerUsageByDifficulty() { /*TODO:propertyler buraya yazılacak ProjectBasePowerUsageByDifficultyId = 1, ProjectBasePowerUsageByDifficultyName = "test"*/ } }.AsQueryable());

            _projectBasePowerUsageByDifficultyRepository.Setup(x => x.Add(It.IsAny<ProjectBasePowerUsageByDifficulty>()));

            var handler = new CreateProjectBasePowerUsageByDifficultyCommandHandler(_projectBasePowerUsageByDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ProjectBasePowerUsageByDifficulty_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateProjectBasePowerUsageByDifficultyCommand();
            //command.ProjectBasePowerUsageByDifficultyName = "test";

            _projectBasePowerUsageByDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new ProjectBasePowerUsageByDifficulty() { /*TODO:propertyler buraya yazılacak ProjectBasePowerUsageByDifficultyId = 1, ProjectBasePowerUsageByDifficultyName = "deneme"*/ });

            _projectBasePowerUsageByDifficultyRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<ProjectBasePowerUsageByDifficulty>()));

            var handler = new UpdateProjectBasePowerUsageByDifficultyCommandHandler(_projectBasePowerUsageByDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ProjectBasePowerUsageByDifficulty_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteProjectBasePowerUsageByDifficultyCommand();

            _projectBasePowerUsageByDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new ProjectBasePowerUsageByDifficulty() { /*TODO:propertyler buraya yazılacak ProjectBasePowerUsageByDifficultyId = 1, ProjectBasePowerUsageByDifficultyName = "deneme"*/});

            _projectBasePowerUsageByDifficultyRepository.Setup(x => x.Delete(It.IsAny<ProjectBasePowerUsageByDifficulty>()));

            var handler = new DeleteProjectBasePowerUsageByDifficultyCommandHandler(_projectBasePowerUsageByDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

