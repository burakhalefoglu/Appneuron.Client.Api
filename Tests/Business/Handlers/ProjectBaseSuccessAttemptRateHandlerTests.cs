
using Business.Handlers.ProjectBaseSuccessAttemptRates.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ProjectBaseSuccessAttemptRates.Queries.GetProjectBaseSuccessAttemptRateQuery;
using Entities.Concrete;
using static Business.Handlers.ProjectBaseSuccessAttemptRates.Queries.GetProjectBaseSuccessAttemptRatesQuery;
using static Business.Handlers.ProjectBaseSuccessAttemptRates.Commands.CreateProjectBaseSuccessAttemptRateCommand;
using Business.Handlers.ProjectBaseSuccessAttemptRates.Commands;
using Business.Constants;
using static Business.Handlers.ProjectBaseSuccessAttemptRates.Commands.UpdateProjectBaseSuccessAttemptRateCommand;
using static Business.Handlers.ProjectBaseSuccessAttemptRates.Commands.DeleteProjectBaseSuccessAttemptRateCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ProjectBaseSuccessAttemptRateHandlerTests
    {
        Mock<IProjectBaseSuccessAttemptRateRepository> _projectBaseSuccessAttemptRateRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _projectBaseSuccessAttemptRateRepository = new Mock<IProjectBaseSuccessAttemptRateRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ProjectBaseSuccessAttemptRate_GetQuery_Success()
        {
            //Arrange
            var query = new GetProjectBaseSuccessAttemptRateQuery();

            _projectBaseSuccessAttemptRateRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new ProjectBaseSuccessAttemptRate()
//propertyler buraya yazılacak
//{																		
//ProjectBaseSuccessAttemptRateId = 1,
//ProjectBaseSuccessAttemptRateName = "Test"
//}
);

            var handler = new GetProjectBaseSuccessAttemptRateQueryHandler(_projectBaseSuccessAttemptRateRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ProjectBaseSuccessAttemptRateId.Should().Be(1);

        }

        [Test]
        public async Task ProjectBaseSuccessAttemptRate_GetQueries_Success()
        {
            //Arrange
            var query = new GetProjectBaseSuccessAttemptRatesQuery();

            _projectBaseSuccessAttemptRateRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ProjectBaseSuccessAttemptRate, bool>>>()))
                        .ReturnsAsync(new List<ProjectBaseSuccessAttemptRate> { new ProjectBaseSuccessAttemptRate() { /*TODO:propertyler buraya yazılacak ProjectBaseSuccessAttemptRateId = 1, ProjectBaseSuccessAttemptRateName = "test"*/ } }.AsQueryable());

            var handler = new GetProjectBaseSuccessAttemptRatesQueryHandler(_projectBaseSuccessAttemptRateRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ProjectBaseSuccessAttemptRate>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task ProjectBaseSuccessAttemptRate_CreateCommand_Success()
        {
            ProjectBaseSuccessAttemptRate rt = null;
            //Arrange
            var command = new CreateProjectBaseSuccessAttemptRateCommand();
            //propertyler buraya yazılacak
            //command.ProjectBaseSuccessAttemptRateName = "deneme";

            _projectBaseSuccessAttemptRateRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _projectBaseSuccessAttemptRateRepository.Setup(x => x.Add(It.IsAny<ProjectBaseSuccessAttemptRate>()));

            var handler = new CreateProjectBaseSuccessAttemptRateCommandHandler(_projectBaseSuccessAttemptRateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ProjectBaseSuccessAttemptRate_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateProjectBaseSuccessAttemptRateCommand();
            //propertyler buraya yazılacak 
            //command.ProjectBaseSuccessAttemptRateName = "test";

            _projectBaseSuccessAttemptRateRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ProjectBaseSuccessAttemptRate, bool>>>()))
                                           .ReturnsAsync(new List<ProjectBaseSuccessAttemptRate> { new ProjectBaseSuccessAttemptRate() { /*TODO:propertyler buraya yazılacak ProjectBaseSuccessAttemptRateId = 1, ProjectBaseSuccessAttemptRateName = "test"*/ } }.AsQueryable());

            _projectBaseSuccessAttemptRateRepository.Setup(x => x.Add(It.IsAny<ProjectBaseSuccessAttemptRate>()));

            var handler = new CreateProjectBaseSuccessAttemptRateCommandHandler(_projectBaseSuccessAttemptRateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ProjectBaseSuccessAttemptRate_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateProjectBaseSuccessAttemptRateCommand();
            //command.ProjectBaseSuccessAttemptRateName = "test";

            _projectBaseSuccessAttemptRateRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new ProjectBaseSuccessAttemptRate() { /*TODO:propertyler buraya yazılacak ProjectBaseSuccessAttemptRateId = 1, ProjectBaseSuccessAttemptRateName = "deneme"*/ });

            _projectBaseSuccessAttemptRateRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<ProjectBaseSuccessAttemptRate>()));

            var handler = new UpdateProjectBaseSuccessAttemptRateCommandHandler(_projectBaseSuccessAttemptRateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ProjectBaseSuccessAttemptRate_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteProjectBaseSuccessAttemptRateCommand();

            _projectBaseSuccessAttemptRateRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new ProjectBaseSuccessAttemptRate() { /*TODO:propertyler buraya yazılacak ProjectBaseSuccessAttemptRateId = 1, ProjectBaseSuccessAttemptRateName = "deneme"*/});

            _projectBaseSuccessAttemptRateRepository.Setup(x => x.Delete(It.IsAny<ProjectBaseSuccessAttemptRate>()));

            var handler = new DeleteProjectBaseSuccessAttemptRateCommandHandler(_projectBaseSuccessAttemptRateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

