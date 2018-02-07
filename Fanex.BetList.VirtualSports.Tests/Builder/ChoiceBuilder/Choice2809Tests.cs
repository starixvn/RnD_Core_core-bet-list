namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice2809Tests : MatchCodeTests
    {
        private const int TestingBetTypeId = 2809;

        [SetUp]
        public override void Setup()
        {
            base.Setup();

            Choice = new Choice2809();
            Ticket.BetTypeId = TestingBetTypeId;
            Ticket.BetTeam = "H";

            TicketHelper
                .GetBetTypeNameById(Arg.Is(TestingBetTypeId), Arg.Any<object>(), Arg.Any<object>(), Arg.Any<object>())
                .Returns("FT Race to X Points");
        }
    }
}