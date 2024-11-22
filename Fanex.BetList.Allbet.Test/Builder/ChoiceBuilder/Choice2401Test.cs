namespace Fanex.BetList.Allbet.Tests.Builder.ChoiceBuilder
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
    public class Choice2401Test
    {
        private const string BetTypeName = "Baccarat";

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice2401();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.BetTypeId = AllbetBetTypes.Baccarat;
            _ticket.TransId = 1;
            _ticket.CustId = 1000;

            _ticketHelper.GetBetTypeNameById(AllbetBetTypes.Baccarat).Returns(BetTypeName);
        }

        [Test]
        public void BuildBetTeam_TransDecsHasDataOfBetChoice_ReturnBetChoiceName()
        {
            ////Arrange
            const string choiceName = "Player";

            _ticket.TransDesc = "choice=1001*comms=1*tableCode=B001*result=P;S6;H2:B;D10;D5 *results=B;8:P;5";
            _ticketHelper.GetResourceData("AllBet_BT", "1001").Returns(choiceName);

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.AreEqual(choiceName, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_TransDecsDoesNotHasDataOfBetChoice_ReturnEmpty()
        {
            ////Arrange
            _ticket.TransDesc = "comms=1*tableCode=B001*result=P;S6;H2:B;D10;D5 *results=B;8:P;5";

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.IsTrue(string.IsNullOrEmpty(_choice.Template.betTeam));
        }

        [Test]
        public void BuildBetType_TransDecsIsEmpty_ReturnEmpty()
        {
            ////Arrange
            _ticket.TransDesc = string.Empty;

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.IsTrue(string.IsNullOrEmpty(_choice.Template.BetType.betTypeName));
        }

        [Test]
        public void BuildBetType_TransDecsIsNotEmpty_ReturnBetId()
        {
            ////Arrange
            _ticket.TransDesc = "Transdesc";
            _ticket.BetCheck = "123";

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            var betIdMarkup = string.Format("<span class='bet-id'>{0}</span>", "BetID: " + "123");
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
            _ticket.TransDesc = string.Empty;
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
            _ticket.TransDesc = string.Empty;
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
            _ticket.TransDesc = string.Empty;
            _ticket.WinlostDate = DateTime.Today.AddDays(-2);

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            string expectedTemplate = BuildExpectedTemplateDetailLinkHtml(_ticket, "0");
            Assert.IsTrue(_choice.Template.League.ToString().Contains(expectedTemplate));
        }

        [Test]
        public void BuildLeague_FromSettledTicket_LeagueNameIsBetTypeNameAndGameTableIdAndGameId()
        {
            // Arrange
            const string gameId = "12345";
            const string gameTableId = "R001 ";

            _ticket.TransDesc = $"choice=1001*comms=100*site=BEST-ODDS*tablecode={gameTableId}*state=0*result=B;S6;H2:P;D10;D5*results=B;8:P;5*winResult=1001;1004";
            _ticket.MatchCode = gameId;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var expectedLeagueName = string.Format(
                "<span class='bet-type-name'>{0}</span> <span class=\"gamegroupname\"><span class=\"gamegroupname\">{1}</span></span> <span class='game-id'>{2}</span>",
                BetTypeName,
                gameTableId,
                gameId);
            Assert.AreEqual(expectedLeagueName, _choice.Template.League.LeagueName.leagueName);
        }

        private string BuildExpectedTemplateDetailLinkHtml(ITicket ticket, string transactionType)
        {
            return string.Format(
                "<div class=\"detail live-casino-detail\"" +
                " onclick='showTodayRunningBetsDetails(&quot;{0}&quot;,&quot;{1}&quot;,&quot;{2}&quot;,&quot;{3}&quot;);'>" +
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
            _ticket.TransDesc = "transdesc";
            var rtfHelper = new RTFHelper().Clone();

            ////Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);

            ////Assert
            Assert.IsFalse(_choice.Template.BetType.betTypeName.Contains("</span>"));
        }

        [Test]
        public void RemoveHtmlTagFromLeagueName_FromSettledTicket_LeagueNameIsBetTypeNameAndGameTableIdAndGameId()
        {
            // Arrange
            const string gameId = "12345";
            const string gameTableId = "R001 ";

            var rtfHelper = new RTFHelper()
            {
                RTFRenderer = new RtfTextRender()
            };

            _ticket.TransDesc = $"choice=1001*comms=100*site=BEST-ODDS*tableCode={gameTableId}*state=0*result=B;S6;H2:P;D10;D5*results=B;8:P;5*winResult=1001;1004";
            _ticket.MatchCode = gameId;

            // Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);

            // Assert
            var expectedLeagueName = string.Format("{0} {1} {2}", BetTypeName, gameTableId, gameId);

            Assert.AreEqual(expectedLeagueName, _choice.Template.League.LeagueName.leagueName);
        }
    }
}