namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice2701Tests : MatchCodeTests
    {
        private const int TestingBetTypeId = 2701;

        [SetUp]
        public override void Setup()
        {
            base.Setup();

            Choice = new Choice2701();
            Ticket.BetTypeId = TestingBetTypeId;

            TicketHelper.GetBetTypeNameById(Arg.Is(TestingBetTypeId), Arg.Any<object>()).Returns("FT.1X2");
        }
    }
}