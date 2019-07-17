namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice2702Tests : MatchCodeTests
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();

            Choice = new Choice2702();
            Ticket.BetTypeId = BetRadarVirtualSoccerBetTypes.FirstHalf1X2;

            TicketHelper.GetBetTypeNameById(Arg.Is(BetRadarVirtualSoccerBetTypes.FirstHalf1X2), Arg.Any<object>()).Returns("1H 1X2");
        }

        [TestCase("1", "HT.1")]
        [TestCase("x", "HT.X")]
        [TestCase("X", "HT.X")]
        [TestCase("2", "HT.2")]
        public void BuildTeamName_Always_RenderCorrectName(string betTeam, string expectedBetTeam)
        {
            // Arrange.
            Ticket.BetTeam = betTeam;

            // Act.
            var builtChoice = Choice.Render(Ticket, TicketHelper, null, false);

            // Assert.
            Assert.AreEqual(expectedBetTeam, builtChoice.betTeam);
        }
    }
}