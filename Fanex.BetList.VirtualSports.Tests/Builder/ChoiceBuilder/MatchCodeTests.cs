namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

#pragma warning disable S1694 // An abstract class should have both abstract and concrete methods

    public abstract class MatchCodeTests
    {
        protected const long HomeTeamId = 1;
        protected const long AwayTeamId = 2;
        protected const string HomeTeamName = "Home team";
        protected const string AwayTeamName = "Away team";
        protected const string TestingMatchCodePattern = "{2} 16-1, {0} 6666";
        protected const string ExpectedMatchDayOrRoundInfo = "Round 16-1, Nation Cup 6666";

        protected IChoice Choice { get; set; }

        protected ITicket Ticket { get; set; }

        protected ITicketHelper TicketHelper { get; set; }

        public virtual void Setup()
        {
            Ticket = Substitute.For<ITicket>();
            TicketHelper = Substitute.For<ITicketHelper>();
            TicketHelper.GetBetRadarMatchCodeFormat(Arg.Is(TestingMatchCodePattern)).Returns(ExpectedMatchDayOrRoundInfo);
            TicketHelper.GetBetRadarMatchCodeFormat(Arg.Is(ExpectedMatchDayOrRoundInfo)).Returns(ExpectedMatchDayOrRoundInfo);
            TicketHelper.GetVirtualSportMatchCodeFormat(Arg.Any<string>(), "LVS_MatchCode").Returns(ExpectedMatchDayOrRoundInfo);

            Ticket.HomeId = HomeTeamId;
            Ticket.AwayId = AwayTeamId;

            TicketHelper.GetTeamNameById(Arg.Is(HomeTeamId)).Returns(HomeTeamName);
            TicketHelper.GetTeamNameById(Arg.Is(AwayTeamId)).Returns(AwayTeamName);
        }

        [Test]
        public void BuildMatch_TransDescContainsFullMatchDayOrRoundInfo_AppendMatchDayOrRoundInfoRightAfterMatch()
        {
            // Arrange.
            Ticket.TransDesc = $"m=\"{ExpectedMatchDayOrRoundInfo}\"";

            // Act.
            var builtChoice = Choice.Render(Ticket, TicketHelper, null, false);

            // Assert.
            var expectedHtml = $"<div class=\"match\">{ExpectedMatchDayOrRoundInfo}</div>";

            StringAssert.EndsWith(expectedHtml, builtChoice.Match.ToString());
        }

        [TestCase("m=\"{2} 16-1, {0} 6666\"")]
        [TestCase("m=\"{2} 16-1, {0} 6666\" trailing text")]
        [TestCase("redundant text m=\"{2} 16-1, {0} 6666\"")]
        public void BuildMatch_TransDescContainsCorrectMatchPattern_AppendMatchDayOrRoundInfoRightAfterMatch(string transDesc)
        {
            // Arrange.
            Ticket.TransDesc = transDesc;

            // Act.
            var builtChoice = Choice.Render(Ticket, TicketHelper, null, false);

            // Assert.
            var expectedHtml = $"<div class=\"match\">{ExpectedMatchDayOrRoundInfo}</div>";

            StringAssert.EndsWith(expectedHtml, builtChoice.Match.ToString());
        }

        [Test]
        public void BuildBetTeamsInfo_TransDescContainsFullMatchDayOrRoundInfo_MatchDayOrRoundInfoIsAtTheForthRow()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            Ticket.TransDesc = $"m=\"{ExpectedMatchDayOrRoundInfo}\"";

            // Act
            var actualText = Choice.RenderRTF(Ticket, TicketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert
            Assert.AreEqual(ExpectedMatchDayOrRoundInfo, linesInActualText[3]);
        }

        [TestCase("m=\"{2} 16-1, {0} 6666\"")]
        [TestCase("m=\"{2} 16-1, {0} 6666\" trailing text")]
        [TestCase("redundant text m=\"{2} 16-1, {0} 6666\"")]
        public void BuildBetTeamsInfo_TransDescContainsCorrectMatchPattern_MatchDayOrRoundInfoIsAtTheForthRow(string transDesc)
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            Ticket.TransDesc = transDesc;

            // Act
            var actualText = Choice.RenderRTF(Ticket, TicketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert
            Assert.AreEqual(ExpectedMatchDayOrRoundInfo, linesInActualText[3]);
        }
    }
}