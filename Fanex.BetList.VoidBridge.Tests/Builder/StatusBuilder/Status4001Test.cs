namespace Fanex.BetList.VoidBridge.Tests.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using Core.Utils;
    using NSubstitute;
    using NUnit.Framework;
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    [TestFixture]
    public class Status4001Test
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IStatus _status;

        [SetUp]
        public void Setup()
        {
            this._ticket = Substitute.For<ITicket>();
            this._ticketHelper = Substitute.For<ITicketHelper>();
            this._status = new Status4001();
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
            _ticket.BetCheck = "123";

            ////Act
            _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.IsTrue(_status.Template.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_ForDetailTickets_RefNoIsBetCheckString()
        {
            ////Arrange
            _ticket.BetCheck = "123";

            ////Act
            _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.AreEqual(_status.Template.StatusResult.refNo, "'123'");
        }

        [Test]
        public void BuildResult_ForSummaryTicket_AlwaysText()
        {
            ////Act
            _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            var htmlTagMatching = new Regex("</?div[^<>]*[^<>]*>");
            var containingHtmlMarkups = htmlTagMatching.IsMatch(_status.Template.result.ToString());
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

            ////Act
            _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            var expectedRunningLink = BuildRunningLink(_ticket, "Running");
            Assert.AreEqual(_status.Template.result, expectedRunningLink);
        }

        [Test]
        public void BuildResult_HasResultStringData_ShowResultString()
        {
            ////Arrange
            _ticket.Status = BetStatus.Won;

            ////Act
            _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.AreEqual(_status.Template.result, "Won");
        }

        [Test]
        public void BuildResult_HasNoResultStringData_ShowStatusString()
        {
            ////Arrange
            _ticket.Status = "status";

            ////Act
            _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.AreEqual(_status.Template.result, "status");
        }

        private string BuildRunningLink(ITicket ticket, string status)
        {
            var viewRunningTicketResultTag = new string[]
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
                        ", 0, 0);\">",
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
            var containingHtmlMarkups = htmlTagMatching.IsMatch(_status.Template.result.ToString());
            Assert.IsFalse(containingHtmlMarkups);
        }
    }
}