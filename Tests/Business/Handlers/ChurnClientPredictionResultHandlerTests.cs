using DataAccess.Abstract;
using MediatR;
using Moq;
using NUnit.Framework;

namespace Tests.Business.Handlers
{
    [TestFixture]
    public class ChurnClientPredictionResultHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _churnClientPredictionResultRepository = new Mock<IChurnClientPredictionResultRepository>();
            _mediator = new Mock<IMediator>();
        }

        private Mock<IChurnClientPredictionResultRepository> _churnClientPredictionResultRepository;
        private Mock<IMediator> _mediator;
    }
}