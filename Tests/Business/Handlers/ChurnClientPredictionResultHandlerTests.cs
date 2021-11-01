
using Business.Handlers.ChurnClientPredictionResults.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ChurnClientPredictionResults.Queries.GetChurnClientCountByDateQuery;
using static Business.Handlers.ChurnClientPredictionResults.Queries.GetChurnClientCountByOfferQuery;
using Entities.Concrete;
using Business.Constants;
using MediatR;
using System.Linq;
using Entities.Dtos;
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
        public async Task ChurnClientPredictionResult_GetByDateQuery_Success()
        {
            //Arrange
            var query = new GetChurnClientCountByDateQuery();
            query.ProjectId = "dfsdfgsgfjlsd";
            query.StartTime = DateTime.Now;
            query.FinishTime = DateTime.Now;
                
            _churnClientPredictionResultRepository.Setup(x =>
                    x.GetListAsync(It.IsAny<Expression<Func<ChurnClientPredictionResult, bool>>>()))
                .ReturnsAsync(new List<ChurnClientPredictionResult> {
                    new ChurnClientPredictionResult(){
                        ChurnPredictionDate = new DateTime(),
                        ClientId = "sdfsfas",
                        ClientsOfferModelDto = new ClientsOfferModelDto[]
                        {
                            new ClientsOfferModelDto()

                        },
                        Id = new ObjectId(),
                        ProjectId = "dfsdfgsgfjlsd"

                },

                }.AsQueryable());

            var handler = new GetChurnClientCountByDateQueryHandler(_churnClientPredictionResultRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            x.Data.Should().Be(1);

        }

        [Test]
        public async Task ChurnClientPredictionResult_GetQueries_Success()
        {
            //Arrange
            var query = new GetChurnClientCountByOfferQuery();
            query.Name = "test";
            query.FinishTime = DateTime.Now;
            query.ProjectId = "dfsdfgsgfjlsd";
            query.Version = 1;

            _churnClientPredictionResultRepository.Setup(x =>
                    x.GetListAsync(It.IsAny<Expression<Func<ChurnClientPredictionResult, bool>>>()))
                        .ReturnsAsync(new List<ChurnClientPredictionResult>
                        {
                            new ChurnClientPredictionResult(){
                                ChurnPredictionDate = new DateTime(),
                                ClientId = "sdfsfas",
                                ClientsOfferModelDto = new ClientsOfferModelDto[]
                                {
                                    new ClientsOfferModelDto()
                                    {
                                        FinishTime = DateTime.Now,
                                        OfferName = "test",
                                        StartTime = DateTime.Now,
                                        Version = 1
                                    }

                                },
                                Id = new ObjectId(),
                                ProjectId = "dfsdfgsgfjlsd"

                            },
                        }.AsQueryable());

            var handler = new GetChurnClientCountByOfferQueryHandler(_churnClientPredictionResultRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            x.Data.Should().Be(1);

        }

    }
}

