namespace Fanex.BetList.CardClub.Tests.Builder.ChoiceBuilder
{
    using System;
    using System.Globalization;
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice1901Tests
    {
        private const string TableResourceId = "Table";
        private const string PointResourceId = "PerPoint";

        private const string TableResourceIdPattern = "Table #{0}";
        private const string PointResourceIdPattern = "({0} Per Point)";
        private const string GameTypeName = "Video Poker";
        private const string GameGroupName = "Table Games";
        private const string SportTypeName = "Card Club";

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1901();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.BetTypeId = 1901;
            _ticket.Stake = 2000;
            _ticket.CustId = 1000;
            _ticket.Currency = "THB";
            _ticket.TransDesc = "1901";
            _ticket.SportTypeId = 165;
            _ticket.MatchCode = "ForcedCompleted=0*ReasonCode=0*RemainingAmount=0*" +
                "BuyAmount=0*ValidBetAmount=0*JPType=0*HandNumber=12345*TableNumber=9985" +
                "*Point=10*PlayerID=168168*DoubleUpRate=0.2*DoubleUpOdds=0.5";

            _ticketHelper.GetResourceData(Arg.Is(Choice1901.GameTypeRefId), Arg.Is(_ticket.SportTypeId.ToString())).Returns(SportTypeName);
            _ticketHelper.GetResourceData(Arg.Is(Choice1901.GameInfoRefId), Arg.Is(TableResourceId)).Returns(TableResourceIdPattern);
            _ticketHelper.GetResourceData(Arg.Is(Choice1901.GameInfoRefId), Arg.Is(PointResourceId)).Returns(PointResourceIdPattern);
            _ticketHelper.GetResourceData(Arg.Is(Choice1901.GameTypeRefId), Arg.Is(_ticket.BetTypeId.ToString())).Returns(GameTypeName);
            _ticketHelper.GetResourceData(Arg.Is(Choice1901.GameGroupRefId), Arg.Is(_ticket.TransDesc)).Returns(GameGroupName);
        }

