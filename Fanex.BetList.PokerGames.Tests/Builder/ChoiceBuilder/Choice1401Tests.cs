namespace Fanex.BetList.PokerGames.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using Core.Utils;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice1401Tests
    {
        private const string SeatResourceId = "Seat";
        private const string TableResourceId = "Table";
        private const string PerPointResourceId = "PerPoint";
        private const string PlayerIdResourceId = "PlayerID";

        private const string SeatResourceIdPattern = "Seat #{0}";
        private const string TableResourceIdPattern = "Table #{0}";
        private const string PerPointResourceIdPattern = "({0} PER POINT)";
        private const string TableMode = "Classic Mode (Mid-tier 2x)";
        private const string PlayerIdPattern = "PlayerID {0}";

        private const string TableNumber = "57236";
        private const string PlayerId = "168168";
        private const string BetTypeName = "8 Cards";

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1401();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.BetTypeId = 1401;
            _ticket.ExtraInfo = $"<p33 to=\"320\" tx=\"0\" is=\"0\" si=\"{PlayerId}\" tn=\"{TableNumber}\" fc=\"0\" rc=\"0\"/>";
            _ticket.Stake = 2000;
            _ticket.LiveHomeScore = 111;
            _ticket.Handicap1 = 1123.5M;
            _ticket.CommStatus = "Mode1";
            _ticket.CustId = 1000;
            _ticket.Currency = "THB";

            _ticketHelper.GetResourceData(Arg.Is(Choice1401.GameInfoRefId), Arg.Is(SeatResourceId)).Returns(SeatResourceIdPattern);
            _ticketHelper.GetResourceData(Arg.Is(Choice1401.GameInfoRefId), Arg.Is(TableResourceId)).Returns(TableResourceIdPattern);
            _ticketHelper.GetResourceData(Arg.Is(Choice1401.GameInfoRefId), Arg.Is(PerPointResourceId)).Returns(PerPointResourceIdPattern);
            _ticketHelper.GetResourceData(Arg.Is(Choice1401.GameModeRefId), Arg.Is(_ticket.CommStatus)).Returns(TableMode);
            _ticketHelper.GetResourceData(Arg.Is(Choice1401.GameInfoRefId), Arg.Is(PlayerIdResourceId)).Returns(PlayerIdPattern);
            _ticketHelper.GetResourceData(Arg.Is(Choice1401.GameTypeRefId), Arg.Is(_ticket.BetTypeId.ToString())).Returns(BetTypeName);
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
        public void BuildBetType_Always_CallRemotingRefToGetGameTypeName()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            _ticketHelper.Received().GetResourceData(Arg.Is(Choice1401.GameTypeRefId), Arg.Is(_ticket.BetTypeId.ToString()));
        }

        [Test]
        public void BuildBetType_Always_ShowGameTypeNameGotFromRemotingRefAsBetTypeName()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(BetTypeName, template.BetType.betTypeName);
        }

        [Test]
        public void BuildMatch_Always_CallRemotingRefToGetTextOfSeat()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            _ticketHelper.Received().GetResourceData(Arg.Is(Choice1401.GameInfoRefId), Arg.Is(SeatResourceId));
        }

        [Test]
        public void BuildMatch_Always_SeatNumberGotFromLiveHomeScore()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);
            var gameInfo = template.Match.ToString();

            // Assert.
            var expectedResult = string.Format(SeatResourceIdPattern, _ticket.LiveHomeScore);

            StringAssert.Contains(expectedResult, gameInfo);
        }

        [Test]
        public void BuildMatch_Always_CallRemotingRefToGetTextOfTable()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            _ticketHelper.Received().GetResourceData(Arg.Is(Choice1401.GameInfoRefId), Arg.Is(TableResourceId));
        }

        [Test]
        public void BuildMatch_Always_TableNumberStoredInExtraInfo()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);
            var gameInfo = template.Match.ToString();

            // Assert.
            var expectedResult = string.Format(TableResourceIdPattern, TableNumber);

            StringAssert.Contains(expectedResult, gameInfo);
        }

        [Test]
        public void BuildMatch_Always_CallRemotingRefToGetTextOfPerPoint()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            _ticketHelper.Received().GetResourceData(Arg.Is(Choice1401.GameInfoRefId), Arg.Is(PerPointResourceId));
        }

        [Test]
        public void BuildMatch_Always_PointStoredInHandicap1()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);
            var gameInfo = template.Match.ToString();

            // Assert.
            var formattedPoints = string.Format(Formatter.DECIMALFORMAT1, _ticket.Handicap1);
            var expectedResult = string.Format(PerPointResourceIdPattern, formattedPoints);

            StringAssert.Contains(expectedResult, gameInfo);
        }

        [Test]
        public void BuildMatch_Always_CallRemotingRefToGetTextOfMode()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            _ticketHelper.Received().GetResourceData(Arg.Is(Choice1401.GameModeRefId), Arg.Is(_ticket.CommStatus));
        }

        [Test]
        public void BuildMatch_Always_ShowTableModeReturnedFromRemotingRef()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);
            var gameInfo = template.Match.ToString();

            // Assert.
            StringAssert.Contains(TableMode, gameInfo);
        }

        [Test]
        public void BuildMatch_Always_CallRemotingRefToGetTextOfPalyerId()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            _ticketHelper.Received().GetResourceData(Arg.Is(Choice1401.GameInfoRefId), Arg.Is(PlayerIdResourceId));
        }

        [Test]
        public void BuildMatch_Always_PlayerIdStoredInExtraInfo()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);
            var gameInfo = template.Match.ToString();

            // Assert.
            var expectedResult = string.Format(PlayerIdPattern, PlayerId);

            StringAssert.Contains(expectedResult, gameInfo);
        }

        [Test]
        public void BuildMatch_Always_SeatTableModeAndPlayerIdAreInOrder()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);
            var gameInfo = template.Match.ToString();

            // Assert.
            var expectedSeat = string.Format(SeatResourceIdPattern, _ticket.LiveHomeScore);
            var expectedTable = string.Format(TableResourceIdPattern, TableNumber);
            var formattedPoints = string.Format(Formatter.DECIMALFORMAT1, _ticket.Handicap1);
            var expectedPerPoint = string.Format(PerPointResourceIdPattern, formattedPoints);
            var expectedPlayer = string.Format(PlayerIdPattern, PlayerId);
            var expectedResult = $"<div>{expectedSeat}, {expectedTable} {expectedPerPoint}</div><div>{TableMode}</div><div>{expectedPlayer}</div>";

            StringAssert.Contains(expectedResult, gameInfo);
        }

        [Test]
        public void BuildBetTypeName_Always_FirstLineIsBetTypeName()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            var expectedText = _choice.Template.BetType.betTypeName;

            Assert.AreEqual(expectedText, linesInActualText[0]);
        }

        [Test]
        public void BuildBetTeamsInfo_Always_SecondLineIsSeatAndTableInfo()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            var expectedSeat = string.Format(SeatResourceIdPattern, _ticket.LiveHomeScore);
            var expectedTable = string.Format(TableResourceIdPattern, TableNumber);
            var expectedPerPoint = string.Format(PerPointResourceIdPattern, string.Format(Formatter.DECIMALFORMAT1, _ticket.Handicap1));
            var expectedText = $"{expectedSeat}, {expectedTable} {expectedPerPoint}";

            Assert.AreEqual(expectedText, linesInActualText[1]);
        }

        [Test]
        public void BuildBetTeamsInfo_Always_ThirdLineIsTableMode()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            Assert.AreEqual(TableMode, linesInActualText[2]);
        }

        [Test]
        public void BuildBetTeamsInfo_Always_ForthLineIsPlayerInfo()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            var expectedPlayer = string.Format(PlayerIdPattern, PlayerId);

            Assert.AreEqual(expectedPlayer, linesInActualText[3]);
        }

        [Test]
        public void BuildLeague_Always_ReplaceLeagueNameByTransId3rdStoredInBetCheck()
        {
            // Arrange.
            _ticket.BetCheck = "BC123";

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(_ticket.BetCheck, template.League.LeagueName.leagueName);
        }
    }
}