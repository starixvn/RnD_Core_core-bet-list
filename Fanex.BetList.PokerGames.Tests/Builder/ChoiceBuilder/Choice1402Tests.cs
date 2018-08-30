namespace Fanex.BetList.PokerGames.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using Fanex.BetList.Core.Utils;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice1402Tests
    {
        private const string SeatResourceId = "Seat";
        private const string TableResourceId = "Table";
        private const string PlayerIdResourceId = "PlayerID";

        private const string SeatResourceIdPattern = "Seat #{0}";
        private const string TableResourceIdPattern = "Table #{0}";
        private const string PlayerIdPattern = "PlayerID {0}";

        private const string TableNumber = "57236";
        private const string PlayerId = "168168";
        private const string BetTypeName = "8 Cards";

        private const string Mode15 = "Mode15";
        private const string Mode15GameInfoPattern = "( {0}/{1} Blinds )";
        private const string Mode15Name = "SB/BB Mode";
        private const string Mode16 = "Mode16";
        private const string Mode16GameInfoPattern = "{0} Ante + {1} Ante(Btn)";
        private const string Mode16Name = "Ante +  Ante(Btn) Mode";
        private const string Mode17 = "Mode17";
        private const string Mode17GameInfoPattern = "{0} Ante Only";
        private const string Mode17Name = "Ante Only Mode";
        private const string Mode18 = "Mode18";
        private const string Mode18Name = "SB/BB + Ante Mode";
        private const string Mode18GameInfoPattern = "{0}/{1} Blinds + {2} Ante";

        private const decimal AnteAmount = 100;

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1402();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.BetTypeId = 1401;
            _ticket.ExtraInfo = $"<p33 to=\"320\" tx=\"0\" is=\"0\" si=\"{PlayerId}\" tn=\"{TableNumber}\" at=\"{AnteAmount}\" fc=\"0\" rc=\"0\"/>";
            _ticket.Stake = 2000;
            _ticket.LiveHomeScore = 111;
            _ticket.Handicap1 = 1123.5M;
            _ticket.CommStatus = Mode15;
            _ticket.CustId = 1000;
            _ticket.Currency = "THB";

            _ticketHelper.GetResourceData(Arg.Is(Choice1402.GameInfoRefId), Arg.Is(SeatResourceId)).Returns(SeatResourceIdPattern);
            _ticketHelper.GetResourceData(Arg.Is(Choice1402.GameInfoRefId), Arg.Is(TableResourceId)).Returns(TableResourceIdPattern);
            _ticketHelper.GetResourceData(Arg.Is(Choice1402.GameInfoRefId), Arg.Is(PlayerIdResourceId)).Returns(PlayerIdPattern);
            _ticketHelper.GetResourceData(Arg.Is(Choice1402.GameTypeRefId), Arg.Is(_ticket.BetTypeId.ToString())).Returns(BetTypeName);

            _ticketHelper.GetResourceData(Arg.Is(Choice1402.GameInfoRefId), Arg.Is(Mode15)).Returns(Mode15GameInfoPattern);
            _ticketHelper.GetResourceData(Arg.Is(Choice1402.GameModeRefId), Arg.Is(Mode15)).Returns(Mode15Name);
            _ticketHelper.GetResourceData(Arg.Is(Choice1402.GameInfoRefId), Arg.Is(Mode16)).Returns(Mode16GameInfoPattern);
            _ticketHelper.GetResourceData(Arg.Is(Choice1402.GameModeRefId), Arg.Is(Mode16)).Returns(Mode16Name);
            _ticketHelper.GetResourceData(Arg.Is(Choice1402.GameInfoRefId), Arg.Is(Mode17)).Returns(Mode17GameInfoPattern);
            _ticketHelper.GetResourceData(Arg.Is(Choice1402.GameModeRefId), Arg.Is(Mode17)).Returns(Mode17Name);
            _ticketHelper.GetResourceData(Arg.Is(Choice1402.GameInfoRefId), Arg.Is(Mode18)).Returns(Mode18GameInfoPattern);
            _ticketHelper.GetResourceData(Arg.Is(Choice1402.GameModeRefId), Arg.Is(Mode18)).Returns(Mode18Name);
        }

        [Test]
        public void BuildMatch_Always_CallRemotingRefToGetTextOfGameInfoPattern()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            _ticketHelper.Received().GetResourceData(Arg.Is(Choice1402.GameInfoRefId), Arg.Is(_ticket.CommStatus));
        }

        [Test]
        public void BuildMatch_Mode15_SmallBlindStoredInHandicap1()
        {
            // Arrange.
            _ticket.CommStatus = Mode15;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);
            var gameInfo = template.Match.ToString();

            // Assert.
            var formattedSmallBlind = string.Format(Formatter.DECIMALFORMAT1, _ticket.Handicap1);

            StringAssert.Contains(formattedSmallBlind, gameInfo);
        }

        [Test]
        public void BuildMatch_Mode15_BigBlindIsDoubleOfHandicap1()
        {
            // Arrange.
            _ticket.CommStatus = Mode15;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);
            var gameInfo = template.Match.ToString();

            // Assert.
            var formattedBigBlind = string.Format(Formatter.DECIMALFORMAT1, _ticket.Handicap1 * 2);

            StringAssert.Contains(formattedBigBlind, gameInfo);
        }

        [Test]
        public void BuildBetTeamsInfo_Mode15_SecondLineIsSeatTableAndBlindInfo()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            _ticket.CommStatus = Mode15;

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            var expectedSeat = string.Format(SeatResourceIdPattern, _ticket.LiveHomeScore);
            var expectedTable = string.Format(TableResourceIdPattern, TableNumber);
            var expectedGameInfo = string.Format(
                Mode15GameInfoPattern,
                string.Format(Formatter.DECIMALFORMAT1, _ticket.Handicap1),
                string.Format(Formatter.DECIMALFORMAT1, _ticket.Handicap1 * 2));
            var expectedText = $"{expectedSeat}, {expectedTable} {expectedGameInfo}";

            Assert.AreEqual(expectedText, linesInActualText[1]);
        }

        [Test]
        public void BuildMatch_Mode16_AnteAmountStoredInHandicap1()
        {
            // Arrange.
            _ticket.CommStatus = Mode16;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);
            var gameInfo = template.Match.ToString();

            // Assert.
            var formattedAnteAmount = string.Format(Formatter.DECIMALFORMAT1, _ticket.Handicap1);

            StringAssert.Contains(formattedAnteAmount, gameInfo);
        }

        [Test]
        public void BuildMatch_Mode16_BtnAnteAmountStoredInExtraInfo()
        {
            // Arrange.
            _ticket.CommStatus = Mode16;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);
            var gameInfo = template.Match.ToString();

            // Assert.
            var formattedBtnAnteAmount = string.Format(Formatter.DECIMALFORMAT1, AnteAmount);

            StringAssert.Contains(formattedBtnAnteAmount, gameInfo);
        }

        [Test]
        public void BuildBetTeamsInfo_Mode16_SecondLineIsSeatTableAndDoubleAnteInfo()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            _ticket.CommStatus = Mode16;

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            var expectedSeat = string.Format(SeatResourceIdPattern, _ticket.LiveHomeScore);
            var expectedTable = string.Format(TableResourceIdPattern, TableNumber);
            var expectedGameInfo = string.Format(
                Mode16GameInfoPattern,
                string.Format(Formatter.DECIMALFORMAT1, _ticket.Handicap1),
                string.Format(Formatter.DECIMALFORMAT1, AnteAmount));
            var expectedText = $"{expectedSeat}, {expectedTable} {expectedGameInfo}";

            Assert.AreEqual(expectedText, linesInActualText[1]);
        }

        [Test]
        public void BuildMatch_Mode17_AnteAmountStoredInHandicap1()
        {
            // Arrange.
            _ticket.CommStatus = Mode17;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);
            var gameInfo = template.Match.ToString();

            // Assert.
            var formattedAnteAmount = string.Format(Formatter.DECIMALFORMAT1, _ticket.Handicap1);

            StringAssert.Contains(formattedAnteAmount, gameInfo);
        }

        [Test]
        public void BuildBetTeamsInfo_Mode17_SecondLineIsSeatTableAndAnteOnlyInfo()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            _ticket.CommStatus = Mode17;

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            var expectedSeat = string.Format(SeatResourceIdPattern, _ticket.LiveHomeScore);
            var expectedTable = string.Format(TableResourceIdPattern, TableNumber);
            var expectedGameInfo = string.Format(Mode17GameInfoPattern, string.Format(Formatter.DECIMALFORMAT1, _ticket.Handicap1));
            var expectedText = $"{expectedSeat}, {expectedTable} {expectedGameInfo}";

            Assert.AreEqual(expectedText, linesInActualText[1]);
        }

        [Test]
        public void BuildMatch_Mode18_SmallBlindsStoredInHandicap1()
        {
            // Arrange.
            _ticket.CommStatus = Mode18;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);
            var gameInfo = template.Match.ToString();

            // Assert.
            var formattedSmallBlinds = string.Format(Formatter.DECIMALFORMAT1, _ticket.Handicap1);

            StringAssert.Contains(formattedSmallBlinds, gameInfo);
        }

        [Test]
        public void BuildMatch_Mode18_BigBlindIsDoubleOfHandicap1()
        {
            // Arrange.
            _ticket.CommStatus = Mode18;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);
            var gameInfo = template.Match.ToString();

            // Assert.
            var formattedBigBlind = string.Format(Formatter.DECIMALFORMAT1, _ticket.Handicap1 * 2);

            StringAssert.Contains(formattedBigBlind, gameInfo);
        }

        [Test]
        public void BuildMatch_Mode18_AnteAmountStoredInExtraInfo()
        {
            // Arrange.
            _ticket.CommStatus = Mode18;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);
            var gameInfo = template.Match.ToString();

            // Assert.
            var formattedAnteAmount = string.Format(Formatter.DECIMALFORMAT1, AnteAmount);

            StringAssert.Contains(formattedAnteAmount, gameInfo);
        }

        [Test]
        public void BuildBetTeamsInfo_Mode18_SecondLineIsSeatTableAndBlindsAnteInfo()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            _ticket.CommStatus = Mode18;

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            var expectedSeat = string.Format(SeatResourceIdPattern, _ticket.LiveHomeScore);
            var expectedTable = string.Format(TableResourceIdPattern, TableNumber);
            var expectedGameInfo = string.Format(
                Mode18GameInfoPattern,
                string.Format(Formatter.DECIMALFORMAT1, _ticket.Handicap1),
                string.Format(Formatter.DECIMALFORMAT1, _ticket.Handicap1 * 2),
                string.Format(Formatter.DECIMALFORMAT1, AnteAmount));
            var expectedText = $"{expectedSeat}, {expectedTable} {expectedGameInfo}";

            Assert.AreEqual(expectedText, linesInActualText[1]);
        }
    }
}