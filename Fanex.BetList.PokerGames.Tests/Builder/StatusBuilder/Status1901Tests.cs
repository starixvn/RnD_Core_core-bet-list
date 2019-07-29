namespace Fanex.BetList.PokerGames.Tests.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Status1901Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status1901();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket.Status = BetStatus.Won;
        }

        [Test]
        public void BuildStatusResult_IsSummaryTicket_HideStatusResult()
        {
            // Arrange
            _ticket.BetCheck = string.Empty;

            // Act.
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.False(template.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_IsDetailTicket_ShowStatusResultWithRefNoIsBetCheck()
        {
            // Arrange
            const string expectedRefNo = "'1234'";
            _ticket.BetCheck = "1234";

            // Act.
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.True(template.StatusResult.Visible);
            Assert.AreEqual(expectedRefNo, template.StatusResult.refNo);
        }
    }
}