namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice2705Tests : MatchCodeTests
    {
        private const int TestingBetTypeId = 2705;

        [SetUp]
        public override void Setup()
        {
            base.Setup();

            Choice = new Choice2705();
            Ticket.BetTypeId = TestingBetTypeId;

            TicketHelper.GetBetTypeNameById(Arg.Is(TestingBetTypeId), Arg.Any<object>()).Returns("FT HDP");
        }
    }
}