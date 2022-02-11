using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Business.Handlers.OfferBehaviorModels.Queries;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using static Business.Handlers.OfferBehaviorModels.Queries.GetOfferBehaviorDtoQuery;

namespace Tests.Business.Handlers
{
    [TestFixture]
    public class OfferBehaviorModelHandlerTests
    {
        Mock<IOfferBehaviorModelRepository> _offerBehaviorModelRepository;
        [SetUp]
        public void Setup()
        {
            _offerBehaviorModelRepository = new Mock<IOfferBehaviorModelRepository>();
        }

     

        [Test]
        public async Task OfferBehaviorModel_GetQueries_Success()
        {
            //Arrange
            var query = new GetOfferBehaviorDtoQuery
            {
                Name = "hello",
                ProjectId = 12,
                Version = 1
            };

            _offerBehaviorModelRepository.Setup(x => x
                    .GetListAsync(It.IsAny<Expression<Func<OfferBehaviorModel, bool>>>()))
                        .ReturnsAsync(new List<OfferBehaviorModel> { 
                        
                            new OfferBehaviorModel()
                        {
                            DateTime = new DateTime(),
                            Id = 1,
                            ProjectId = 21,
                            Version = 1,
                            OfferName = "hello",
                            ClientId = 1,
                            CustomerId = 2,
                            IsBuyOffer = 0

                        }, 
                            
                            new OfferBehaviorModel()
                        {
                            DateTime = new DateTime(),
                            Id = 1,
                            ProjectId = 22,
                            Version = 2,
                            OfferName = "hello",
                            ClientId = 2,
                            CustomerId = 2,
                            IsBuyOffer = 0

                        },
                            
                        }.AsQueryable());

            var handler = new GetOfferBehaviorDtoQueryHandler(_offerBehaviorModelRepository.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            x.Data.ToList().Count.Should().Be(2);
            x.Data.ToList()[0].Version.Should().Be(1);
            x.Data.ToList()[0].IsBuyOffer.Should().Be(0);
            x.Data.ToList()[0].OfferName.Should().Be("hello");

        }
   
    }
}

