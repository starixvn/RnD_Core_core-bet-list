namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using Core.App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice2707Tests : MatchCodeTests
    {
        private const int TestingBetTypeId = 2707;

        [SetUp]
        public override void Setup()
        {
            base.Setup();

            Choice = new Choice2707();
            Ticket.BetTypeId = TestingBetTypeId;

            TicketHelper.GetBetTypeNameById(Arg.Is(TestingBetTypeId), Arg.Any<object>()).Returns("Correct Score");
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
            Ticket.BetTeam = betOption;

            // Act.
            var builtChoice = Choice.Render(Ticket, TicketHelper, null, false);

            // Assert.
            var expectedText = betOption.Replace(':', '-');

            Assert.AreEqual(expectedText, builtChoice.betTeam);
        }

        [TestCase("aos")]
        [TestCase("AOS")]
        public void BuildBetTeam_BetOptionIsAOS_BetChoiceIsAOS(string betOption)
        {
            // Arrange.
            Ticket.BetTeam = betOption;

            // Act.
            var builtChoice = Choice.Render(Ticket, TicketHelper, null, false);

            // Assert.
            var expectedText = CoreBetList.AOS;

            Assert.AreEqual(expectedText, builtChoice.betTeam);
        }

        [TestCase("m=\"{2} 16-1, {0} 6666\" s=\"1:2,2:1,2:2\"", "1-2, 2-1, 2-2")]
        [TestCase("s=\"1:2,2:1,2:2\"", "1-2, 2-1, 2-2")]
        [TestCase("s=\"1:2,2:1,2:2\" redundant text", "1-2, 2-1, 2-2")]
        [TestCase("redundant text s=\"1:2,2:1,2:2\"", "1-2, 2-1, 2-2")]
        public void BuildBetTeam_BetOptionIsAOSAndContainsCorrectExcludingScoresPattern_AppendExcludingScoreRightAfterBetChoice(
            string transDesc,
            string expectedExculingScores)
        {
            // Arrange.
            Ticket.BetTeam = "aos";
            Ticket.TransDesc = transDesc;

            // Act.
            var builtChoice = Choice.Render(Ticket, TicketHelper, null, false);

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
            Ticket.BetTeam = "aos";
            Ticket.TransDesc = transDesc;

            // Act.
            var builtChoice = Choice.Render(Ticket, TicketHelper, null, false);

            // Assert.
            Assert.False(builtChoice.Score.Visible);
        }

        [TestCase("m=\"{2} 16-1, {0} 6666\" s=\"1:2,2:1,2:2\"", "1-2, 2-1, 2-2")]
        [TestCase("s=\"1:2,2:1,2:2\"", "1-2, 2-1, 2-2")]
        [TestCase("s=\"1:2,2:1,2:2\" redundant text", "1-2, 2-1, 2-2")]
        [TestCase("redundant text s=\"1:2,2:1,2:2\"", "1-2, 2-1, 2-2")]
        public void BuildBetFigure_BetOptionIsAOSAndContainsCorrectExcludingScoresPattern_AppendExcludingScoreRightAfterBetChoice(
            string transDesc,
            string expectedExculingScores)
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            Ticket.BetTeam = "aos";
            Ticket.TransDesc = transDesc;

            // Act.
            var actualText = Choice.RenderRTF(Ticket, TicketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            var expectedText = $"{CoreBetList.Excluding}: {expectedExculingScores}";

            Assert.AreEqual(expectedText, linesInActualText[1]);
        }

        [TestCase("m=\"Round 16-1, Nation Cup 6666\" s=\"1:2,2:1,2:2\"")]
        [TestCase("m=\"{2} 16-1, {0} 6666\" s=\"1:2,2:1,2:2\"")]
        public void BuildBetTeamsInfo_BetOptionIsAOSAndContainsCorrectExcludingScoresPattern_MatchDayOrRoundInfoIsAtTheFifthRow(string transDesc)
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            Ticket.BetTeam = "aos";
            Ticket.TransDesc = transDesc;

            // Act
            var actualText = Choice.RenderRTF(Ticket, TicketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert
            Assert.AreEqual(ExpectedMatchDayOrRoundInfo, linesInActualText[4]);
        }
    }
}