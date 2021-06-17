
using Business.Handlers.ChallengeBasedSegmentations.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ChallengeBasedSegmentations.Queries.GetChallengeBasedSegmentationQuery;
using Entities.Concrete;
using static Business.Handlers.ChallengeBasedSegmentations.Queries.GetChallengeBasedSegmentationsQuery;
using static Business.Handlers.ChallengeBasedSegmentations.Commands.CreateChallengeBasedSegmentationCommand;
using Business.Handlers.ChallengeBasedSegmentations.Commands;
using Business.Constants;
using static Business.Handlers.ChallengeBasedSegmentations.Commands.UpdateChallengeBasedSegmentationCommand;
using static Business.Handlers.ChallengeBasedSegmentations.Commands.DeleteChallengeBasedSegmentationCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ChallengeBasedSegmentationHandlerTests
    {
        Mock<IChallengeBasedSegmentationRepository> _challengeBasedSegmentationRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _challengeBasedSegmentationRepository = new Mock<IChallengeBasedSegmentationRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ChallengeBasedSegmentation_GetQuery_Success()
        {
            //Arrange
            var query = new GetChallengeBasedSegmentationQuery();

            _challengeBasedSegmentationRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new ProjectAndChallengeBasedSegmentation()
//propertyler buraya yazılacak
//{																		
//ChallengeBasedSegmentationId = 1,
//ChallengeBasedSegmentationName = "Test"
//}
);

            var handler = new GetChallengeBasedSegmentationQueryHandler(_challengeBasedSegmentationRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ChallengeBasedSegmentationId.Should().Be(1);

        }

        [Test]
        public async Task ChallengeBasedSegmentation_GetQueries_Success()
        {
            //Arrange
            var query = new GetChallengeBasedSegmentationsQuery();

            _challengeBasedSegmentationRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ProjectAndChallengeBasedSegmentation, bool>>>()))
                        .ReturnsAsync(new List<ProjectAndChallengeBasedSegmentation> { new ProjectAndChallengeBasedSegmentation() { /*TODO:propertyler buraya yazılacak ChallengeBasedSegmentationId = 1, ChallengeBasedSegmentationName = "test"*/ } }.AsQueryable());

            var handler = new GetChallengeBasedSegmentationsQueryHandler(_challengeBasedSegmentationRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ProjectAndChallengeBasedSegmentation>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task ChallengeBasedSegmentation_CreateCommand_Success()
        {
            ProjectAndChallengeBasedSegmentation rt = null;
            //Arrange
            var command = new CreateChallengeBasedSegmentationCommand();
            //propertyler buraya yazılacak
            //command.ChallengeBasedSegmentationName = "deneme";

            _challengeBasedSegmentationRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _challengeBasedSegmentationRepository.Setup(x => x.Add(It.IsAny<ProjectAndChallengeBasedSegmentation>()));

            var handler = new CreateChallengeBasedSegmentationCommandHandler(_challengeBasedSegmentationRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ChallengeBasedSegmentation_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateChallengeBasedSegmentationCommand();
            //propertyler buraya yazılacak 
            //command.ChallengeBasedSegmentationName = "test";

            _challengeBasedSegmentationRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ProjectAndChallengeBasedSegmentation, bool>>>()))
                                           .ReturnsAsync(new List<ProjectAndChallengeBasedSegmentation> { new ProjectAndChallengeBasedSegmentation() { /*TODO:propertyler buraya yazılacak ChallengeBasedSegmentationId = 1, ChallengeBasedSegmentationName = "test"*/ } }.AsQueryable());

            _challengeBasedSegmentationRepository.Setup(x => x.Add(It.IsAny<ProjectAndChallengeBasedSegmentation>()));

            var handler = new CreateChallengeBasedSegmentationCommandHandler(_challengeBasedSegmentationRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ChallengeBasedSegmentation_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateChallengeBasedSegmentationCommand();
            //command.ChallengeBasedSegmentationName = "test";

            _challengeBasedSegmentationRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new ProjectAndChallengeBasedSegmentation() { /*TODO:propertyler buraya yazılacak ChallengeBasedSegmentationId = 1, ChallengeBasedSegmentationName = "deneme"*/ });

            _challengeBasedSegmentationRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<ProjectAndChallengeBasedSegmentation>()));

            var handler = new UpdateChallengeBasedSegmentationCommandHandler(_challengeBasedSegmentationRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ChallengeBasedSegmentation_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteChallengeBasedSegmentationCommand();

            _challengeBasedSegmentationRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new ProjectAndChallengeBasedSegmentation() { /*TODO:propertyler buraya yazılacak ChallengeBasedSegmentationId = 1, ChallengeBasedSegmentationName = "deneme"*/});

            _challengeBasedSegmentationRepository.Setup(x => x.Delete(It.IsAny<ProjectAndChallengeBasedSegmentation>()));

            var handler = new DeleteChallengeBasedSegmentationCommandHandler(_challengeBasedSegmentationRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

