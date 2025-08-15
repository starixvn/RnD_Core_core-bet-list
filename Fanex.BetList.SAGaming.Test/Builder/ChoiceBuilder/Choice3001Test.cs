namespace Fanex.BetList.SAGaming.Tests.Builder.ChoiceBuilder
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
    public class Choice3001Test
    {
        private const string BetTypeName = "Baccarat";

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice3001();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.BetTypeId = SAGamingBetTypes.Baccarat;
            _ticket.TransId = 1;
            _ticket.CustId = 1000;

            _ticketHelper.GetBetTypeNameById(SAGamingBetTypes.Baccarat).Returns(BetTypeName);
        }

        [Test]
        public void BuildBetTeam_BetCheckHasDataOfBetChoice_HasNotChoiceId_ReturnBetChoiceName()
        {
            ////Arrange
            const string choiceName = "Player";
            var betTeamFormat = "<div><span class='favorite'>{0}</span>&nbsp;<span class='handicap custom'>(UUS {1})</span></div>";
            var expectedBetTeam = string.Format(betTeamFormat, choiceName, 2);
            _ticket.BetCheck = "[1,2]";
            _ticket.Currency = "UUS";
            _ticketHelper.GetResourceData("SA_BT", "1").Returns(choiceName);

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetCheckHasDataOfBetChoice_ReturnBetChoiceName()
        {
            ////Arrange
            const string choiceName = "Player";
            var betTeamFormat = "<div><span class='favorite'>{0}</span>&nbsp;<span class='handicap custom'>(UUS {1})</span><span class='bet-id'> - 123</span></div>";
            var expectedBetTeam = string.Format(betTeamFormat, choiceName, 2);
            _ticket.BetCheck = "[1,2]&123";
            _ticket.Currency = "UUS";
            _ticketHelper.GetResourceData("SA_BT", "1").Returns(choiceName);

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetCheckDoesNotHasDataOfBetChoice_ReturnEmpty()
        {
            ////Arrange

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.IsTrue(string.IsNullOrEmpty(_choice.Template.betTeam));
        }

        [Test]
        public void BuildBetTeam_BetCheckHasMultipDataOfBetChoice_HasNotChoiceId_ReturnMultipBetChoice()
        {
            ////Arrange
            const string choiceName = "Player";
            _ticket.BetCheck = "[1,2];[2,10]";
            _ticket.Currency = "UUS";
            var betTeamFormat = "<div><span class='favorite'>{0}</span>&nbsp;<span class='handicap custom'>(UUS {1})</span></div>";
            var expectedChoice1 = string.Format(betTeamFormat, choiceName, 2);
            var expectedChoice2 = string.Format(betTeamFormat, choiceName, 10);
            _ticketHelper.GetResourceData("SA_BT", Arg.Any<string>()).Returns(choiceName);

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.AreEqual(expectedChoice1+expectedChoice2, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetCheckHasMultipDataOfBetChoice_ReturnMultipBetChoice()
        {
            ////Arrange
            const string choiceName = "Player";
            _ticket.BetCheck = "[1,2];[2,10]&123;123";
            _ticket.Currency = "UUS";
            var betTeamFormat = "<div><span class='favorite'>{0}</span>&nbsp;<span class='handicap custom'>(UUS {1})</span><span class='bet-id'> - 123</span></div>";
            var expectedChoice1 = string.Format(betTeamFormat, choiceName, 2);
            var expectedChoice2 = string.Format(betTeamFormat, choiceName, 10);
            _ticketHelper.GetResourceData("SA_BT", Arg.Any<string>()).Returns(choiceName);

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.AreEqual(expectedChoice1 + expectedChoice2, _choice.Template.betTeam);
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
        public void BuildBetType_TransDescIsEmpty_ReturnEmpty()
        {
            ////Arrange
            _ticket.TransDesc = string.Empty;

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.IsEmpty(_choice.Template.BetType.betTypeName);
        }

        [Test]
        public void BuildBetType_TransDecsIsNotEmpty_ReturnBetId()
        {
            ////Arrange
            _ticket.TransDesc = "Transdesc";
            _ticket.BetTeam = "123";

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
            const string gameTableId = "R001";

            _ticket.TransDesc = $"choice=1001*comms=100*site=BEST-ODDS*TableCode={gameTableId}*state=0*result=B;S6;H2:P;D10;D5*results=B;8:P;5*winResult=1001;1004";
            _ticket.MatchCode = gameId;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var expectedLeagueName = string.Format(
                "<span class='bet-type-name'>{0}</span> <span class=\"gamegroupname\">{1}</span> <span class='game-id'>{2}</span>",
                BetTypeName,
                gameTableId,
                gameId);
            Assert.AreEqual(expectedLeagueName, _choice.Template.League.LeagueName.leagueName);
        }

        private string BuildExpectedTemplateDetailLinkHtml(ITicket ticket, string transactionType)
        {
            return string.Format(
                "<div class=\"detail live-casino-detail\"" +
                " onclick='showSAGamingDetails(&quot;{0}&quot;,&quot;{1}&quot;,&quot;{2}&quot;,&quot;{3}&quot;);'>" +
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
            const string gameTableId = "R001";

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