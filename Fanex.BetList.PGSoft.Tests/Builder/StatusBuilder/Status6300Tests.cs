namespace Fanex.BetList.PGSoft.Tests.Builder.StatusBuilder
{
    using System;
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Status6300Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status6300();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.BetCheck = "BetCheck";
            _ticket.CustId = 456;
            _ticket.Status = BetStatus.Won;
            _ticket.BetTeam = "betteam";
            _ticket.CurrencyId = 20;
            _ticket.SiteId = "siteid";
            _ticket.WinlostDate = DateTime.Today;
        }

        public void BuildStatusResult_TicketBetCheckIsNullOrEmpty_HideResultLink(string betCheck)
        {
            // Arrange.

            // Act.
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.False(template.StatusResult.Visible);
        }
    }
}