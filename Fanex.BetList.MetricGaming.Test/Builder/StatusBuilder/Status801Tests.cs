namespace Fanex.BetList.MetricGaming.Test.Builder.StatusBuilder
{
    using System.Globalization;
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Status801Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status801();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.Status = BetStatus.Won;
        }

        [Test]
        public void BuildStatusResult_SettledTicketBetIdIsNull_ResultLinkBetIdIsZero()
        {
            // Arrange.
            _ticket.BetId = null;

            // Act.
            var template = _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual("0", template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult__SettledTicketBetIdBetIdHasValue_ResultLinkBetIdIsTicketBetId()
        {
            // Arrange.
            const long betId = 80101;

            _ticket.BetId = betId;

            // Act.
            var template = _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(betId.ToString(CultureInfo.InvariantCulture), template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult__RefundedTicket_HideResultLink()
        {
            // Arrange.
            _ticket.Status = BetStatus.Refund;

            // Act.
            var template = _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.IsFalse(template.StatusResult.Visible);
        }
    }
}