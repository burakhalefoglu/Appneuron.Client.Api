using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Clients.Queries;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using static Business.Handlers.Clients.Queries.GetPaidClientCountByProjectIdQuery;
using static Business.Handlers.Clients.Queries.GetPaidClientLastSevenDayCountQuery;
using static Business.Handlers.Clients.Queries.GetTotalClientCountByProjectIdQuery;
using static Business.Handlers.Clients.Queries.GetTotalClientLastSevenDayCountQuery;

namespace Tests.Business.Handlers;

[TestFixture]
public class ClientHandlerTests
{
    [SetUp]
    public void Setup()
    {
        _clientRepository = new Mock<IClientRepository>();
        _getPaidClientCountByProjectIdQueryHandler =
            new GetPaidClientCountByProjectIdQueryHandler(_clientRepository.Object);
        _getPaidClientLastSevenDayCountQueryHandler =
            new GetPaidClientLastSevenDayCountQueryHandler(_clientRepository.Object);
        _getTotalClientCountByProjectIdQueryHandler =
            new GetTotalClientCountByProjectIdQueryHandler(_clientRepository.Object);
        _getTotalClientLastSevenDayCountQueryHandler =
            new GetTotalClientLastSevenDayCountQueryHandler(_clientRepository.Object);
    }

    private Mock<IClientRepository> _clientRepository;
    private GetPaidClientCountByProjectIdQueryHandler _getPaidClientCountByProjectIdQueryHandler;
    private GetPaidClientLastSevenDayCountQueryHandler _getPaidClientLastSevenDayCountQueryHandler;
    private GetTotalClientCountByProjectIdQueryHandler _getTotalClientCountByProjectIdQueryHandler;
    private GetTotalClientLastSevenDayCountQueryHandler _getTotalClientLastSevenDayCountQueryHandler;

    [Test]
    public async Task PaidClientCount_GetQuery_Success()
    {
        var query = new GetPaidClientCountByProjectIdQuery
        {
            ProjectId = 1
        };

        _clientRepository.Setup(x => x.GetListAsync(
                It.IsAny<Expression<Func<ClientDataModel, bool>>>()))
            .ReturnsAsync(new List<ClientDataModel>
            {
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-1),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 1
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-1),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 0
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-2),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 1
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-2),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 1
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-3),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 0
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-4),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 1
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-5),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 1
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-6),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 0
                }
            }.AsQueryable());

        var result = await _getPaidClientCountByProjectIdQueryHandler.Handle(query, new CancellationToken());
        result.Success.Should().BeTrue();
        result.Data.Should().Be(5);
    }

    [Test]
    public async Task TotalClientCount_GetQuery_Success()
    {
        var query = new GetTotalClientCountByProjectIdQuery
        {
            ProjectId = 1
        };

        _clientRepository.Setup(x => x.GetListAsync(
                It.IsAny<Expression<Func<ClientDataModel, bool>>>()))
            .ReturnsAsync(new List<ClientDataModel>
            {
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-1),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 1
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-1),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 0
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-2),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 1
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-2),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 1
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-3),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 0
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-4),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 1
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-5),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 1
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-6),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 0
                }
            }.AsQueryable());

        var result = await _getTotalClientCountByProjectIdQueryHandler.Handle(query, new CancellationToken());
        result.Success.Should().BeTrue();
        result.Data.Should().Be(8);
    }

    [Test]
    public async Task PaidClientLastSevenDayCount_GetQuery_Success()
    {
        var query = new GetPaidClientLastSevenDayCountQuery
        {
            ProjectId = 1
        };

        _clientRepository.Setup(x => x.GetListAsync(
                It.IsAny<Expression<Func<ClientDataModel, bool>>>()))
            .ReturnsAsync(new List<ClientDataModel>
            {
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(0).AddHours(-10),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 1
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(0).AddHours(-10),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 1
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(0).AddHours(-10),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 1
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-1).AddHours(-10),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 0
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-2).AddHours(-10),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 1
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-2).AddHours(-10),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 1
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-3).AddHours(-10),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 0
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-4).AddHours(-10),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 1
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-5).AddHours(-10),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 1
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-6).AddHours(-10),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 0
                }
            }.AsQueryable());

        var result = await _getPaidClientLastSevenDayCountQueryHandler.Handle(query, new CancellationToken());
        result.Success.Should().BeTrue();
        result.Data[0].Should().Be(7);
        result.Data[2].Should().Be(4);
    }

    [Test]
    public async Task TotalClientLastSevenDayCount_GetQuery_Success()
    {
        var query = new GetTotalClientLastSevenDayCountQuery
        {
            ProjectId = 1
        };

        _clientRepository.Setup(x => x.GetListAsync(
                It.IsAny<Expression<Func<ClientDataModel, bool>>>()))
            .ReturnsAsync(new List<ClientDataModel>
            {
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(0).AddHours(-10),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 1
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(0).AddHours(-10),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 0
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(0).AddHours(-10),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 1
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-1).AddHours(-10),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 0
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-2).AddHours(-10),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 0
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-2).AddHours(-10),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 1
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-3).AddHours(-10),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 0
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-4).AddHours(-10),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 1
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-5).AddHours(-10),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 1
                },
                new()
                {
                    Id = 1,
                    Status = true,
                    CreatedAt = DateTimeOffset.Now.AddDays(-6).AddHours(-10),
                    PaidTime = DateTimeOffset.Now,
                    ProjectId = 1,
                    IsPaidClient = 0
                }
            }.AsQueryable());

        var result = await _getTotalClientLastSevenDayCountQueryHandler.Handle(query, new CancellationToken());
        result.Success.Should().BeTrue();
        result.Data[0].Should().Be(10);
        result.Data[2].Should().Be(6);
        result.Data[4].Should().Be(3);
    }
}