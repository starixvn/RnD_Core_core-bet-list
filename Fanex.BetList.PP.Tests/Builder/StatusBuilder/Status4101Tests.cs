namespace Fanex.BetList.PP.Tests.Builder.StatusBuilder
{
    using System;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using NSubstitute;
    using NUnit.Framework;

    public class Status4101Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status4101();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.BetId = 123;
            _ticket.CustId = 456;
            _ticket.BetCheck = "BetCheck";
            _ticket.Status = BetStatus.Won;
            _ticket.BetTeam = "betteam";
            _ticket.CurrencyId = 20;
            _ticket.SiteId = "siteid";
            _ticket.WinlostDate = DateTime.Today;
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
            var expectedParameters = $"20, '{_ticket.BetTeam}', {_ticket.CustId}, '{_ticket.BetCheck}', '{_ticket.SiteId}', ''";

            Assert.AreEqual(expectedParameters, template.StatusResult.betId);
        }

        [Test]
        public void BuildResult_TicketIsRunningAndBetCheckIsNotNullOrEmpty_ResultIsDisplayedAsLink()
        {
            // Arrange.
            _ticket.Status = BetStatus.Running;

            // Act.
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var parameterList = new object[]
            {
                _ticket.MatchId,
                string.IsNullOrEmpty(_ticket.Race) ? "0" : _ticket.Race,
                _ticket.BetTypeId,
                _ticket.SportTypeId,
                $"'{_ticket.TransId}'",
                $"'{_ticket.UserName}'",
                $"'{Formatter.FullDateFormat(_ticket.WinlostDate)}'",
                "0",
                _ticket.LeagueId,
                "0",
                _ticket.CurrencyId,
                $"'{_ticket.BetTeam}'",
                _ticket.CustId,
                $"'{_ticket.BetCheck}'",
                $"'{_ticket.SiteId}'",
            };
            var expectedHtml = $"<div class=\"result\" onclick=\"ViewResult({string.Join(", ", parameterList)});\">{CoreBetList.running}</div>";

            Assert.AreEqual(expectedHtml, template.result);
        }
    }
}