using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Business.Handlers.MlResults.Queries;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;

namespace Tests.Business.Handlers
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
                ProjectId = 21
            };

            _mlResultModelRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ChurnBlokerMlResult, bool>>>()))
                        .ReturnsAsync(new List<ChurnBlokerMlResult> { new ChurnBlokerMlResult()
                        {
                            DateTime = new DateTime(),
                            ClientId = 1,
                            Id = 1,
                            ProductId = 1,
                            ProjectId = 21,
                            ResultValue = 1
                        },
                            new ChurnBlokerMlResult()
                            {
                                DateTime = new DateTime(),
                                ClientId = 2,
                                Id = 3,
                                ProductId = 1,
                                ProjectId = 22,
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

