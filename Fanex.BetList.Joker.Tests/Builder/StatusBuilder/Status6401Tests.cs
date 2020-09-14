namespace Fanex.BetList.Spadegaming.Tests.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Status6401Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status6401();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.BetId = 123;
            _ticket.BetCheck = "BetCheck";
            _ticket.CustId = 456;
            _ticket.Status = BetStatus.Won;
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
            var expectedParameters = $"{_ticket.BetId}, '{_ticket.BetCheck}', {_ticket.CustId}";

            Assert.AreEqual(expectedParameters, template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_TicketBetIdIsNullAndTicketBetCheckIsNotNullOrEmpty_SetZeroAsBetIdInParameterListOfResultLink()
        {
            // Arrange.
            _ticket.BetId = null;

            // Act.
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedParameters = $"0, '{_ticket.BetCheck}', {_ticket.CustId}";

            Assert.AreEqual(expectedParameters, template.StatusResult.betId);
        }
    }
}