
using Business.Handlers.ChurnClientPredictionResults.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ChurnClientPredictionResults.Queries.GetChurnClientPredictionResultQuery;
using Entities.Concrete;
using static Business.Handlers.ChurnClientPredictionResults.Queries.GetChurnClientPredictionResultsQuery;
using static Business.Handlers.ChurnClientPredictionResults.Commands.CreateChurnClientPredictionResultCommand;
using Business.Handlers.ChurnClientPredictionResults.Commands;
using Business.Constants;
using static Business.Handlers.ChurnClientPredictionResults.Commands.UpdateChurnClientPredictionResultCommand;
using static Business.Handlers.ChurnClientPredictionResults.Commands.DeleteChurnClientPredictionResultCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ChurnClientPredictionResultHandlerTests
    {
        Mock<IChurnClientPredictionResultRepository> _churnClientPredictionResultRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _churnClientPredictionResultRepository = new Mock<IChurnClientPredictionResultRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ChurnClientPredictionResult_GetQuery_Success()
        {
            //Arrange
            var query = new GetChurnClientPredictionResultQuery();

            _churnClientPredictionResultRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new ChurnClientPredictionResult()
//propertyler buraya yazılacak
//{																		
//ChurnClientPredictionResultId = 1,
//ChurnClientPredictionResultName = "Test"
//}
);

            var handler = new GetChurnClientPredictionResultQueryHandler(_churnClientPredictionResultRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ChurnClientPredictionResultId.Should().Be(1);

        }

        [Test]
        public async Task ChurnClientPredictionResult_GetQueries_Success()
        {
            //Arrange
            var query = new GetChurnClientPredictionResultsQuery();

            _churnClientPredictionResultRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ChurnClientPredictionResult, bool>>>()))
                        .ReturnsAsync(new List<ChurnClientPredictionResult> { new ChurnClientPredictionResult() { /*TODO:propertyler buraya yazılacak ChurnClientPredictionResultId = 1, ChurnClientPredictionResultName = "test"*/ } }.AsQueryable());

            var handler = new GetChurnClientPredictionResultsQueryHandler(_churnClientPredictionResultRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ChurnClientPredictionResult>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task ChurnClientPredictionResult_CreateCommand_Success()
        {
            ChurnClientPredictionResult rt = null;
            //Arrange
            var command = new CreateChurnClientPredictionResultCommand();
            //propertyler buraya yazılacak
            //command.ChurnClientPredictionResultName = "deneme";

            _churnClientPredictionResultRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _churnClientPredictionResultRepository.Setup(x => x.Add(It.IsAny<ChurnClientPredictionResult>()));

            var handler = new CreateChurnClientPredictionResultCommandHandler(_churnClientPredictionResultRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ChurnClientPredictionResult_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateChurnClientPredictionResultCommand();
            //propertyler buraya yazılacak 
            //command.ChurnClientPredictionResultName = "test";

            _churnClientPredictionResultRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ChurnClientPredictionResult, bool>>>()))
                                           .ReturnsAsync(new List<ChurnClientPredictionResult> { new ChurnClientPredictionResult() { /*TODO:propertyler buraya yazılacak ChurnClientPredictionResultId = 1, ChurnClientPredictionResultName = "test"*/ } }.AsQueryable());

            _churnClientPredictionResultRepository.Setup(x => x.Add(It.IsAny<ChurnClientPredictionResult>()));

            var handler = new CreateChurnClientPredictionResultCommandHandler(_churnClientPredictionResultRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ChurnClientPredictionResult_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateChurnClientPredictionResultCommand();
            //command.ChurnClientPredictionResultName = "test";

            _churnClientPredictionResultRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new ChurnClientPredictionResult() { /*TODO:propertyler buraya yazılacak ChurnClientPredictionResultId = 1, ChurnClientPredictionResultName = "deneme"*/ });

            _churnClientPredictionResultRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<ChurnClientPredictionResult>()));

            var handler = new UpdateChurnClientPredictionResultCommandHandler(_churnClientPredictionResultRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ChurnClientPredictionResult_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteChurnClientPredictionResultCommand();

            _churnClientPredictionResultRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new ChurnClientPredictionResult() { /*TODO:propertyler buraya yazılacak ChurnClientPredictionResultId = 1, ChurnClientPredictionResultName = "deneme"*/});

            _churnClientPredictionResultRepository.Setup(x => x.Delete(It.IsAny<ChurnClientPredictionResult>()));

            var handler = new DeleteChurnClientPredictionResultCommandHandler(_churnClientPredictionResultRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

