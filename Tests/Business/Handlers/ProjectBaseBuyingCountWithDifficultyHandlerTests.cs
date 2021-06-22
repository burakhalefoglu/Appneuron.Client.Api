
using Business.Handlers.ProjectBaseBuyingCountWithDifficulties.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ProjectBaseBuyingCountWithDifficulties.Queries.GetProjectBaseBuyingCountWithDifficultyQuery;
using Entities.Concrete;
using static Business.Handlers.ProjectBaseBuyingCountWithDifficulties.Queries.GetProjectBaseBuyingCountWithDifficultiesQuery;
using static Business.Handlers.ProjectBaseBuyingCountWithDifficulties.Commands.CreateProjectBaseBuyingCountWithDifficultyCommand;
using Business.Handlers.ProjectBaseBuyingCountWithDifficulties.Commands;
using Business.Constants;
using static Business.Handlers.ProjectBaseBuyingCountWithDifficulties.Commands.UpdateProjectBaseBuyingCountWithDifficultyCommand;
using static Business.Handlers.ProjectBaseBuyingCountWithDifficulties.Commands.DeleteProjectBaseBuyingCountWithDifficultyCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ProjectBaseBuyingCountWithDifficultyHandlerTests
    {
        Mock<IProjectBaseBuyingCountWithDifficultyRepository> _projectBaseBuyingCountWithDifficultyRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _projectBaseBuyingCountWithDifficultyRepository = new Mock<IProjectBaseBuyingCountWithDifficultyRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ProjectBaseBuyingCountWithDifficulty_GetQuery_Success()
        {
            //Arrange
            var query = new GetProjectBaseBuyingCountWithDifficultyQuery();

            _projectBaseBuyingCountWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new ProjectBuyingCountWithDifficulty()
//propertyler buraya yazılacak
//{																		
//ProjectBaseBuyingCountWithDifficultyId = 1,
//ProjectBaseBuyingCountWithDifficultyName = "Test"
//}
);

            var handler = new GetProjectBaseBuyingCountWithDifficultyQueryHandler(_projectBaseBuyingCountWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ProjectBaseBuyingCountWithDifficultyId.Should().Be(1);

        }

        [Test]
        public async Task ProjectBaseBuyingCountWithDifficulty_GetQueries_Success()
        {
            //Arrange
            var query = new GetProjectBaseBuyingCountWithDifficultiesQuery();

            _projectBaseBuyingCountWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ProjectBuyingCountWithDifficulty, bool>>>()))
                        .ReturnsAsync(new List<ProjectBuyingCountWithDifficulty> { new ProjectBuyingCountWithDifficulty() { /*TODO:propertyler buraya yazılacak ProjectBaseBuyingCountWithDifficultyId = 1, ProjectBaseBuyingCountWithDifficultyName = "test"*/ } }.AsQueryable());

            var handler = new GetProjectBaseBuyingCountWithDifficultiesQueryHandler(_projectBaseBuyingCountWithDifficultyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ProjectBuyingCountWithDifficulty>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task ProjectBaseBuyingCountWithDifficulty_CreateCommand_Success()
        {
            ProjectBuyingCountWithDifficulty rt = null;
            //Arrange
            var command = new CreateProjectBaseBuyingCountWithDifficultyCommand();
            //propertyler buraya yazılacak
            //command.ProjectBaseBuyingCountWithDifficultyName = "deneme";

            _projectBaseBuyingCountWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _projectBaseBuyingCountWithDifficultyRepository.Setup(x => x.Add(It.IsAny<ProjectBuyingCountWithDifficulty>()));

            var handler = new CreateProjectBaseBuyingCountWithDifficultyCommandHandler(_projectBaseBuyingCountWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ProjectBaseBuyingCountWithDifficulty_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateProjectBaseBuyingCountWithDifficultyCommand();
            //propertyler buraya yazılacak 
            //command.ProjectBaseBuyingCountWithDifficultyName = "test";

            _projectBaseBuyingCountWithDifficultyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ProjectBuyingCountWithDifficulty, bool>>>()))
                                           .ReturnsAsync(new List<ProjectBuyingCountWithDifficulty> { new ProjectBuyingCountWithDifficulty() { /*TODO:propertyler buraya yazılacak ProjectBaseBuyingCountWithDifficultyId = 1, ProjectBaseBuyingCountWithDifficultyName = "test"*/ } }.AsQueryable());

            _projectBaseBuyingCountWithDifficultyRepository.Setup(x => x.Add(It.IsAny<ProjectBuyingCountWithDifficulty>()));

            var handler = new CreateProjectBaseBuyingCountWithDifficultyCommandHandler(_projectBaseBuyingCountWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ProjectBaseBuyingCountWithDifficulty_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateProjectBaseBuyingCountWithDifficultyCommand();
            //command.ProjectBaseBuyingCountWithDifficultyName = "test";

            _projectBaseBuyingCountWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new ProjectBuyingCountWithDifficulty() { /*TODO:propertyler buraya yazılacak ProjectBaseBuyingCountWithDifficultyId = 1, ProjectBaseBuyingCountWithDifficultyName = "deneme"*/ });

            _projectBaseBuyingCountWithDifficultyRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<ProjectBuyingCountWithDifficulty>()));

            var handler = new UpdateProjectBaseBuyingCountWithDifficultyCommandHandler(_projectBaseBuyingCountWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ProjectBaseBuyingCountWithDifficulty_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteProjectBaseBuyingCountWithDifficultyCommand();

            _projectBaseBuyingCountWithDifficultyRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new ProjectBuyingCountWithDifficulty() { /*TODO:propertyler buraya yazılacak ProjectBaseBuyingCountWithDifficultyId = 1, ProjectBaseBuyingCountWithDifficultyName = "deneme"*/});

            _projectBaseBuyingCountWithDifficultyRepository.Setup(x => x.Delete(It.IsAny<ProjectBuyingCountWithDifficulty>()));

            var handler = new DeleteProjectBaseBuyingCountWithDifficultyCommandHandler(_projectBaseBuyingCountWithDifficultyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

