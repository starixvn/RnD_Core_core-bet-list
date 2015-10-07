namespace Fanex.BetList.MyanmarOdds.Test.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
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

        [Test]
        public void BuildBetTeamClassNameAndHandicap_SetHandicap()
        {
            _ticket.Handicap1 = 2;

            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.IsTrue(_choice.Template.Handicap.handicap.StartsWith(_ticket.Handicap1.ToString(), System.StringComparison.OrdinalIgnoreCase));
        }
    }
}