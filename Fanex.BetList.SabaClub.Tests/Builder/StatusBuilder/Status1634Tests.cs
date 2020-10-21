namespace Fanex.BetList.SabaClub.Tests.Builder.StatusBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using NSubstitute;
    using NUnit.Framework;

    public class Status1634Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status1634();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.MatchCode = "matchcode";
            _ticket.BetId = 123;
            _ticket.CustId = 456;
            _ticket.BetCheck = "BetCheck";
            _ticket.Status = BetStatus.Won;
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
                _ticket.TransId,
                $"'{_ticket.UserName}'",
                $"'{Formatter.FullDateFormat(_ticket.WinlostDate)}'",
                "0",
                _ticket.LeagueId,
                "0",
                _ticket.BetId ?? 0,
                $"'{_ticket.MatchCode}'",
                _ticket.CustId,
                $"'{_ticket.BetCheck}'"
            };
            var expectedHtml = $"<div class=\"result\" onclick=\"ViewResult({string.Join(", ", parameterList)});\">{CoreBetList.running}</div>";

            Assert.AreEqual(expectedHtml, template.result);
        }

        [TestCase(BetStatus.Won)]
        [TestCase(BetStatus.Lose)]
        [TestCase(BetStatus.Draw)]
        [TestCase(BetStatus.Refund)]
        [TestCase(BetStatus.Reject)]
        public void BuildResult_TicketIsNotRunning_ResultIsText(string status)
        {
            // Arrange.
            _ticket.Status = status;

            // Act.
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(status, template.result.ToLower());
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
            var expectedParameters = $"{_ticket.BetId}, '{_ticket.MatchCode}', {_ticket.CustId}, '{_ticket.BetCheck}'";

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
            var expectedParameters = $"0, '{_ticket.MatchCode}', {_ticket.CustId}, '{_ticket.BetCheck}'";

            Assert.AreEqual(expectedParameters, template.StatusResult.betId);
        }

        
    }
}