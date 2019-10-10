namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice703Tests
    {
        [Test]
        public void BuildBetTeam_Always_DisplayedBetTeamIsTicketBetTeam()
        {
            // Arrange.
            const string betTeam = "1-2";

            var choice = new Choice703();
            var ticket = TestData.CreateTicket(BetTypes.MatchCorrectScore);
            var ticketHelper = Substitute.For<ITicketHelper>();

            ticket.BetTeam = betTeam;

            // Act.
            var template = choice.Render(ticket, ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(betTeam, template.betTeam);
        }
    }
}