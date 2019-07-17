namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice2808Tests : Choice2807Tests
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();

            Choice = new Choice2808();
            Ticket.BetTypeId = BetRadarVirtualBasketballBetTypes.FirstHalfWinningMargin7Way;

            TicketHelper.GetBetTypeNameById(Arg.Is(BetRadarVirtualBasketballBetTypes.FirstHalfWinningMargin7Way), Arg.Any<object>()).Returns("FH Winning Margin 7 Way");
        }

        [TestCase("d")]
        [TestCase("D")]
        public void BuildTeamName_TicketBetTeamIsD_RenderedBetTeamIsDraw(string betTeam)
        {
            // Arrange.
            const string ExpectedBetTeam = "Draw";

            Ticket.BetTeam = betTeam;

            TicketHelper.GetResourceData(Choice2807.BasketballRefId, "D").Returns(ExpectedBetTeam);

            // Act.
            var builtChoice = Choice.Render(Ticket, TicketHelper, null, false);

            // Assert.
            Assert.AreEqual(ExpectedBetTeam, builtChoice.betTeam);
        }
    }
}