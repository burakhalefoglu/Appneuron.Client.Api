using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Business.Constants;
using Business.Internals.Handlers.Clients;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using static Business.Internals.Handlers.Clients.CreateClientInternalCommand;
using static Business.Internals.Handlers.Clients.GetClientByProjectIdInternalQuery;

namespace Tests.Business.Handlers
{
    [TestFixture]
    public class ClientHandlerTests
    {
        Mock<IClientRepository> _clientRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _clientRepository = new Mock<IClientRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Client_GetQueriesByProjectId_Success()
        {
            //Arrange
            var query = new GetClientByProjectIdInternalQuery();
            query.ClientId = 1;
            query.ProjectId = 21;

            _clientRepository.Setup(x => x.GetAsync(
                    It.IsAny<Expression<Func<ClientDataModel, bool>>>()))
                .ReturnsAsync(new ClientDataModel()
                {
                    Id = 1,
                    ProjectId = 21,
                    CreatedAt = new DateTime(),
                    IsPaidClient = 0,
                    PaidTime = DateTime.Now
                });

            var handler = new GetClientByProjectIdInternalQueryHandler(_clientRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            x.Data.ProjectId.Should().Be(21);

        }


        [Test]
        public async Task Client_CreateCommand_Success()
        {
            var command = new CreateClientInternalCommand
            {
                ClientId = 1,
                ProjectId = 21,
                CreatedAt = new DateTime(),
                IsPaidClient = 0
            };

            _clientRepository.Setup(x => x
                    .Any(It.IsAny<Expression<Func<ClientDataModel, bool>>>()))
                .Returns(false);

            var handler = new CreateClientInternalCommandHandler(_clientRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Client_CreateCommand_NameAlreadyExist()
        {
            var command = new CreateClientInternalCommand();
            command.ClientId = 1;
            command.ProjectId = 21;
            command.CreatedAt = new DateTime();
            command.IsPaidClient = 0;

            _clientRepository.Setup(x => x
                    .Any(It.IsAny<Expression<Func<ClientDataModel, bool>>>()))
                    .Returns(true);

            var handler = new CreateClientInternalCommandHandler(_clientRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }
    }
}

