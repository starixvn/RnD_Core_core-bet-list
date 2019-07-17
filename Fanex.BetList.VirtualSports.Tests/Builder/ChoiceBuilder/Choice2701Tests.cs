namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice2701Tests : MatchCodeTests
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();

            Choice = new Choice2701();
            Ticket.BetTypeId = BetRadarVirtualSoccerBetTypes.FullTime1X2;

            TicketHelper.GetBetTypeNameById(Arg.Is(BetRadarVirtualSoccerBetTypes.FullTime1X2), Arg.Any<object>()).Returns("FT.1X2");
        }
    }
}