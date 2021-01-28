namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice9404Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice9404();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.HomeInnsRunsOU;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }


        [TestCase("o", "Over")]
        [TestCase("O", "Over")]
        [TestCase("u", "Under")]
        [TestCase("U", "Under")]
        public void BuildBetTeam_Always_ShowTeamName(string betteam, string teamName)
        {
            // Arrange            
            _ticket.BetTeam = betteam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(teamName, _choice.Template.betTeam);
        }
    }
}