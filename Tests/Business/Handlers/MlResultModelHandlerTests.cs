using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Entities.Concrete;
using MediatR;
using System.Linq;
using Business.Handlers.MlResults.Queries;
using FluentAssertions;
using MongoDB.Bson;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class MlResultModelHandlerTests
    {
        Mock<IMlResultRepository> _mlResultModelRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _mlResultModelRepository = new Mock<IMlResultRepository>();
            _mediator = new Mock<IMediator>();
        }


        [Test]
        public async Task MlResultModel_GetQueries_Success()
        {
            //Arrange
            var query = new GetMlResultByProjectAndProductIdQuery
            {
                ProductId = 1,
                ProjectId = "sdfsdf"
            };

            _mlResultModelRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ChurnBlokerMlResult, bool>>>()))
                        .ReturnsAsync(new List<ChurnBlokerMlResult> { new ChurnBlokerMlResult()
                        {
                            DateTime = new DateTime(),
                            ClientId = "sdfsdf",
                            Id = new ObjectId(),
                            ProductId = 1,
                            ProjectId = "sdfsdf",
                            ResultValue = 1
                        },
                            new ChurnBlokerMlResult()
                            {
                                DateTime = new DateTime(),
                                ClientId = "AasdSsd",
                                Id = new ObjectId(),
                                ProductId = 1,
                                ProjectId = "sdfsdf",
                                ResultValue = 2
                            },

                        }.AsQueryable());

            var handler = new GetMlResultByProjectAndProductIdQuery.GetMlResultByProjectAndProductIdQueryHandler(_mlResultModelRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            x.Data.ToList().Count.Should().BeGreaterThan(1);

        }
    }
}

