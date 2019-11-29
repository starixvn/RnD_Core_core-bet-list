namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice384Tests
    {
        [Test]
        public void BuildBetType_Always_SendBetIdAndBetCheckToGetCorrectBetTypeName()
        {
            // Arrange.
            const int betId = 801;
            const string betCheck = "801";
            const string expectedBetTypeName = "Penalty Shootout Both/One/Neither - Round 1";

            var choice = new Choice384();
            var ticketHelper = Substitute.For<ITicketHelper>();
            var ticket = new Ticket
            {
                BetTypeId = BetTypes.PenaltyShootoutBothOneNeitherRoundX,
                BetTeam = BetTeamValue.N,
                BetId = betId,
                BetCheck = betCheck
            };

            ticketHelper
                .GetBetTypeNameById(ticket.BetTypeId, Arg.Any<object>(), ticket.BetId, ticket.BetCheck)
                .Returns(expectedBetTypeName);

            // Act.
            var template = choice.Render(ticket, ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(expectedBetTypeName, template.BetType.betTypeName);
        }
    }
}