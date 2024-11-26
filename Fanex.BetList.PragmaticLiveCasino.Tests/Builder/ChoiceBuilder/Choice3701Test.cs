namespace Fanex.BetList.PragmaticLiveCasino.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using Core.Entities;
    using Core.Utils;
    using NSubstitute;
    using NUnit.Framework;
    using System;
    using System.Globalization;

    [TestFixture]
    public class Choice3701Test
    {
        private const string BetTypeName = "BaccaratA";

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice3701();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.BetTypeId = PragmaticLiveCasinoBetTypes.BaccaratA;
            _ticket.TransId = 1;
            _ticket.CustId = 1000;

            _ticketHelper.GetBetTypeNameById(PragmaticLiveCasinoBetTypes.BaccaratA).Returns(BetTypeName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            ////Arrange

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.IsNull(_choice.Template.Handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetbetTeamClassNameIsFavorite()
        {
            ////Arrange

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.AreEqual("favorite", _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetType_SummaryTicket_IsEmpty()
        {
            ////Arrange
            _ticket.TransDesc = string.Empty;

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.IsTrue(string.IsNullOrEmpty(_choice.Template.BetType.betTypeName));
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
            _ticket.BetCheck = "";
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
            _ticket.BetCheck = "";
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
            _ticket.BetCheck = "";
            _ticket.WinlostDate = DateTime.Today.AddDays(-2);

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            string expectedTemplate = BuildExpectedTemplateDetailLinkHtml(_ticket, "0");
            Assert.IsTrue(_choice.Template.League.ToString().Contains(expectedTemplate));
        }

        [Test]
        public void BuildLeague_FromBetCheck_LeagueNameIsBetTypeNameAndGameId()
        {
            // Arrange
            const string gameId = "12345";
            _ticket.BetCheck = "BetCheck";
            _ticket.MatchCode = gameId;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var expectedLeagueName = string.Format(
                "<span class='bet-type-name'>{0}</span>", BetTypeName);
            Assert.AreEqual(expectedLeagueName, _choice.Template.League.LeagueName.leagueName);
        }

        private string BuildExpectedTemplateDetailLinkHtml(ITicket ticket, string transactionType)
        {
            return string.Format(
                "<div class=\"detail live-casino-detail\"" +
                " onclick='showPragmaticLiveCasinoBetsDetails(&quot;{0}&quot;,&quot;{1}&quot;,&quot;{2}&quot;,&quot;{3}&quot;);'>" +
                "<a class=\"detail-link\"" +
                " href=\"javascript:void(&quot;&quot;);\">Details</a></div>",
                ticket.TransId.ToString(),
                ticket.WinlostDate.ToString(CultureInfo.InvariantCulture),
                ticket.CustId.ToString(),
                transactionType);
        }

        [Test]
        public void BuildLeague_ForLeagueName_ReturnBetTypeName()
        {
            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.IsTrue(_choice.Template.League.LeagueName.leagueName.Contains(BetTypeName));
        }

        [TestCase("")]
        [TestCase("Trandesc")]
        public void RenderRTF_ForLeagueName_RemoveHtmlTag(string trandesc)
        {
            ////Arrange
            _ticket.TransDesc = trandesc;
            var rtfHelper = new RTFHelper().Clone();

            ////Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);

            ////Assert
            Assert.IsFalse(_choice.Template.League.LeagueName.leagueName.Contains("</span>"));
        }

        [Test]
        public void RenderRTF_ForBetTypeNameOfDetailTicket_RemoveHtmlTag()
        {
            ////Arrange
            _ticket.BetCheck = "BetCheck";
            var rtfHelper = new RTFHelper().Clone();

            ////Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);

            ////Assert
            Assert.IsFalse(_choice.Template.BetType.betTypeName.Contains("</span>"));
        }

        [Test]
        public void RemoveHtmlTagFromLeagueName_FromBetCheck_LeagueNameIsBetTypeNameAndGameId()
        {
            // Arrange
            const string gameId = "12345";

            var rtfHelper = new RTFHelper()
            {
                RTFRenderer = new RtfTextRender()
            };
            _ticket.BetCheck = "BetCheck";
            _ticket.MatchCode = gameId;

            // Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);

            // Assert
            var expectedLeagueName = string.Format("{0}", BetTypeName);

            Assert.AreEqual(expectedLeagueName, _choice.Template.League.LeagueName.leagueName);
        }
    }
}