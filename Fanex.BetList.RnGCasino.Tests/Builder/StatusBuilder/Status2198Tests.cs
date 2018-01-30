namespace Fanex.BetList.RnGCasino.Tests.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Status2198Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status2198();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildStatusResult_Always_HideResultLink()
        {
            // Act
            var status = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.False(status.StatusResult.Visible);
            Assert.AreEqual("-", status.result);
        }
    }
}