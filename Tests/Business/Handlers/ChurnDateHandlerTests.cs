
using Business.Handlers.ChurnDates.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ChurnDates.Queries.GetChurnDateQuery;
using Entities.Concrete;
using static Business.Handlers.ChurnDates.Queries.GetChurnDatesQuery;
using static Business.Handlers.ChurnDates.Commands.CreateChurnDateCommand;
using Business.Handlers.ChurnDates.Commands;
using Business.Constants;
using static Business.Handlers.ChurnDates.Commands.UpdateChurnDateCommand;
using static Business.Handlers.ChurnDates.Commands.DeleteChurnDateCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ChurnDateHandlerTests
    {
        Mock<IChurnDateRepository> _churnDateRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _churnDateRepository = new Mock<IChurnDateRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ChurnDate_GetQuery_Success()
        {
            //Arrange
            var query = new GetChurnDateQuery();

            _churnDateRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new ChurnDate()
//propertyler buraya yazılacak
//{																		
//ChurnDateId = 1,
//ChurnDateName = "Test"
//}
);

            var handler = new GetChurnDateQueryHandler(_churnDateRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ChurnDateId.Should().Be(1);

        }

        [Test]
        public async Task ChurnDate_GetQueries_Success()
        {
            //Arrange
            var query = new GetChurnDatesQuery();

            _churnDateRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ChurnDate, bool>>>()))
                        .ReturnsAsync(new List<ChurnDate> { new ChurnDate() { /*TODO:propertyler buraya yazılacak ChurnDateId = 1, ChurnDateName = "test"*/ } }.AsQueryable());

            var handler = new GetChurnDatesQueryHandler(_churnDateRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ChurnDate>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task ChurnDate_CreateCommand_Success()
        {
            ChurnDate rt = null;
            //Arrange
            var command = new CreateChurnDateCommand();
            //propertyler buraya yazılacak
            //command.ChurnDateName = "deneme";

            _churnDateRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _churnDateRepository.Setup(x => x.Add(It.IsAny<ChurnDate>()));

            var handler = new CreateChurnDateCommandHandler(_churnDateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ChurnDate_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateChurnDateCommand();
            //propertyler buraya yazılacak 
            //command.ChurnDateName = "test";

            _churnDateRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ChurnDate, bool>>>()))
                                           .ReturnsAsync(new List<ChurnDate> { new ChurnDate() { /*TODO:propertyler buraya yazılacak ChurnDateId = 1, ChurnDateName = "test"*/ } }.AsQueryable());

            _churnDateRepository.Setup(x => x.Add(It.IsAny<ChurnDate>()));

            var handler = new CreateChurnDateCommandHandler(_churnDateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ChurnDate_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateChurnDateCommand();
            //command.ChurnDateName = "test";

            _churnDateRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new ChurnDate() { /*TODO:propertyler buraya yazılacak ChurnDateId = 1, ChurnDateName = "deneme"*/ });

            _churnDateRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<ChurnDate>()));

            var handler = new UpdateChurnDateCommandHandler(_churnDateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ChurnDate_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteChurnDateCommand();

            _churnDateRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new ChurnDate() { /*TODO:propertyler buraya yazılacak ChurnDateId = 1, ChurnDateName = "deneme"*/});

            _churnDateRepository.Setup(x => x.Delete(It.IsAny<ChurnDate>()));

            var handler = new DeleteChurnDateCommandHandler(_churnDateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

