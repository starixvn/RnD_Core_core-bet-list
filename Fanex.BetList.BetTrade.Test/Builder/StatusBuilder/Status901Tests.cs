namespace Fanex.BetList.BetTrade.Test.Builder.StatusBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status901Tests
    {
        private IStatus status;
        private ITicketHelper ticketHelper;
        private ITicket ticket;
        private RTFHelper rtfHelper;

        [SetUp]
        public void SetUp()
        {
            status = new Status901();
            ticketHelper = Substitute.For<ITicketHelper>();
            ticket = new Ticket
            {
                Status = BetStatus.Won
            };

            rtfHelper = new RTFHelper().Clone();
        }

        [TestCase(BetStatus.Void, "Void")]
        [TestCase(BetStatus.Refund, "Refund")]
        public void BuildResult_StatusIsVoidOrRefund_ShowSoldStatusBelow(string ticketStatus, string originalDisplayedStatus)
        {
            // Arrange
            ticket.Status = ticketStatus;

            // Act
            status.Render(ticket, ticketHelper, null, false);

            // Assert
            Assert.AreEqual(
                $"{originalDisplayedStatus}<br/><div style='font-weight: normal;'>({CoreBetList.Sold})</div>",
                status.Template.result);
        }

        [TestCase(BetStatus.Running)]
        [TestCase(BetStatus.Won)]
        [TestCase(BetStatus.Lose)]
        [TestCase(BetStatus.Draw)]
        public void BuildResult_StatusIsNotVoidOrRefund_ShowSoldStatus(string ticketStatus)
        {
            // Arrange
            ticket.Status = ticketStatus;

            // Act
            status.Render(ticket, ticketHelper, null, false);

            // Assert
            Assert.AreEqual(CoreBetList.Sold, status.Template.result);
        }

        [Test]
        public void BuildStatusResult_Always_Hide()
        {
            // Act
            status.Render(ticket, ticketHelper, null, false);

            // Assert
            Assert.False(status.Template.StatusResult.Visible);
        }

        [Test]
        public void BuildResult_Always_DisableDetectAbnormalBet()
        {
            // Arrange
            ticket.Status = BetStatus.Void;
            ticket.ExtraInfo = "<p1 ab=\"1\" />";

            // Act
            status.Render(ticket, ticketHelper, null, true);

            // Assert
            Assert.AreEqual(
                $"{CoreBetList.voided}<br/><div style='font-weight: normal;'>({CoreBetList.Sold})</div>",
                status.Template.result);
        }

        [TestCase(BetStatus.Void, "Void\n(Sold)\n")]
        [TestCase(BetStatus.Won, "Sold\n")]
        public void BuildResultRTF_Always_GetExpectedRTFText(string ticketStatus, string expected)
        {
            // Arrange
            ticket.Status = ticketStatus;

            // Act
            var actual = status.RenderRTF(ticket, ticketHelper, null, false, rtfHelper);

            // Assert
            Assert.AreEqual(expected, actual.ToString());
        }
    }
}