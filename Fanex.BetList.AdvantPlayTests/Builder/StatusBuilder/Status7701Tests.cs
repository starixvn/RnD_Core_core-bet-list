namespace Fanex.BetList.AdvantPlay.Tests.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Status7701Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status7701();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.MatchCode = "matchcode";
            _ticket.BetCheck = "BetCheck";
            _ticket.CustId = 456;
            _ticket.Status = BetStatus.Won;
            _ticket.BetTeam = "betteam";
        }

        [TestCase(null)]
        [TestCase("")]
        public void BuildStatusResult_TicketBetCheckIsNullOrEmpty_HideResultLink(string betCheck)
        {
            // Arrange.
            _ticket.BetCheck = betCheck;

            // Act.
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.False(template.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_TicketBetCheckIsNotNullOrEmpty_ShowResultLink()
        {
            // Act.
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.True(template.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_TicketBetCheckIsNotNullOrEmpty_AppendBetCheckAndCustIdAfterBetIdInParameterListOfResultLink()
        {
            // Act.
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedParameters = $"matchcode, '{_ticket.BetTeam}', {_ticket.CustId}, '{_ticket.BetCheck}'";

            Assert.AreEqual(expectedParameters, template.StatusResult.betId);
        }
    }
}