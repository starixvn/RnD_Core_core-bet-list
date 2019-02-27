namespace Fanex.BetList.BetTrade.Test.Builder.StatusBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status902Tests
    {
        private IStatus status;
        private ITicketHelper ticketHelper;
        private ITicket ticket;

        [SetUp]
        public void SetUp()
        {
            status = new Status902();
            ticketHelper = Substitute.For<ITicketHelper>();
            ticket = new Ticket
            {
                Status = "Won"
            };
        }

        [TestCase("Void")]
        [TestCase("Refund")]
        public void BuildResult_StatusIsVoidOrRefund_ShowSoldStatusBelow(string ticketStatus)
        {
            // Arrange
            ticket.Status = ticketStatus;

            // Act
            status.Render(ticket, ticketHelper, null, false);

            // Assert
            Assert.AreEqual(
                $"{ticketStatus}<br/><div style='font-weight: normal;'>({CoreBetList.Bought})</div>",
                status.Template.result);
        }

        [TestCase("Running")]
        [TestCase("Won")]
        [TestCase("Lose")]
        [TestCase("Draw")]
        public void BuildResult_StatusIsNotVoidOrRefund_ShowSoldStatus(string ticketStatus)
        {
            // Arrange
            ticket.Status = ticketStatus;

            // Act
            status.Render(ticket, ticketHelper, null, false);

            // Assert
            Assert.AreEqual(CoreBetList.Bought, status.Template.result);
        }
    }
}