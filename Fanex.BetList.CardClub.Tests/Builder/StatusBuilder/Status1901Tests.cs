namespace Fanex.BetList.CardClub.Tests.Builder.StatusBuilder
{
    using System;
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Status1901Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status1901();
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

        [Test]
        public void BuildStatusResult_IsSummaryTicket_HideStatusResult()
        {
            // Arrange
            _ticket.BetCheck = string.Empty;

            // Act.
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.False(template.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_OldTicket_ShowResultLink()
        {
            // Act.
            _ticket.WinlostDate = DateTime.Today.AddDays(-91);
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.False(template.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_NewDetailTicket_ShowResultLink()
        {
            // Act.
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.True(template.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_IsDetailTicket_ShowStatusResultWithRefNoIsBetCheck()
        {
            // Arrange

            // Act.
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedParameters = $"20, '{_ticket.BetTeam}', {_ticket.CustId}, '{_ticket.BetCheck}', '{_ticket.SiteId}'";

            Assert.AreEqual(expectedParameters, template.StatusResult.betId);
        }
    }
}