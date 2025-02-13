namespace Fanex.BetList.GPI.Tests.Builder.StatusBuilder
{
    using System;
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Status15199Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status15199();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.BetCheck = "BetCheck";
            _ticket.CustId = 456;
            _ticket.Status = BetStatus.Won;
            _ticket.BetTeam = "betteam";
            _ticket.CurrencyId = 20;
            _ticket.SiteId = "siteid";
            _ticket.WinlostDate = DateTime.Today;
            _ticket.BetTypeId = 15199;
        }

        [Test]
        public void BuildStatusResult_DetailTicket_HideResultLink()
        {
            // Act.
            _ticket.WinlostDate = DateTime.Today.AddDays(-1);
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.False(template.StatusResult.Visible);
        }
    }
}