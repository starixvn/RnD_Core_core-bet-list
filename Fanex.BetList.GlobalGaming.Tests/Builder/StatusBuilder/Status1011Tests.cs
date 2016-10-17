namespace Fanex.BetList.GlobalGaming.Tests.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status1011Tests
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IStatus _status;

        [SetUp]
        public void SetUp()
        {
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _status = new Status1011();
        }

        [Test]
        public void BuildStatusResult_Always_StatusResultIsNull()
        {
            // Act
            var template = _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(template.StatusResult);
        }
    }
}