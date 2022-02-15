namespace Fanex.BetList.PragmaticLiveCasino.Test.Builder.StatusBuilder
{
    using System;
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status3701Test
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IStatus _status;

        [SetUp]
        public void Setup()
        {
            _status = new Status3701();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.BetCheck = "BetCheck";
            _ticket.CustId = 456;
            _ticket.Status = BetStatus.Won;
            _ticket.MatchCode = "matchcode";
            _ticket.CurrencyId = 20;
            _ticket.SiteId = "siteid";
            _ticket.WinlostDate = DateTime.Today;
        }

        [Test]
        public void BuildStatusResult_BetCheckDataIsEmpty_HideStatusResultLink()
        {
            ////Arrange
            _ticket.BetCheck = string.Empty;

            ////Act
            _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
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
        public void BuildStatusResult_OldDetailTicket_ShowResultLink()
        {
            // Act.
            _ticket.WinlostDate = DateTime.Today.AddDays(-31);
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.False(template.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_TicketBetCheckIsNotNullOrEmpty_AppendResultParameterList()
        {
            // Act.
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedParameters = $"20, '{_ticket.MatchCode}', {_ticket.CustId}, '{_ticket.BetCheck}', '{_ticket.SiteId}'";

            Assert.AreEqual(expectedParameters, template.StatusResult.betId);
        }
    }
}