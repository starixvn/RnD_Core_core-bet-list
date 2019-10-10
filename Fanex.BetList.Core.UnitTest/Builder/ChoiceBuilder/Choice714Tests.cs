namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice714Tests
    {
        [Test]
        public void BuildBetType_Always_SendBetIdAndBetCheckToGetCorrectGameName()
        {
            // Arrange.
            const int betId = 803;
            const string betCheck = "803";
            const string expectedBetTypeName = "Game 3 Extra Points";

            var choice = new Choice714();
            var ticketHelper = Substitute.For<ITicketHelper>();
            var ticket = new Ticket
            {
                BetTypeId = BetTypes.GameXExtraPoints,
                BetTeam = BetTeamValue.Y,
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