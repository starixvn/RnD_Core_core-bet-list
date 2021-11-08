namespace Fanex.BetList.BG.Tests.Builder.ChoiceBuilder
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
    public class Choice3501Test
    {
        private const string BetTypeName = "Baccarat";

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice3501();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.BetTypeId = BGBetTypes.Baccarat;
            _ticket.TransId = 1;
            _ticket.CustId = 1000;

            _ticketHelper.GetBetTypeNameById(BGBetTypes.Baccarat).Returns(BetTypeName);
        }

        [TestCase("choice=268435457_0_0*comms=0*site=*tableCode=*state=*result=*results=*winResult=*ForcedCompleted=*ReasonCode=*enterType=")]
        [TestCase("Choice=268435457_0_0*comms=0*site=*tableCode=*state=*result=*results=*winResult=*ForcedCompleted=*ReasonCode=*enterType=")]
        [TestCase("CHOICE=268435457_0_0*comms=0*site=*tableCode=*state=*result=*results=*winResult=*ForcedCompleted=*ReasonCode=*enterType=")]
        [TestCase("choice=268435457_0_0")]
        public void BuildBetTeam_WrongData_ReturnBetChoiceName(string transdesc)
        {
            ////Arrange
            const string choiceName = "Player";
            var expectedBetTeam = choiceName;
            expectedBetTeam += string.Format("<span class='bet-id'>{0}</span>", "BetID: " + "123");
            _ticket.TransDesc = transdesc;
            _ticket.BetCheck = "123";
            _ticketHelper.GetResourceData("BG_BT", "268435457_0_0").Returns(choiceName);

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_SingleChoice_ReturnOneBetChoiceName()
        {
            ////Arrange
            const string choiceName = "Player";
            var expectedBetTeam = choiceName;
            expectedBetTeam += string.Format("<span class='bet-id'>{0}</span>", "BetID: " + "123");
            _ticket.TransDesc = "choice=268435457_0_0*comms=0*site=*tableCode=*state=*result=*results=*winResult=*ForcedCompleted=*ReasonCode=*enterType=";
            _ticket.BetCheck = "123";
            _ticketHelper.GetResourceData("BG_BT", "268435457_0_0").Returns(choiceName);

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
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
        public void BuildBetType_Always_Hide()
        {
            ////Arrange

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.IsFalse(_choice.Template.BetType.Visible);
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
        public void BuildLeague_FromSettledTicket_LeagueNameIsBetTypeNameAndGameId()
        {
            // Arrange
            const string gameId = "12345";
            _ticket.TransDesc = "transdesc";
            _ticket.MatchCode = gameId;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var expectedLeagueName = string.Format(
                "<span class='bet-type-name'>{0}</span> <span class='game-id'>{1}</span>",
                BetTypeName,
                gameId);
            Assert.AreEqual(expectedLeagueName, _choice.Template.League.LeagueName.leagueName);
        }

        private string BuildExpectedTemplateDetailLinkHtml(ITicket ticket, string transactionType)
        {
            return string.Format(
                "<div class=\"detail bg-casino-detail\"" +
                " onclick='showBGBetDetails(&quot;{0}&quot;,&quot;{1}&quot;,&quot;{2}&quot;,&quot;{3}&quot;);'>" +
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
        public void RemoveHtmlTagFromLeagueName_FromSettledTicket_LeagueNameIsBetTypeNameAndGameId()
        {
            // Arrange
            const string gameId = "12345";

            var rtfHelper = new RTFHelper()
            {
                RTFRenderer = new RtfTextRender()
            };
            _ticket.TransDesc = "transdesc";
            _ticket.MatchCode = gameId;

            // Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);

            // Assert
            var expectedLeagueName = string.Format("{0} {1}", BetTypeName, gameId);

            Assert.AreEqual(expectedLeagueName, _choice.Template.League.LeagueName.leagueName);
        }
    }
}