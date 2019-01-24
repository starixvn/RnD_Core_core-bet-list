namespace Fanex.BetList.PokerGames.Tests.Builder.ChoiceBuilder
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
        private const string PointResourceId = "Point";
        private const string PlayerIdResourceId = "PlayerID";

        private const string TableResourceIdPattern = "Table #{0}";
        private const string PointResourceIdPattern = "({0} Points)";
        private const string PlayerIdPattern = "PlayerID {0}";
        private const string GameTypeName = "8 Cards";
        private const string SportTypeName = "Video Poker";

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
            _ticket.LiveHomeScore = 111;
            _ticket.Handicap1 = 1123.5M;
            _ticket.CustId = 1000;
            _ticket.Currency = "THB";
            _ticket.TransDesc = "1401";
            _ticket.SportTypeId = 165;
            _ticket.MatchCode = "ForcedCompleted=0*ReasonCode=0*RemainingAmount=0*" +
                "BuyAmount=0*ValidBetAmount=0*JPType=0*HandNumber=12345*TableNumber=9985" +
                "*Point=10*PlayerID=168168*DoubleUpRate=0.2*DoubleUpOdds=0.5";

            _ticketHelper.GetResourceData(Arg.Is(Choice1401.GameTypeRefId), Arg.Is(_ticket.SportTypeId.ToString())).Returns(SportTypeName);
            _ticketHelper.GetResourceData(Arg.Is(Choice1401.GameInfoRefId), Arg.Is(TableResourceId)).Returns(TableResourceIdPattern);
            _ticketHelper.GetResourceData(Arg.Is(Choice1401.GameInfoRefId), Arg.Is(PointResourceId)).Returns(PointResourceIdPattern);
            _ticketHelper.GetResourceData(Arg.Is(Choice1401.GameInfoRefId), Arg.Is(PlayerIdResourceId)).Returns(PlayerIdPattern);
            _ticketHelper.GetResourceData(Arg.Is(Choice1401.GameTypeRefId), Arg.Is(_ticket.TransDesc)).Returns(GameTypeName);
        }

        [Test]
        public void BuildBetTeam_Always_BetTeamIsEmptyString()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsEmpty(template.betTeam);
        }

        [Test]
        public void BuildBetType_IsSummaryTicket_NotShow()
        {
            // Arrange
            _ticket.BetCheck = string.Empty;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsEmpty(_choice.Template.BetType.betTypeName);
        }

        [Test]
        public void BuildBetType_IsDetailTicket_ShowGameTypeNameGotFromRemotingRefAsBetTypeName()
        {
            // Arrange
            _ticket.BetCheck = "12312432";

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(GameTypeName, template.BetType.betTypeName);
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

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(template.Match.ToString(), "<div class=\"game-info\"><div>Table #9985 (10 Points)</div><div>PlayerID 168168</div></div>");
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
        public void BuildLeague_IsSummaryTicket_ShowLeagueNameAsBetTypeName()
        {
            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(GameTypeName, template.League.LeagueName.leagueName);
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
        public void BuildLeague_IsDetailTicket_ReplaceLeagueNameByTransId3rdStoredInBetCheck()
        {
            // Arrange.
            _ticket.BetCheck = "BC123";

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(_ticket.BetCheck, template.League.LeagueName.leagueName);
        }

        [Test]
        public void BuildBetTeamInfo_MatchIsNotNull_RenderForExcel()
        {
            // Arrange.
            _ticket.BetCheck = "BC123";
            var rtfHelper = new RTFHelper().Clone();

            // Act
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert
            Assert.AreEqual(GameTypeName, linesInActualText[1]);
            Assert.AreEqual("Table #9985 (10 Points)", linesInActualText[2]);
            Assert.AreEqual("PlayerID 168168", linesInActualText[3]);
            Assert.AreEqual($"{SportTypeName} {_ticket.BetCheck}", linesInActualText[4]);
        }

        [Test]
        public void BuildBetTeamInfo_MatchIsNull_RenderForExcel()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            // Act
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert
            Assert.AreEqual($"{SportTypeName} {GameTypeName}", linesInActualText[1]);
        }
    }
}