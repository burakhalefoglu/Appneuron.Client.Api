
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Entities.Concrete;
using static Business.Internals.Handlers.Clients.CreateClientInternalCommand;
using Business.Constants;
using static Business.Internals.Handlers.Clients.GetClientByProjectIdInternalQuery;
using MediatR;
using Business.Internals.Handlers.Clients;
using FluentAssertions;
using MongoDB.Bson;

namespace Tests.Business.HandlersTest
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
            query.ClientId = "asdas";
            query.ProjectId = "asfdzs";

            _clientRepository.Setup(x => x.GetByFilterAsync(
                    It.IsAny<Expression<Func<ClientDataModel, bool>>>()))
                .ReturnsAsync(new ClientDataModel()
                {
                    Id = new ObjectId(),
                    ProjectId = "asfdzs",
                    ClientId = "asdas",
                    CreatedAt = new DateTime(),
                    IsPaidClient = 0,
                    PaidTime = DateTime.Now
                });

            var handler = new GetClientByProjectIdInternalQueryHandler(_clientRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            x.Data.ProjectId.Should().Be("asfdzs");

        }


        [Test]
        public async Task Client_CreateCommand_Success()
        {
            var command = new CreateClientInternalCommand();
            command.ClientId = "sdfdsf";
            command.ProjectKey = "sfd";
            command.CreatedAt = new DateTime();
            command.IsPaidClient = 0;

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
            command.ClientId = "sdfdsf";
            command.ProjectKey = "sfd";
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

