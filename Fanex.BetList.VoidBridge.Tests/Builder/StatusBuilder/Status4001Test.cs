namespace Fanex.BetList.Voidbridge.Tests.Builder.StatusBuilder
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using Core.Utils;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status4001Test
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IStatus _status;

        [SetUp]
        public void Setup()
        {
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _status = new Status4001();

            _ticket.CustId = 456;
            _ticket.BetTeam = "betteam";
            _ticket.CurrencyId = 20;
            _ticket.SiteId = "siteid";
        }

        [Test]
        public void BuildStatusResult_TicketBetCheckIsNotNullOrEmpty_AppendResultParameterList()
        {
            // Arrange
            _ticket.BetCheck = "BetCheck";

            // Act.
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedParameters = $"20, '{_ticket.BetTeam}', {_ticket.CustId}, '{_ticket.BetCheck}', '{_ticket.SiteId}'";

            Assert.AreEqual(expectedParameters, template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_ForSummaryTickets_HideStatusResultLink()
        {
            ////Arrange
            _ticket.BetCheck = string.Empty;

            ////Act
            _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.IsFalse(_status.Template.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_ForDetailTickets_ShowStatusResultLink()
        {
            ////Arrange
            _ticket.Status = BetStatus.Won;
            _ticket.BetCheck = "123";

            ////Act
            _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.IsTrue(_status.Template.StatusResult.Visible);
        }

        [Test]
        public void BuildResult_ForSummaryTicket_AlwaysText()
        {
            ////Act
            _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            var htmlTagMatching = new Regex("</?div[^<>]*[^<>]*>");
            var containingHtmlMarkups = htmlTagMatching.IsMatch(_status.Template.result);
            Assert.IsFalse(containingHtmlMarkups);
        }

        [Test]
        public void BuildResult_ForDetailRunningTicket_ShowRunningLink()
        {
            ////Arrange
            _ticket.BetCheck = "123";
            _ticket.Status = BetStatus.Running;
            _ticket.SportTypeId = 1;
            _ticket.WinlostDate = DateTime.Today;
            _ticket.CustId = 54321;

            ////Act
            _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            var expectedRunningLink = BuildRunningLink(_ticket, "Running");
            Assert.AreEqual(expectedRunningLink, _status.Template.result);
        }

        [Test]
        public void BuildResult_HasResultStringData_ShowResultString()
        {
            ////Arrange
            _ticket.Status = BetStatus.Won;

            ////Act
            _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.AreEqual("Won", _status.Template.result);
        }

        [Test]
        public void BuildResult_HasNoResultStringData_ShowStatusString()
        {
            ////Arrange
            _ticket.Status = "status";

            ////Act
            _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.AreEqual("status", _status.Template.result);
        }

        private static string BuildRunningLink(ITicket ticket, string status)
        {
            var viewRunningTicketResultTag = new[]
                    {
                        "<div class=\"result\" onclick=\"ViewResult(",
                        ticket.MatchId.ToString(CultureInfo.InvariantCulture),
                        ", ",
                        string.IsNullOrEmpty(ticket.Race) ? "0" : ticket.Race,
                        ", ",
                        ticket.BetTypeId.ToString(CultureInfo.InvariantCulture),
                        ", ",
                        ticket.SportTypeId.ToString(CultureInfo.InvariantCulture),
                        ", ",
                        "'",
                        ticket.BetCheck ?? string.Empty,
                        "'",
                        ", ",
                        "'",
                        ticket.UserName ?? string.Empty,
                        "'",
                        ", ",
                        "'",
                        Formatter.FullDateFormat(ticket.WinlostDate),
                        "'",
                        ", ",
                        "0, ",
                        ticket.LeagueId.ToString(CultureInfo.InvariantCulture),
                        ", 0, 0, '",
                        ticket.BetCheck ?? string.Empty,
                        "','",
                        ticket.CustId.ToString(),
                        "');\">",
                        status,
                        "</div>"
                    };

            return string.Join(null, viewRunningTicketResultTag);
        }

        [Test]
        public void BuildResultLRF_Always_RemoveHtmlTag()
        {
            ////Act
            _status.RenderRTF(_ticket, _ticketHelper, null, false, new RTFHelper().Clone());

            ////Assert
            var htmlTagMatching = new Regex("</?div[^<>]*[^<>]*>");
            var containingHtmlMarkups = htmlTagMatching.IsMatch(_status.Template.result);
            Assert.IsFalse(containingHtmlMarkups);
        }
    }
}