namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice2705Tests : MatchCodeTests
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();

            Choice = new Choice2705();
            Ticket.BetTypeId = BetRadarVirtualSoccerBetTypes.Handicap;

            TicketHelper.GetBetTypeNameById(Arg.Is(BetRadarVirtualSoccerBetTypes.Handicap), Arg.Any<object>()).Returns("Handicap");
        }
    }
}