
using Business.Handlers.OfferBehaviorModels.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OfferBehaviorModels.Queries.GetOfferBehaviorDtoQuery;
using Entities.Concrete;
using Business.Constants;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OfferBehaviorModelHandlerTests
    {
        Mock<IOfferBehaviorModelRepository> _offerBehaviorModelRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _offerBehaviorModelRepository = new Mock<IOfferBehaviorModelRepository>();
            _mediator = new Mock<IMediator>();
        }

     

        [Test]
        public async Task OfferBehaviorModel_GetQueries_Success()
        {
            //Arrange
            var query = new GetOfferBehaviorDtoQuery();
            query.Name = "hello";
            query.ProjectId = "fswfs";
            query.Version = 1;

           _offerBehaviorModelRepository.Setup(x => x
                    .GetListAsync(It.IsAny<Expression<Func<OfferBehaviorModel, bool>>>()))
                        .ReturnsAsync(new List<OfferBehaviorModel> { 
                        
                            new OfferBehaviorModel()
                        {
                            DateTime = new DateTime(),
                            Id = new ObjectId(),
                            ProjectId = "fswfs",
                            Version = 1,
                            OfferName = "hello",
                            ClientId = "aaa",
                            CustomerId = "bgdghbsdhs",
                            IsBuyOffer = 0

                        }, 
                            
                            new OfferBehaviorModel()
                        {
                            DateTime = new DateTime(),
                            Id = new ObjectId(),
                            ProjectId = "fswfs",
                            Version = 1,
                            OfferName = "hello",
                            ClientId = "bbb",
                            CustomerId = "bgdghbsdhs",
                            IsBuyOffer = 1

                        },
                            
                        }.AsQueryable());

            var handler = new GetOfferBehaviorDtoQueryHandler(_offerBehaviorModelRepository.Object, _mediator.Object);

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

