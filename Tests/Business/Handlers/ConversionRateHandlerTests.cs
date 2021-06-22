
using Business.Handlers.ConversionRates.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ConversionRates.Queries.GetConversionRateQuery;
using Entities.Concrete;
using static Business.Handlers.ConversionRates.Queries.GetConversionRatesQuery;
using static Business.Handlers.ConversionRates.Commands.CreateConversionRateCommand;
using Business.Handlers.ConversionRates.Commands;
using Business.Constants;
using static Business.Handlers.ConversionRates.Commands.UpdateConversionRateCommand;
using static Business.Handlers.ConversionRates.Commands.DeleteConversionRateCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using MongoDB.Bson;
using Entities.Concrete.ChartModels;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ConversionRateHandlerTests
    {
        Mock<IConversionRateRepository> _conversionRateRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _conversionRateRepository = new Mock<IConversionRateRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task ConversionRate_GetQuery_Success()
        {
            //Arrange
            var query = new GetConversionRateQuery();

            _conversionRateRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>())).ReturnsAsync(new ConversionRate()
//propertyler buraya yazılacak
//{																		
//ConversionRateId = 1,
//ConversionRateName = "Test"
//}
);

            var handler = new GetConversionRateQueryHandler(_conversionRateRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ConversionRateId.Should().Be(1);

        }

        [Test]
        public async Task ConversionRate_GetQueries_Success()
        {
            //Arrange
            var query = new GetConversionRatesQuery();

            _conversionRateRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ConversionRate, bool>>>()))
                        .ReturnsAsync(new List<ConversionRate> { new ConversionRate() { /*TODO:propertyler buraya yazılacak ConversionRateId = 1, ConversionRateName = "test"*/ } }.AsQueryable());

            var handler = new GetConversionRatesQueryHandler(_conversionRateRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ConversionRate>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task ConversionRate_CreateCommand_Success()
        {
            ConversionRate rt = null;
            //Arrange
            var command = new CreateConversionRateCommand();
            //propertyler buraya yazılacak
            //command.ConversionRateName = "deneme";

            _conversionRateRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(rt);

            _conversionRateRepository.Setup(x => x.Add(It.IsAny<ConversionRate>()));

            var handler = new CreateConversionRateCommandHandler(_conversionRateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task ConversionRate_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateConversionRateCommand();
            //propertyler buraya yazılacak 
            //command.ConversionRateName = "test";

            _conversionRateRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ConversionRate, bool>>>()))
                                           .ReturnsAsync(new List<ConversionRate> { new ConversionRate() { /*TODO:propertyler buraya yazılacak ConversionRateId = 1, ConversionRateName = "test"*/ } }.AsQueryable());

            _conversionRateRepository.Setup(x => x.Add(It.IsAny<ConversionRate>()));

            var handler = new CreateConversionRateCommandHandler(_conversionRateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task ConversionRate_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateConversionRateCommand();
            //command.ConversionRateName = "test";

            _conversionRateRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new ConversionRate() { /*TODO:propertyler buraya yazılacak ConversionRateId = 1, ConversionRateName = "deneme"*/ });

            _conversionRateRepository.Setup(x => x.UpdateAsync(It.IsAny<ObjectId>(), It.IsAny<ConversionRate>()));

            var handler = new UpdateConversionRateCommandHandler(_conversionRateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task ConversionRate_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteConversionRateCommand();

            _conversionRateRepository.Setup(x => x.GetByIdAsync(It.IsAny<ObjectId>()))
                        .ReturnsAsync(new ConversionRate() { /*TODO:propertyler buraya yazılacak ConversionRateId = 1, ConversionRateName = "deneme"*/});

            _conversionRateRepository.Setup(x => x.Delete(It.IsAny<ConversionRate>()));

            var handler = new DeleteConversionRateCommandHandler(_conversionRateRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());


            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

