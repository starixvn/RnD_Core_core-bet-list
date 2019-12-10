namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice152Test
    {
        private Choice152 _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice152();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.HalfTimeFullTimeCorrectScore;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_Always_SetBetTeamIsBetTeam()
        {
            // Arrange
            _ticket.BetTeam = "A Bet Team";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(_ticket.BetTeam, _choice.Template.betTeam);
        }
    }
}