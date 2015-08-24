namespace Fanex.BetList.MyanmarOdds.Test.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice302Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice302();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("h", "Over")]
        [TestCase("a", "Under")]
        public void BuildBetTeam_BetTeamIsH_SetBetTeam(string betteam, string expectedBetTeam)
        {
            _ticket.BetTeam = betteam;

            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }
    }
}
