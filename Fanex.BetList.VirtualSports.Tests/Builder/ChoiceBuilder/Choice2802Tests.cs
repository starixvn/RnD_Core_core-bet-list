namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using Core.App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice2802Tests : MatchCodeTests
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();

            Choice = new Choice2802();
            Ticket.BetTypeId = BetRadarVirtualBasketballBetTypes.FirstHalfWinner;

            TicketHelper.GetBetTypeNameById(Arg.Is(BetRadarVirtualBasketballBetTypes.FirstHalfWinner), Arg.Any<object>()).Returns("1H Winner");
        }

        [Test]
        public void BuildTeamName_TicketBetTeamIs1_RenderedBetTeamIsHomeTeamName()
        {
            // Arrange.
            Ticket.BetTeam = "1";

            // Act.
            var builtChoice = Choice.Render(Ticket, TicketHelper, null, false);

            // Assert.
            Assert.AreEqual(HomeTeamName, builtChoice.betTeam);
        }

        [TestCase("x")]
        [TestCase("X")]
        public void BuildTeamName_TicketBetTeamIsX_RenderedBetTeamIsDraw(string betTeam)
        {
            // Arrange.
            Ticket.BetTeam = betTeam;

            // Act.
            var builtChoice = Choice.Render(Ticket, TicketHelper, null, false);

            // Assert.
            Assert.AreEqual(CoreBetList.draw, builtChoice.betTeam);
        }

        [Test]
        public void BuildTeamName_TicketBetTeamIs2_RenderedBetTeamIsAwayTeamName()
        {
            // Arrange.
            Ticket.BetTeam = "2";

            // Act.
            var builtChoice = Choice.Render(Ticket, TicketHelper, null, false);

            // Assert.
            Assert.AreEqual(AwayTeamName, builtChoice.betTeam);
        }
    }
}