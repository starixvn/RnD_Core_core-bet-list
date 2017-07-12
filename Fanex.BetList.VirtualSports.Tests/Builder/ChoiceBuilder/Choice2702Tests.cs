namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using System;
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

        [Test]
        public void BuildMatch_Always_AppendMatchDayOrRoundInfoRightAfterMatch()
        {
            // Arrange.
            _ticket.TransDesc = "Round 16-1, Nation Cup 6666";

            // Act.
            var builtChoice = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            string expectedHtmlOfRoundInfo = "<div class=\"match\">Round 16-1, Nation Cup 6666</div>";
            Assert.IsTrue(builtChoice.Match.ToString().EndsWith(expectedHtmlOfRoundInfo, StringComparison.InvariantCulture));
        }

        [Test]
        public void BuildBetTeamsInfo_Always_MatchDayOrRoundInfoIsAtTheThirdRow()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            _ticket.TransDesc = "Round 16-1, Nation Cup 6666";
            _ticketHelper.GetBetTypeNameById(Arg.Is(2702), Arg.Any<object>()).Returns("1H 1X2");

            // Act
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert
            var expectedRoundInfo = "Round 16-1, Nation Cup 6666";

            Assert.AreEqual(linesInActualText[2], expectedRoundInfo);
        }
    }
}