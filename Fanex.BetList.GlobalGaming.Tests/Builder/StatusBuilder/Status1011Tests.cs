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
        public void BuildStatusResult_FromSummaryTicket_StatusResultIsNull()
        {
            // Arrange
            _ticket.BetCheck = null;

            // Act
            var template = _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(template.StatusResult);
        }

        [TestCase("won")]
        [TestCase("LosE")]
        [TestCase("DRAW")]
        public void BuildStatusResult_FromDetailTicketHasWonLostStatus_StatusResultIsVisible(string status)
        {
            // Arrange
            _ticket.BetCheck = "123";
            _ticket.Status = status;

            // Act
            var template = _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
        }

        [TestCase("won")]
        [TestCase("LosE")]
        [TestCase("DRAW")]
        public void BuildStatusResult_FromDetailTicketHasWonLostStatus_BetIdIncludeBetCheckAndCustId(string status)
        {
            // Arrange
            const string BetCheck = "123";
            const long CustId = 100;
            const long BetId = 100;
            _ticket.BetCheck = BetCheck;
            _ticket.CustId = CustId;
            _ticket.Status = status;
            _ticket.BetId = BetId;

            // Act
            var template = _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedBetId = $"{BetId}, '{BetCheck}', {CustId}";
            Assert.AreEqual(expectedBetId, template.StatusResult.betId);
        }

        [TestCase("running")]
        [TestCase("Reject")]
        [TestCase("Void")]
        [TestCase("refund")]
        public void BuildStatusResult_FromDetailTicketHasNoWonLostStatus_StatusResultIsNull(string status)
        {
            // Arrange
            _ticket.BetCheck = "123";
            _ticket.Status = status;

            // Act
            var template = _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(template.StatusResult);
        }
    }
}