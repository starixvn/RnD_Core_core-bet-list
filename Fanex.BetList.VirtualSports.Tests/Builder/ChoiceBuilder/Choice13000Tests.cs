namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using Core.App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice13000Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice13000();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket.HomeId = 1;
            _ticket.AwayId = 2;
            _ticketHelper.GetTeamName3rd(Arg.Is("1")).Returns("HomeTeam");
            _ticketHelper.GetTeamName3rd(Arg.Is("2")).Returns("AwayTeam");
        }

        [TestCase("0", "Draw")]
        [TestCase("1", "HomeTeam")]
        [TestCase("2", "AwayTeam")]
        public void BuildBetTeam_Always_ReturnMappingTeamName(string betTeam, string teamName)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(teamName, _choice.Template.betTeam);
        }
    }
}