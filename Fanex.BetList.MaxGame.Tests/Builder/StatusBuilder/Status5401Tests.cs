namespace Fanex.BetList.MaxGame.Tests.Builder.StatusBuilder
{
    using System;
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using NSubstitute;
    using NUnit.Framework;

    public class Status5401Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status5401();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.BetId = 123;
            _ticket.BetCheck = "BetCheck";
            _ticket.EventDate = DateTime.Now;
            _ticket.CustId = 456;
            _ticket.Status = BetStatus.Won;
            _ticket.BetTeam = "4100200";
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
        public void BuildStatusResult_TicketBetCheckIsNotNullOrEmpty_AppendSiteIdAndBetCheckAndEventDateResultLink()
        {
            // Act.
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedParameters = $"{_ticket.BetTeam}, '{Formatter.FullDateFormat(_ticket.EventDate.Value)}', {_ticket.CustId}, '{_ticket.BetCheck}'";

            Assert.AreEqual(expectedParameters, template.StatusResult.betId);
        }        
    }
}