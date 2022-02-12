using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Business.Constants;
using Business.Handlers.ChurnDates.Commands;
using Business.Handlers.ChurnDates.Queries;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using static Business.Handlers.ChurnDates.Queries.GetChurnDateByProjectIdQuery;
using static Business.Handlers.ChurnDates.Commands.CreateChurnDateCommand;
using static Business.Handlers.ChurnDates.Commands.UpdateChurnDateCommand;

namespace Tests.Business.Handlers
{
    [TestFixture]
    public class ChurnDateHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _churnDateRepository = new Mock<IChurnDateRepository>();
            _mediator = new Mock<IMediator>();
        }

        private Mock<IChurnDateRepository> _churnDateRepository;
        private Mock<IMediator> _mediator;

        [Test]
        public async Task ChurnDate_GetQuery_Success()
        {
            //Arrange
            var query = new GetChurnDateByProjectIdQuery();

            _churnDateRepository.Setup(x =>
                    x.GetAsync(It.IsAny<Expression<Func<ChurnDate, bool>>>()))
                .ReturnsAsync(new ChurnDate
                    {
                        Id = 1,
                        ChurnDateMinutes = new DateTime().Ticks,
                        DateTypeOnGui = "Hour",
                        ProjectId = 12
                    }
                );

            var handler = new GetChurnDateByProjectIdQueryHandler(_churnDateRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            x.Data.ProjectId.Should().Be(12);
        }

        [Test]
        public async Task ChurnDate_CreateCommand_Success()
        {
            //Arrange
            var command = new CreateChurnDateCommand
            {
                ProjectId = 12,
                DateTypeOnGui = "Day",
                ChurnDateMinutes = new DateTime().Ticks
            };

            _churnDateRepository.Setup(x =>
                    x.Any(It.IsAny<Expression<Func<ChurnDate, bool>>>()))
                .Returns(false);

            _churnDateRepository.Setup(x => x.Add(It.IsAny<ChurnDate>()));

            var handler = new CreateChurnDateCommandHandler(_churnDateRepository.Object);
            var x = await handler.Handle(command, new CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ChurnDate_CreateCommand_NameAlreadyExist()
        {
            var command = new CreateChurnDateCommand
            {
                ProjectId = 12,
                DateTypeOnGui = "Day",
                ChurnDateMinutes = new DateTime().Ticks
            };

            _churnDateRepository.Setup(x =>
                    x.AnyAsync(It.IsAny<Expression<Func<ChurnDate, bool>>>()))
                .ReturnsAsync(true);

            _churnDateRepository.Setup(x => x.Add(It.IsAny<ChurnDate>()));

            var handler = new CreateChurnDateCommandHandler(_churnDateRepository.Object);
            var x = await handler.Handle(command, new CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ChurnDate_UpdateCommand_UpdatedSuccess()
        {
            //Arrange
            var command = new UpdateChurnDateCommand
            {
                ProjectId = 12
            };

            _churnDateRepository.Setup(x => x.GetAsync(
                    It.IsAny<Expression<Func<ChurnDate, bool>>>()))
                .ReturnsAsync(new ChurnDate());

            _churnDateRepository.Setup(x => x.UpdateAsync(It.IsAny<ChurnDate>()));

            var handler = new UpdateChurnDateCommandHandler(_churnDateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ChurnDate_UpdateCommand_AddedSuccess()
        {
            //Arrange
            var command = new UpdateChurnDateCommand();
            //command.ChurnDateName = "test";

            _churnDateRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ChurnDate, bool>>>()))
                .ReturnsAsync((ChurnDate) null);

            _churnDateRepository.Setup(x => x.UpdateAsync(It.IsAny<ChurnDate>()));

            var handler = new UpdateChurnDateCommandHandler(_churnDateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }
    }
}