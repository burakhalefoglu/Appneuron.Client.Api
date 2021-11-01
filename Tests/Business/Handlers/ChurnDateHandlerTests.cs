
using Business.Handlers.ChurnDates.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ChurnDates.Queries.GetChurnDateByProjectIdQuery;
using Entities.Concrete;
using static Business.Handlers.ChurnDates.Commands.CreateChurnDateCommand;
using Business.Handlers.ChurnDates.Commands;
using Business.Constants;
using static Business.Handlers.ChurnDates.Commands.UpdateChurnDateCommand;
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
            var query = new GetChurnDateByProjectIdQuery();

            _churnDateRepository.Setup(x =>
                x.GetByFilterAsync(It.IsAny<Expression<Func<ChurnDate, bool>>>()))
                .ReturnsAsync(new ChurnDate()
                {
                    Id = new ObjectId(),
                    ChurnDateMinutes = new DateTime().Ticks,
                    DateTypeOnGui = "Hour",
                    ProjectId = "sdfsadfa"

                }
                );

            var handler = new GetChurnDateByProjectIdQueryHandler(_churnDateRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            x.Data.ProjectId.Should().Be("sdfsadfa");

        }

        [Test]
        public async Task ChurnDate_CreateCommand_Success()
        {

            //Arrange
            var command = new CreateChurnDateCommand
            {
                ProjectId = "asdas",
                DateTypeOnGui = "Day",
                churnDateMinutes = new DateTime().Ticks
            };

            _churnDateRepository.Setup(x => 
                    x.Any(It.IsAny<Expression<Func<ChurnDate, bool>>>()))
                    .Returns(false);

            _churnDateRepository.Setup(x => x.Add(It.IsAny<ChurnDate>()));

            var handler = new CreateChurnDateCommandHandler(_churnDateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ChurnDate_CreateCommand_NameAlreadyExist()
        {
            var command = new CreateChurnDateCommand
            {
                ProjectId = "asdas",
                DateTypeOnGui = "Day",
                churnDateMinutes = new DateTime().Ticks
            };

            _churnDateRepository.Setup(x =>
                    x.Any(It.IsAny<Expression<Func<ChurnDate, bool>>>()))
                .Returns(true);

            _churnDateRepository.Setup(x => x.Add(It.IsAny<ChurnDate>()));

            var handler = new CreateChurnDateCommandHandler(_churnDateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ChurnDate_UpdateCommand_UpdatedSuccess()
        {
            //Arrange
            var command = new UpdateChurnDateCommand();
            command.ProjectId = "qwdsada";

            _churnDateRepository.Setup(x => x.GetByFilterAsync(
                    It.IsAny<Expression<Func<ChurnDate, bool>>>()))
                        .ReturnsAsync(new ChurnDate()
                        {
                        });

            _churnDateRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<ChurnDate>()));

            var handler = new UpdateChurnDateCommandHandler(_churnDateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ChurnDate_UpdateCommand_AddedSuccess()
        {
            //Arrange
            var command = new UpdateChurnDateCommand();
            //command.ChurnDateName = "test";

            _churnDateRepository.Setup(x => x.GetByFilterAsync(It.IsAny<Expression<Func<ChurnDate, bool>>>()))
                .ReturnsAsync(new ChurnDate()
                {
                    Id = new ObjectId(),
                    ProjectId = "sadfa",
                    ChurnDateMinutes = new DateTime().Ticks,
                    DateTypeOnGui = "Day"
                });

            _churnDateRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<ChurnDate>()));

            var handler = new UpdateChurnDateCommandHandler(_churnDateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }
    }
}

