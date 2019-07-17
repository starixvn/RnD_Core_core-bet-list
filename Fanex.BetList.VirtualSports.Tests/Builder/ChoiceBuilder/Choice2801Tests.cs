namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice2801Tests : MatchCodeTests
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();

            Choice = new Choice2801();
            Ticket.BetTypeId = BetRadarVirtualBasketballBetTypes.MatchWinner;

            TicketHelper.GetBetTypeNameById(Arg.Is(BetRadarVirtualBasketballBetTypes.MatchWinner), Arg.Any<object>()).Returns("Match Winner");
        }
    }
}