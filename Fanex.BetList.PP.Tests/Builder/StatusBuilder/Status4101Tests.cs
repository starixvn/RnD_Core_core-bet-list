namespace Fanex.BetList.PP.Tests.Builder.StatusBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
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
        }

        [TestCase(null)]
        [TestCase("")]
        public void BuildStatusResult_TicketBetCheckIsNullOrEmpty_HideResultLink(string betCheck)
        {
            // Arrange.
            _ticket.BetCheck = betCheck;

            // Act.
            Status_Template template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.False(template.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_TicketBetCheckIsNotNullOrEmpty_ShowResultLink()
        {
            // Arrange.
            _ticket.BetCheck = "BetCheck";
            _ticket.Status = BetStatus.Won;

            // Act.
            Status_Template template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.True(template.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_TicketBetCheckIsNotNullOrEmpty_AppendBetCheckAndCustIdAfterBetIdInParameterListOfResultLink()
        {
            // Arrange.
            _ticket.BetId = 123;
            _ticket.BetCheck = "BetCheck";
            _ticket.CustId = 456;

            // Act.
            Status_Template template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedParameters = $"123, 'BetCheck', 456";

            Assert.AreEqual(expectedParameters, template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_TicketBetIdIsNullAndTicketBetCheckIsNotNullOrEmpty_SetZeroAsBetIdInParameterListOfResultLink()
        {
            // Arrange.
            _ticket.BetId = null;
            _ticket.BetCheck = "BetCheck";
            _ticket.CustId = 456;

            // Act.
            Status_Template template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedParameters = $"0, 'BetCheck', 456";

            Assert.AreEqual(expectedParameters, template.StatusResult.betId);
        }

        [Test]
        public void BuildResult_TicketIsRunningAndBetCheckIsNotNullOrEmpty_ResultIsDisplayedAsLink()
        {
            // Arrange.
            _ticket.BetCheck = "BetCheck";
            _ticket.Status = BetStatus.Running;

            // Act.
            Status_Template template = _status.Render(_ticket, _ticketHelper, null, false);

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
                $"'{_ticket.BetCheck}'",
                _ticket.CustId
            };
            var expectedHtml = $"<div class=\"result\" onclick=\"ViewResult({string.Join(", ", parameterList)});\">{CoreBetList.running}</div>";

            Assert.AreEqual(expectedHtml, template.result);
        }
    }
}