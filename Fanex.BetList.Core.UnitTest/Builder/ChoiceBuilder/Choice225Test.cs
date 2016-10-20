namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice225Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice225();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket.BetTypeId = 221;
        }

        [Test]
        public void BuildBetTeam_FromBetTeamIsInvalid_BetTeamIsTimeRange()
        {
            // Arrange
            _ticket.BetTeam = "0";
            _ticket.Handicap1 = 9;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(" (09:00-09:59)", _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_FromBetTeamIs1_BetTeamIsNoAndTimeRange()
        {
            // Arrange
            _ticket.BetTeam = "1";
            _ticket.Handicap1 = 9;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual($"{CoreBetList.lblNo} (09:00-09:59)", _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_FromBetTeamIs156_BetTeamIsYesAndTimeRange()
        {
            // Arrange
            _ticket.BetTeam = "156";
            _ticket.Handicap1 = 80;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual($"{CoreBetList.lblYes} (80:00-80:59)", _choice.Template.betTeam);
        }
    }
}