namespace Fanex.BetList.FGG.Test.Builder.ChoiceBuilder
{

    using System;
    using System.Globalization;
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using Core.Entities;
    using Core.Utils;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice8202Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice8202();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.BetTypeId = FGGBetTypes.Baccarat;
            _ticket.TransId = 1;
            _ticket.CustId = 1000;

            _ticketHelper.GetBetTypeNameById(FGGBetTypes.Baccarat).Returns("Baccarat");
        }

        [Test]
        public void BuildBetType_MatchCodeIsEmpty_ReturnEmpty()
        {
            ////Arrange
            _ticket.MatchCode = string.Empty;

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.IsTrue(string.IsNullOrEmpty(_choice.Template.BetType.betTypeName));
        }

        [Test]
        public void BuildBetType_MatchCodeIsNotEmpty_ReturnBetId()
        {
            ////Arrange
            _ticket.MatchCode = "123123";
            _ticket.BetCheck = "123";

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            var betIdMarkup = string.Format("<span class='fgg-bet-id'>{0}</span>", "BetID: " + "123");
            Assert.AreEqual(betIdMarkup, _choice.Template.BetType.betTypeName);
        }

        [Test]
        public void BuildMatch_Always_HideMatch()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.Match);
        }

        [TestCase(BetStatus.Won, "1")]
        [TestCase(BetStatus.Lose, "1")]
        [TestCase(BetStatus.Draw, "1")]
        [TestCase(BetStatus.Refund, "1")]
        [TestCase(BetStatus.Reject, "2")]
        [TestCase(BetStatus.Void, "2")]
        [TestCase(BetStatus.Running, "3")]
        [TestCase(BetStatus.Waiting, "3")]
        public void BuildLeague_ForTodaySummaryTicket_ReturnDetailLinkHtml(string ticketStatus, string transactionType)
        {
            ////Arrange
            _ticket.MatchCode = string.Empty;
            _ticket.WinlostDate = DateTime.Today;
            _ticket.Status = ticketStatus;

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            string expectedTemplate = BuildExpectedTemplateDetailLinkHtml(_ticket, transactionType);
            Assert.IsTrue(_choice.Template.League.ToString().Contains(expectedTemplate));
        }

        [TestCase(BetStatus.Won, "1")]
        [TestCase(BetStatus.Lose, "1")]
        [TestCase(BetStatus.Draw, "1")]
        [TestCase(BetStatus.Refund, "1")]
        [TestCase(BetStatus.Reject, "2")]
        [TestCase(BetStatus.Void, "2")]
        [TestCase(BetStatus.Running, "3")]
        [TestCase(BetStatus.Waiting, "3")]
        public void BuildLeague_ForYesterdaySummaryTicket_ReturnDetailLinkHtml(string ticketStatus, string transactionType)
        {
            ////Arrange
            _ticket.MatchCode = string.Empty;
            _ticket.WinlostDate = DateTime.Today.AddDays(-1);
            _ticket.Status = ticketStatus;

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            string expectedTemplate = BuildExpectedTemplateDetailLinkHtml(_ticket, transactionType);
            Assert.IsTrue(_choice.Template.League.ToString().Contains(expectedTemplate));
        }

        [Test]
        public void BuildLeague_ForPreviousDaysSummaryTicket_ReturnDetailLinkHtml()
        {
            ////Arrange
            _ticket.MatchCode = string.Empty;
            _ticket.WinlostDate = DateTime.Today.AddDays(-2);

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            string expectedTemplate = BuildExpectedTemplateDetailLinkHtml(_ticket, "0");
            Assert.IsTrue(_choice.Template.League.ToString().Contains(expectedTemplate));
        }

        [Test]
        public void BuildLeague_ForLeagueName_ReturnBetTypeName()
        {
            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.IsTrue(_choice.Template.League.LeagueName.leagueName.Contains('Baccarat'));
        }

        private string BuildExpectedTemplateDetailLinkHtml(ITicket ticket, string transactionType)
        {
            return string.Format(
                "<div class=\"detail fgg-detail\"" +
                " onclick='showTodayRunningBetsDetails(&quot;{0}&quot;,&quot;{1}&quot;,&quot;{2}&quot;,&quot;{3}&quot;);'>" +
                "<a class=\"detail-link\"" +
                " href=\"javascript:void(&quot;&quot;);\">Details</a></div>",
                ticket.TransId.ToString(),
                ticket.WinlostDate.ToString(CultureInfo.InvariantCulture),
                ticket.CustId.ToString(),
                transactionType);
        }
    }
}
