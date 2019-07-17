namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice2809Tests : MatchCodeTests
    {
#pragma warning disable S4142 // Duplicate values should not be passed as arguments

        [SetUp]
        public override void Setup()
        {
            base.Setup();

            Choice = new Choice2809();
            Ticket.BetTypeId = BetRadarVirtualBasketballBetTypes.FullTimeRaceToXPoints;
            Ticket.BetTeam = "H";

            TicketHelper
                .GetBetTypeNameById(Arg.Is(BetRadarVirtualBasketballBetTypes.FullTimeRaceToXPoints), Arg.Any<object>(), Arg.Any<object>(), Arg.Any<object>())
                .Returns("FT Race to X Points");
        }

#pragma warning restore S4142 // Duplicate values should not be passed as arguments
    }
}