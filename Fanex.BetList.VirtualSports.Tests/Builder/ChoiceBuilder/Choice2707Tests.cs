namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using System;
    using Core.App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice2707Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice2707();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = 2707;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("0:0")]
        [TestCase("1:0")]
        [TestCase("0:1")]
        [TestCase("2:0")]
        [TestCase("0:2")]
        [TestCase("3:0")]
        [TestCase("0:3")]
        [TestCase("2:1")]
        [TestCase("1:2")]
        [TestCase("3:1")]
        [TestCase("2:2")]
        [TestCase("3:2")]
        [TestCase("2:3")]
        [TestCase("3:3")]
        public void BuildBetTeam_BetOptionIsOneOfSupportedList_ReplaceColonWithHyphen(string betOption)
        {
            // Arrange.
            _ticket.BetTeam = betOption;

            // Act.
            var builtChoice = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedText = betOption.Replace(':', '-');

            Assert.AreEqual(expectedText, builtChoice.betTeam);
        }

        [TestCase("aos")]
        [TestCase("AOS")]
        public void BuildBetTeam_BetOptionIsAOS_BetChoiceIsAOS(string betOption)
        {
            // Arrange.
            _ticket.BetTeam = betOption;

            // Act.
            var builtChoice = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedText = CoreBetList.AOS;

            Assert.AreEqual(expectedText, builtChoice.betTeam);
        }

        [TestCase("m=\"Round 16-1, Nation Cup 6666\" s=\"1:2,2:1,2:2\"", "1-2, 2-1, 2-2")]
        [TestCase("s=\"1:2,2:1,2:2\"", "1-2, 2-1, 2-2")]
        [TestCase("s=\"1:2,2:1,2:2\" redundant text", "1-2, 2-1, 2-2")]
        [TestCase("redundant text s=\"1:2,2:1,2:2\"", "1-2, 2-1, 2-2")]
        public void BuildBetTeam_BetOptionIsAOSAndContainsCorrectExcludingScoresPattern_AppendExcludingScoreRightAfterBetChoice(
            string transDesc,
            string expectedExculingScores)
        {
            // Arrange.
            _ticket.BetTeam = "aos";
            _ticket.TransDesc = transDesc;

            // Act.
            var builtChoice = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedHtml = $"<div class=\"excluding-choices\">{CoreBetList.Excluding}: {expectedExculingScores}</div>";

            Assert.AreEqual(expectedHtml, builtChoice.Score.ToString());
        }

        [TestCase("s=1:2,2:1,2:2\"")]
        [TestCase("s=1:2,2:1,2:2")]
        [TestCase("\"1:2,2:1,2:2\"")]
        public void BuildBetTeam_BetOptionIsAOSAndContainsIncorrectExcludingScoresPattern_NotAppendExcludingScore(string transDesc)
        {
            // Arrange.
            _ticket.BetTeam = "aos";
            _ticket.TransDesc = transDesc;

            // Act.
            var builtChoice = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.False(builtChoice.Score.Visible);
        }

        [TestCase("m=\"Round 16-1, Nation Cup 6666\" s=\"1:2,2:1,2:2\"", "1-2, 2-1, 2-2")]
        [TestCase("s=\"1:2,2:1,2:2\"", "1-2, 2-1, 2-2")]
        [TestCase("s=\"1:2,2:1,2:2\" redundant text", "1-2, 2-1, 2-2")]
        [TestCase("redundant text s=\"1:2,2:1,2:2\"", "1-2, 2-1, 2-2")]
        public void BuildBetFigure_BetOptionIsAOSAndContainsCorrectExcludingScoresPattern_AppendExcludingScoreRightAfterBetChoice(
            string transDesc,
            string expectedExculingScores)
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            _ticket.BetTeam = "aos";
            _ticket.TransDesc = transDesc;

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split(new[] { "\n", "- vs -" }, StringSplitOptions.None);

            // Assert.
            var expectedText = $"{CoreBetList.Excluding}: {expectedExculingScores}";

            Assert.AreEqual(expectedText, linesInActualText[1]);
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
            _ticketHelper.GetBetTypeNameById(Arg.Is(2707), Arg.Any<object>()).Returns("FT.1X2");

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
            _ticketHelper.GetBetTypeNameById(Arg.Is(2707), Arg.Any<object>()).Returns("FT.1X2");

            // Act
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert
            var expectedRoundInfo = string.Empty;

            Assert.AreEqual(expectedRoundInfo, linesInActualText[2]);
        }
    }
}