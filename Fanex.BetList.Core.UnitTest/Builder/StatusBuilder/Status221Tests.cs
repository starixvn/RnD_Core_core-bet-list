namespace Fanex.BetList.Core.UnitTest.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Status221Tests
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IStatus _status;

        [SetUp]
        public void SetUp()
        {
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _status = new Status221();
        }

        [TestCase(BetStatus.Won)]
        [TestCase(BetStatus.HalfLose)]
        [TestCase(BetStatus.Lose)]
        [TestCase(BetStatus.HalfLose)]
        [TestCase(BetStatus.Draw)]
        public void BuildStatusResult_SettledTicket_AppendBetIdWithHandicap1AndHandicap2(string status)
        {
            // Arrange.
            _ticket.Status = status;
            _ticket.Handicap1 = 128;
            _ticket.Handicap2 = 10;

            // Act.
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            string expectedBetId = $"{_ticket.Handicap1}, {_ticket.Handicap2}";

            Assert.AreEqual(expectedBetId, _status.Template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_RefundedTicket_HideResultLink()
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