        [Test]
        public void BuildBetTeam_IsSummaryTicket_NotContainBetcheck()
        {
            // Arrange
            _ticket.BetCheck = string.Empty;
            var expectedBetTeam = $"<span class=\"handicap\">{GameTypeName}</span>";

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(expectedBetTeam, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_IsDetailTicket_ContainBetcheck()
        {
            // Arrange
            _ticket.BetCheck = "123456";
            var expectedBetTeam = $"<span class=\"handicap\">{GameTypeName}</span> {_ticket.BetCheck}";

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(expectedBetTeam, template.betTeam);
        }

        [Test]
        public void BuildBetType_Alway_Null()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(_choice.Template.BetType);
        }

        [Test]
        public void BuildScore_Alway_Null()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(_choice.Template.Score);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Alway_NullAndUnderdogClass()
        {
            // Arrange
            var expectedClass = "underdog";

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(_choice.Template.Handicap.handicap);
            Assert.AreEqual(expectedClass,_choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildMatch_IsSummaryTicket_SetNullMatch()
        {
            // Arrange
            _ticket.BetCheck = string.Empty;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.Null(template.Match);
        }

        [Test]
        public void BuildMatch_IsDetailTicket_GetGameInfoFromMatchCode()
        {
            // Arrange
            _ticket.BetCheck = "12312432";
            var expectedMatch = "<div class=\"game-info\"><div>Table #9985 (10 Per Point)</div></div>";
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(expectedMatch, template.Match.ToString());
        }

        [Test]
        public void BuildSport_Always_GetFromResourceData()
        {
            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(SportTypeName, template.League.sportTypeName);
        }

        [Test]
        public void BuildLeague_IsSummarTicket_ShowGameGroupName()
        {
            // Arrange
            _ticket.BetCheck = string.Empty;
            var expectedGameGroupName = $"<span class='gamegroupname'> {GameGroupName}</span>";

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsTrue(template.League.ToString().Contains(expectedGameGroupName));
        }

        [Test]
        public void BuildLeague_IsSummarTicket_ShowJSFunction()
        {
            // Arrange
            _ticket.BetCheck = string.Empty;
            var expectedJSFunction = "showVideoPokerGameDetails";

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsTrue(template.League.ToString().Contains(expectedJSFunction));
        }

        [TestCase("WON", "1")]
        [TestCase("LOSE", "1")]
        [TestCase("DRAW", "1")]
        [TestCase("REFUND", "1")]
        [TestCase("REJECT", "2")]
        [TestCase("VOID", "2")]
        [TestCase("RUNNING", "3")]
        [TestCase("WAITING", "3")]
        public void BuildLeague_IsSummaryTicket_ShowDetailLinkJsFunction(string ticketStatus, string transctionType)
        {
            ////Arrange
            _ticket.BetCheck = string.Empty;
            _ticket.TransId = 123;
            _ticket.CustId = 456;
            _ticket.Status = ticketStatus;
            _ticket.WinlostDate = DateTime.Today;

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            string expectedJsFunction = string.Format(
                "showVideoPokerGameDetails(123, '{0}', 456, {1});",
                _ticket.WinlostDate.ToString(CultureInfo.InvariantCulture),
                transctionType);
            string expectedTemplate = $"<div class=\"detail video-poker-game-detail\" onclick=\"{expectedJsFunction}\">" +
                 "<a style=\"color:#755200;font-weight:bold\" href=\"javascript:void('');\">Details</a></div>";

            Assert.IsTrue(_choice.Template.League.ToString().Contains(expectedTemplate));
        }

        [TestCase("WON")]
        [TestCase("LOSE")]
        [TestCase("DRAW")]
        [TestCase("REFUND")]
        [TestCase("REJECT")]
        [TestCase("VOID")]
        [TestCase("RUNNING")]
        [TestCase("WAITING")]
        public void BuildLeague_IsPreviousDaySummaryTicket_ShowDetailLinkJsFunction(string ticketStatus)
        {
            ////Arrange
            _ticket.BetCheck = string.Empty;
            _ticket.TransId = 123;
            _ticket.CustId = 456;
            _ticket.Status = ticketStatus;
            _ticket.WinlostDate = DateTime.Today.AddDays(-2);

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            string expectedJsFunction = string.Format(
                "showVideoPokerGameDetails(123, '{0}', 456, {1});",
                _ticket.WinlostDate.ToString(CultureInfo.InvariantCulture),
                0);
            Assert.IsTrue(_choice.Template.League.ToString().Contains(expectedJsFunction));
        }

        [Test]
        public void BuildLeague_IsDetailTicket_NotShowJsFunction()
        {
            // Arrange.
            _ticket.BetCheck = "BC123";
            var jsFunctionName = "showVideoPokerGameDetails";

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsFalse(template.League.LeagueName.leagueName.Contains(jsFunctionName));
        }

        [Test]
        public void BuildBetTeamInfo_IsDetailTicket_RenderForExcel()
        {
            // Arrange.
            _ticket.BetCheck = "BC123";
            var rtfHelper = new RTFHelper().Clone();

            // Act
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert
            Assert.AreEqual($"{GameTypeName} {_ticket.BetCheck}", linesInActualText[0]);
            Assert.AreEqual("Table #9985 (10 Per Point)", linesInActualText[1]);
            Assert.AreEqual($"{SportTypeName} {GameGroupName}", linesInActualText[2]);
        }

        [Test]
        public void BuildBetTeamInfo_IsSummaryTicket_RenderForExcel()
        {
            // Arrange.
            _ticket.BetCheck = string.Empty;
            var rtfHelper = new RTFHelper().Clone();

            // Act
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert
            Assert.AreEqual($"{GameTypeName}", linesInActualText[0]);
            Assert.AreEqual($"{SportTypeName} {GameGroupName}", linesInActualText[1]);
        }
    }
}