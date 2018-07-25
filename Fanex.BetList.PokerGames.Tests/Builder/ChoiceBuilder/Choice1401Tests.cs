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
        private const string BuyInResourceId = "BuyIn";
        private const string RemainingResourceId = "Remaining";

        private const string SeatResourceIdPattern = "Seat #{0}";
        private const string TableResourceIdPattern = "Table #{0}";
        private const string PerPointResourceIdPattern = "({0} PER POINT)";
        private const string TableMode = "Classic Mode (Mid-tier 2x)";
        private const string PlayerIdPattern = "PlayerID {0}";
        private const string BuyInResourcePattern = "Buy In {0}";
        private const string RemainingPattern = "Remaining {0}";

        private const string TableNumber = "57236";
        private const decimal RemaningAmount = 620.55M;
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
            _ticket.ExtraInfo = $"<p33 to=\"320\" tx=\"0\" is=\"0\" ra=\"{RemaningAmount}\" tn=\"{TableNumber}\" fc=\"0\" rc=\"0\"/>";
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
            _ticketHelper.GetResourceData(Arg.Is(Choice1401.GameInfoRefId), Arg.Is(BuyInResourceId)).Returns(BuyInResourcePattern);
            _ticketHelper.GetResourceData(Arg.Is(Choice1401.GameInfoRefId), Arg.Is(RemainingResourceId)).Returns(RemainingPattern);
            _ticketHelper.GetResourceData(Arg.Is(Choice1401.GameTypeRefId), Arg.Is(_ticket.BetTypeId.ToString())).Returns(BetTypeName);
        }

        [Test]
        public void BuildBetTeam_Always_CallRemotingRefToGetTextOfBuyIn()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            _ticketHelper.Received().GetResourceData(Arg.Is(Choice1401.GameInfoRefId), Arg.Is(BuyInResourceId));
        }

        [Test]
        public void BuildBetTeam_Always_BuyInAmountIsCalculatedFromStakeAndRemainingAmount()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedBuyInAmount = ConvertByBetType.Stake(_ticket.Stake - RemaningAmount);

            StringAssert.Contains(expectedBuyInAmount, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_Always_BuyInAmountInfoContainsCurrency()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedBuyInAmount = _ticket.Stake - RemaningAmount;
            var expectedBuyInWithCurrency = $"{_ticket.Currency} {ConvertByBetType.Stake(expectedBuyInAmount)}";

            StringAssert.Contains(expectedBuyInWithCurrency, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_Always_BuyInAmountInfoIsPlacedInCorrectHtml()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedBuyInAmount = _ticket.Stake - RemaningAmount;
            var expectedBuyInWithCurrency = $"<span class=\"underdog\">{_ticket.Currency} {ConvertByBetType.Stake(expectedBuyInAmount)}</span>";
            var expectedResult = $"<div class=\"dark-orange\">{string.Format(BuyInResourcePattern, expectedBuyInWithCurrency)}</div>";

            StringAssert.StartsWith(expectedResult, template.betTeam);
        }

        [TestCase("<p33 to=\"320\" tx=\"0\" is=\"0\" ra=\"\" />")]
        [TestCase("<p33 to=\"320\" tx=\"0\" is=\"0\" ra=\"0\" />")]
        [TestCase("<p33 to=\"320\" tx=\"0\" is=\"0\" />")]
        public void BuildBetTeam_RemainingAmountDoesNotExist_DoNoCallRemotingRefToGetTextOfRemaining(string extraInfo)
        {
            // Arrange.
            _ticket.ExtraInfo = extraInfo;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            _ticketHelper.DidNotReceive().GetResourceData(Arg.Is(Choice1401.GameInfoRefId), Arg.Is(RemainingResourceId));
        }

        [TestCase("<p33 to=\"320\" tx=\"0\" is=\"0\" ra=\"\" />")]
        [TestCase("<p33 to=\"320\" tx=\"0\" is=\"0\" ra=\"0\" />")]
        [TestCase("<p33 to=\"320\" tx=\"0\" is=\"0\" />")]
        public void BuildBetTeam_RemainingAmountDoesNotExist_OnlyShowBuyInAmount(string extraInfo)
        {
            // Arrange.
            _ticket.ExtraInfo = extraInfo;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedBuyInWithCurrency = $"<span class=\"underdog\">{_ticket.Currency} {ConvertByBetType.Stake(_ticket.Stake)}</span>";
            var expectedResult = $"<div class=\"dark-orange\">{string.Format(BuyInResourcePattern, expectedBuyInWithCurrency)}</div>";

            Assert.AreEqual(expectedResult, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_RemainingAmountIsPositiveNumber_CallRemotingRefToGetTextOfRemaining()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            _ticketHelper.Received().GetResourceData(Arg.Is(Choice1401.GameInfoRefId), Arg.Is(RemainingResourceId));
        }

        [Test]
        public void BuildBetTeam_RemainingAmountIsPositiveNumber_RemainingAmountGotFromExtraInfo()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            StringAssert.Contains(ConvertByBetType.Stake(RemaningAmount), template.betTeam);
        }

        [Test]
        public void BuildBetTeam_RemainingAmountIsPositiveNumber_RemainingAmountInfoContainsCurrency()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedBuyInWithCurrency = $"{_ticket.Currency} {ConvertByBetType.Stake(RemaningAmount)}";

            StringAssert.Contains(expectedBuyInWithCurrency, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_RemainingAmountIsPositiveNumber_RemainingAmountInfoIsPlacedInCorrectHtml()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedRemainingWithCurrency = $"<span class=\"underdog\">{_ticket.Currency} {ConvertByBetType.Stake(RemaningAmount)}</span>";
            var expectedResult = $"<div class=\"dark-orange\">{string.Format(RemainingPattern, expectedRemainingWithCurrency)}</div>";

            StringAssert.EndsWith(expectedResult, template.betTeam);
        }

        [Test]
        public void BuildBetType_RemainingAmountIsPositiveNumber_CallRemotingRefToGetGameTypeName()
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
        public void BuildMatch_Always_PlayerIdIsTheCombinationOfCPAndCustId()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);
            var gameInfo = template.Match.ToString();

            // Assert.
            var expectedResult = string.Format(PlayerIdPattern, $"CP{_ticket.CustId}");

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
            var expectedPlayer = string.Format(PlayerIdPattern, $"CP{_ticket.CustId}");
            var expectedResult = $"<div>{expectedSeat}, {expectedTable} {expectedPerPoint}</div><div>{TableMode}</div><div>{expectedPlayer}</div>";

            StringAssert.Contains(expectedResult, gameInfo);
        }

        [Test]
        public void BuildBetFigure_Always_FirstLineIsBuyInAmount()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            var expectedBuyInAmount = _ticket.Stake - RemaningAmount;
            var expectedBuyInWithCurrency = $"{_ticket.Currency} {ConvertByBetType.Stake(expectedBuyInAmount)}";
            var expectedText = string.Format(BuyInResourcePattern, expectedBuyInWithCurrency);

            Assert.AreEqual(expectedText, linesInActualText[0]);
        }

        [Test]
        public void BuildBetFigure_TicketWithRemaingAmount_SecondLineIsRemainingAmount()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            var expectedRemainingWithCurrency = $"{_ticket.Currency} {ConvertByBetType.Stake(RemaningAmount)}";
            var expectedText = string.Format(RemainingPattern, expectedRemainingWithCurrency);

            Assert.AreEqual(expectedText, linesInActualText[1]);
        }

        [Test]
        public void BuildBetTeamsInfo_TicketWithRemaingAmount_ForthLineIsSeatAndTableInfo()
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

            Assert.AreEqual(expectedText, linesInActualText[3]);
        }

        [Test]
        public void BuildBetTeamsInfo_TicketWithRemaingAmount_FifthLineIsSTableMode()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            Assert.AreEqual(TableMode, linesInActualText[4]);
        }

        [Test]
        public void BuildBetTeamsInfo_TicketWithRemaingAmount_SixthLineIsPlayerInfo()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            var expectedPlayer = string.Format(PlayerIdPattern, $"CP{_ticket.CustId}");

            Assert.AreEqual(expectedPlayer, linesInActualText[5]);
        }

        [Test]
        public void BuildBetTeamsInfo_TicketWithoutRemaingAmount_ThirdLineIsSeatAndTableInfo()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            _ticket.ExtraInfo = $"<p33 to=\"320\" tx=\"0\" is=\"0\" ra=\"0\" tn=\"{TableNumber}\" fc=\"0\" rc=\"0\"/>";

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            var expectedSeat = string.Format(SeatResourceIdPattern, _ticket.LiveHomeScore);
            var expectedTable = string.Format(TableResourceIdPattern, TableNumber);
            var expectedPerPoint = string.Format(PerPointResourceIdPattern, string.Format(Formatter.DECIMALFORMAT1, _ticket.Handicap1));
            var expectedText = $"{expectedSeat}, {expectedTable} {expectedPerPoint}";

            Assert.AreEqual(expectedText, linesInActualText[2]);
        }

        [Test]
        public void BuildBetTeamsInfo_TicketWithoutRemaingAmount_ForthLineIsSTableMode()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            _ticket.ExtraInfo = $"<p33 to=\"320\" tx=\"0\" is=\"0\" ra=\"0\" tn=\"{TableNumber}\" fc=\"0\" rc=\"0\"/>";

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            Assert.AreEqual(TableMode, linesInActualText[3]);
        }

        [Test]
        public void BuildBetTeamsInfo_TicketWithoutRemaingAmount_FifthLineIsPlayerInfo()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            _ticket.ExtraInfo = $"<p33 to=\"320\" tx=\"0\" is=\"0\" ra=\"0\" tn=\"{TableNumber}\" fc=\"0\" rc=\"0\"/>";

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            var expectedPlayer = string.Format(PlayerIdPattern, $"CP{_ticket.CustId}");

            Assert.AreEqual(expectedPlayer, linesInActualText[4]);
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