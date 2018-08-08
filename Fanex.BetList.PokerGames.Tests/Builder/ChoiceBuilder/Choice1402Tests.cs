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
        private const string BlindsResourceId = "Blinds";
        private const string PlayerIdResourceId = "PlayerID";

        private const string SeatResourceIdPattern = "Seat #{0}";
        private const string TableResourceIdPattern = "Table #{0}";
        private const string BlindsResourceIdPattern = "( {0}/{1} Blinds )";
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
            _choice = new Choice1402();
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

            _ticketHelper.GetResourceData(Arg.Is(Choice1402.GameInfoRefId), Arg.Is(SeatResourceId)).Returns(SeatResourceIdPattern);
            _ticketHelper.GetResourceData(Arg.Is(Choice1402.GameInfoRefId), Arg.Is(TableResourceId)).Returns(TableResourceIdPattern);
            _ticketHelper.GetResourceData(Arg.Is(Choice1402.GameInfoRefId), Arg.Is(BlindsResourceId)).Returns(BlindsResourceIdPattern);
            _ticketHelper.GetResourceData(Arg.Is(Choice1402.GameModeRefId), Arg.Is(_ticket.CommStatus)).Returns(TableMode);
            _ticketHelper.GetResourceData(Arg.Is(Choice1402.GameInfoRefId), Arg.Is(PlayerIdResourceId)).Returns(PlayerIdPattern);
            _ticketHelper.GetResourceData(Arg.Is(Choice1402.GameTypeRefId), Arg.Is(_ticket.BetTypeId.ToString())).Returns(BetTypeName);
        }

        [Test]
        public void BuildMatch_Always_CallRemotingRefToGetTextOfBlinds()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            _ticketHelper.Received().GetResourceData(Arg.Is(Choice1401.GameInfoRefId), Arg.Is(BlindsResourceId));
        }

        [Test]
        public void BuildMatch_Always_SmallBlindStoredInHandicap1()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);
            var gameInfo = template.Match.ToString();

            // Assert.
            var formattedSmallBlind = string.Format(Formatter.DECIMALFORMAT1, _ticket.Handicap1);

            StringAssert.Contains(formattedSmallBlind, gameInfo);
        }

        [Test]
        public void BuildMatch_Always_BigBlindIsDoubleOfHandicap1()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);
            var gameInfo = template.Match.ToString();

            // Assert.
            var formattedBigBlind = string.Format(Formatter.DECIMALFORMAT1, _ticket.Handicap1 * 2);

            StringAssert.Contains(formattedBigBlind, gameInfo);
        }

        [Test]
        public void BuildMatch_Always_SmallBlindAndBigBlindAreInCorrectOrder()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);
            var gameInfo = template.Match.ToString();

            // Assert.
            var formattedSmallBlind = string.Format(Formatter.DECIMALFORMAT1, _ticket.Handicap1);
            var formattedBigBlind = string.Format(Formatter.DECIMALFORMAT1, _ticket.Handicap1 * 2);
            var expectedResult = string.Format(BlindsResourceIdPattern, formattedSmallBlind, formattedBigBlind);

            StringAssert.Contains(expectedResult, gameInfo);
        }

        [Test]
        public void BuildBetTeamsInfo_Always_SecondLineIsSeatTableAndBlindInfo()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            var expectedSeat = string.Format(SeatResourceIdPattern, _ticket.LiveHomeScore);
            var expectedTable = string.Format(TableResourceIdPattern, TableNumber);
            var expectedBlind = string.Format(
                BlindsResourceIdPattern,
                string.Format(Formatter.DECIMALFORMAT1, _ticket.Handicap1),
                string.Format(Formatter.DECIMALFORMAT1, _ticket.Handicap1 * 2));
            var expectedText = $"{expectedSeat}, {expectedTable} {expectedBlind}";

            Assert.AreEqual(expectedText, linesInActualText[1]);
        }
    }
}