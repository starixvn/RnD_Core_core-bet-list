namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice2702Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice2702();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = 2702;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("1", "HT.1")]
        [TestCase("x", "HT.X")]
        [TestCase("X", "HT.X")]
        [TestCase("2", "HT.2")]
        public void BuildTeamName_Always_RenderCorrectName(string betTeam, string expectedBetTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var builtChoice = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(expectedBetTeam, builtChoice.betTeam);
        }

        [TestCase("m=\"Round 16-1, Nation Cup 6666\"")]
        [TestCase("m=\"Round 16-1, Nation Cup 6666\" trailing text")]
        [TestCase("redundant text m=\"Round 16-1, Nation Cup 6666\"")]
        public void BuildMatch_TransDescContainsCorrectMatchPattern_AppendMatchDayOrRoundInfoRightAfterMatch(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var builtChoice = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedHtmlOfKickOffTime = "<div class=\"match\">Round 16-1, Nation Cup 6666</div>";

            StringAssert.EndsWith(expectedHtmlOfKickOffTime, builtChoice.Match.ToString());
        }

        [TestCase("m=\"Round 16-1, Nation Cup 6666")]
        [TestCase("m=Round 16-1, Nation Cup 6666")]
        [TestCase("Round 16-1, Nation Cup 6666")]
        public void BuildMatch_TransDescContainsIncorrectMatchPattern_AppendEmptyDiv(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var builtChoice = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedHtmlOfKickOffTime = "<div class=\"match\"></div>";

            StringAssert.EndsWith(expectedHtmlOfKickOffTime, builtChoice.Match.ToString());
        }

        [TestCase("m=\"Round 16-1, Nation Cup 6666\"")]
        [TestCase("m=\"Round 16-1, Nation Cup 6666\" trailing text")]
        [TestCase("redundant text m=\"Round 16-1, Nation Cup 6666\"")]
        public void BuildBetTeamsInfo_TransDescContainsCorrectMatchPattern_MatchDayOrRoundInfoIsAtTheThirdRow(string transDesc)
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            _ticket.TransDesc = transDesc;
            _ticketHelper.GetBetTypeNameById(Arg.Is(2702), Arg.Any<object>()).Returns("FT.1X2");

            // Act
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert
            var expectedRoundInfo = "Round 16-1, Nation Cup 6666";

            Assert.AreEqual(expectedRoundInfo, linesInActualText[2]);
        }

        [TestCase("m=\"Round 16-1, Nation Cup 6666")]
        [TestCase("m=Round 16-1, Nation Cup 6666")]
        [TestCase("Round 16-1, Nation Cup 6666")]
        public void BuildBetTeamsInfo_TransDescContainsIncorrectMatchPattern_TheThirdRowIsEmpty(string transDesc)
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            _ticket.TransDesc = transDesc;
            _ticketHelper.GetBetTypeNameById(Arg.Is(2702), Arg.Any<object>()).Returns("FT.1X2");

            // Act
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert
            var expectedRoundInfo = string.Empty;

            Assert.AreEqual(expectedRoundInfo, linesInActualText[2]);
        }
    }
}