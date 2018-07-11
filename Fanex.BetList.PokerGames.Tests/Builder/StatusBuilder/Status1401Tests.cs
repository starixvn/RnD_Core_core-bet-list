namespace Fanex.BetList.PokerGames.Tests.Builder.StatusBuilder
{
    using Core.App_GlobalResources;
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using Core.Templates;
    using Core.Utils;
    using NSubstitute;
    using NUnit.Framework;

    public class Status1401Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status1401();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildStatusResult_SettledTicket_AppendBetCheckAndCustIdAfterBetIdInParameterListOfResultLink()
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
        public void BuildStatusResult_SettledTicketAndNullBetId_SetZeroAsBetIdInParameterListOfResultLink()
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
        public void BuildResult_RunningTicket_ResultIsDisplayedAsLink()
        {
            // Arrange.
            _ticket.Status = "running";
            _ticket.BetCheck = "BetCheck";

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

        [Test]
        public void RenderRTF_RunningTicket_ResultLinkDoesNotContainHtmlTag()
        {
            // Arrange
            _ticket.Status = "running";
            _ticketHelper.IsVnIp(Arg.Any<string>()).Returns(true);

            // Act
            var actualStatus = _status.RenderRTF(_ticket, _ticketHelper, null, false, new RTFHelper().Clone());

            // Assert
            Assert.AreEqual(CoreBetList.running, actualStatus.ToString());
        }
    }
}