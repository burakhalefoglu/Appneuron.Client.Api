
using Business.Handlers.ProjectBaseDieCountWithLevels.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ProjectBaseDieCountWithLevels.Queries.GetProjectBaseDieCountWithLevelQuery;
using Entities.Concrete;
using static Business.Handlers.ProjectBaseDieCountWithLevels.Queries.GetProjectBaseDieCountWithLevelsQuery;
using static Business.Handlers.ProjectBaseDieCountWithLevels.Commands.CreateProjectBaseDieCountWithLevelCommand;
using Business.Handlers.ProjectBaseDieCountWithLevels.Commands;
using Business.Constants;
using static Business.Handlers.ProjectBaseDieCountWithLevels.Commands.UpdateProjectBaseDieCountWithLevelCommand;
using static Business.Handlers.ProjectBaseDieCountWithLevels.Commands.DeleteProjectBaseDieCountWithLevelCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ProjectBaseDieCountWithLevelHandlerTests
    {
        Mock<IProjectBaseDieCountWithLevelRepository> _projectBaseDieCountWithLevelRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _projectBaseDieCountWithLevelRepository = new Mock<IProjectBaseDieCountWithLevelRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ProjectBaseDieCountWithLevel_GetQuery_Success()
        {
            //Arrange
            var query = new GetProjectBaseDieCountWithLevelQuery();

            _projectBaseDieCountWithLevelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new DieCountWithLevel()
//propertyler buraya yazılacak
//{																		
//ProjectBaseDieCountWithLevelId = 1,
//ProjectBaseDieCountWithLevelName = "Test"
//}
);

            var handler = new GetProjectBaseDieCountWithLevelQueryHandler(_projectBaseDieCountWithLevelRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ProjectBaseDieCountWithLevelId.Should().Be(1);

        }

        [Test]
        public async Task ProjectBaseDieCountWithLevel_GetQueries_Success()
        {
            //Arrange
            var query = new GetProjectBaseDieCountWithLevelsQuery();

            _projectBaseDieCountWithLevelRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<DieCountWithLevel, bool>>>()))
                        .ReturnsAsync(new List<DieCountWithLevel> { new DieCountWithLevel() { /*TODO:propertyler buraya yazılacak ProjectBaseDieCountWithLevelId = 1, ProjectBaseDieCountWithLevelName = "test"*/ } }.AsQueryable());

            var handler = new GetProjectBaseDieCountWithLevelsQueryHandler(_projectBaseDieCountWithLevelRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<DieCountWithLevel>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task ProjectBaseDieCountWithLevel_CreateCommand_Success()
        {
            DieCountWithLevel rt = null;
            //Arrange
            var command = new CreateProjectBaseDieCountWithLevelCommand();
            //propertyler buraya yazılacak
            //command.ProjectBaseDieCountWithLevelName = "deneme";

            _projectBaseDieCountWithLevelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _projectBaseDieCountWithLevelRepository.Setup(x => x.Add(It.IsAny<DieCountWithLevel>()));

            var handler = new CreateProjectBaseDieCountWithLevelCommandHandler(_projectBaseDieCountWithLevelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ProjectBaseDieCountWithLevel_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateProjectBaseDieCountWithLevelCommand();
            //propertyler buraya yazılacak 
            //command.ProjectBaseDieCountWithLevelName = "test";

            _projectBaseDieCountWithLevelRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<DieCountWithLevel, bool>>>()))
                                           .ReturnsAsync(new List<DieCountWithLevel> { new DieCountWithLevel() { /*TODO:propertyler buraya yazılacak ProjectBaseDieCountWithLevelId = 1, ProjectBaseDieCountWithLevelName = "test"*/ } }.AsQueryable());

            _projectBaseDieCountWithLevelRepository.Setup(x => x.Add(It.IsAny<DieCountWithLevel>()));

            var handler = new CreateProjectBaseDieCountWithLevelCommandHandler(_projectBaseDieCountWithLevelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ProjectBaseDieCountWithLevel_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateProjectBaseDieCountWithLevelCommand();
            //command.ProjectBaseDieCountWithLevelName = "test";

            _projectBaseDieCountWithLevelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new DieCountWithLevel() { /*TODO:propertyler buraya yazılacak ProjectBaseDieCountWithLevelId = 1, ProjectBaseDieCountWithLevelName = "deneme"*/ });

            _projectBaseDieCountWithLevelRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<DieCountWithLevel>()));

            var handler = new UpdateProjectBaseDieCountWithLevelCommandHandler(_projectBaseDieCountWithLevelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ProjectBaseDieCountWithLevel_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteProjectBaseDieCountWithLevelCommand();

            _projectBaseDieCountWithLevelRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new DieCountWithLevel() { /*TODO:propertyler buraya yazılacak ProjectBaseDieCountWithLevelId = 1, ProjectBaseDieCountWithLevelName = "deneme"*/});

            _projectBaseDieCountWithLevelRepository.Setup(x => x.Delete(It.IsAny<DieCountWithLevel>()));

            var handler = new DeleteProjectBaseDieCountWithLevelCommandHandler(_projectBaseDieCountWithLevelRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

