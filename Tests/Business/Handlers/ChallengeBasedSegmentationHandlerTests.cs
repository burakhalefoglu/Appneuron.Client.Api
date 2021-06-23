﻿using Business.Constants;
using Business.Handlers.ChallengeBasedSegmentations.Commands;
using Business.Handlers.ChallengeBasedSegmentations.Queries;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.ChartModels;
using FluentAssertions;
using MediatR;
using MongoDB.Bson;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ChallengeBasedSegmentations.Commands.CreateChallengeBasedSegmentationCommand;
using static Business.Handlers.ChallengeBasedSegmentations.Commands.DeleteChallengeBasedSegmentationCommand;
using static Business.Handlers.ChallengeBasedSegmentations.Commands.UpdateChallengeBasedSegmentationCommand;
using static Business.Handlers.ChallengeBasedSegmentations.Queries.GetChallengeBasedSegmentationQuery;
using static Business.Handlers.ChallengeBasedSegmentations.Queries.GetChallengeBasedSegmentationsQuery;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ChallengeBasedSegmentationHandlerTests
    {
        private Mock<IChallengeBasedSegmentationRepository> _challengeBasedSegmentationRepository;
        private Mock<IMediator> _mediator;

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

            _challengeBasedSegmentationRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new ChallengeBasedSegmentation()
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

            _challengeBasedSegmentationRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ChallengeBasedSegmentation, bool>>>()))
                        .ReturnsAsync(new List<ChallengeBasedSegmentation> { new ChallengeBasedSegmentation() { /*TODO:propertyler buraya yazılacak ChallengeBasedSegmentationId = 1, ChallengeBasedSegmentationName = "test"*/ } }.AsQueryable());

            var handler = new GetChallengeBasedSegmentationsQueryHandler(_challengeBasedSegmentationRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ChallengeBasedSegmentation>)x.Data).Count.Should().BeGreaterThan(1);
        }

        [Test]
        public async Task ChallengeBasedSegmentation_CreateCommand_Success()
        {
            ChallengeBasedSegmentation rt = null;
            //Arrange
            var command = new CreateChallengeBasedSegmentationCommand();
            //propertyler buraya yazılacak
            //command.ChallengeBasedSegmentationName = "deneme";

            _challengeBasedSegmentationRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _challengeBasedSegmentationRepository.Setup(x => x.Add(It.IsAny<ChallengeBasedSegmentation>()));

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

            _challengeBasedSegmentationRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ChallengeBasedSegmentation, bool>>>()))
                                           .ReturnsAsync(new List<ChallengeBasedSegmentation> { new ChallengeBasedSegmentation() { /*TODO:propertyler buraya yazılacak ChallengeBasedSegmentationId = 1, ChallengeBasedSegmentationName = "test"*/ } }.AsQueryable());

            _challengeBasedSegmentationRepository.Setup(x => x.Add(It.IsAny<ChallengeBasedSegmentation>()));

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
                        .ReturnsAsync(new ChallengeBasedSegmentation() { /*TODO:propertyler buraya yazılacak ChallengeBasedSegmentationId = 1, ChallengeBasedSegmentationName = "deneme"*/ });

            _challengeBasedSegmentationRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<ChallengeBasedSegmentation>()));

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
                        .ReturnsAsync(new ChallengeBasedSegmentation() { /*TODO:propertyler buraya yazılacak ChallengeBasedSegmentationId = 1, ChallengeBasedSegmentationName = "deneme"*/});

            _challengeBasedSegmentationRepository.Setup(x => x.Delete(It.IsAny<ChallengeBasedSegmentation>()));

            var handler = new DeleteChallengeBasedSegmentationCommandHandler(_challengeBasedSegmentationRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}