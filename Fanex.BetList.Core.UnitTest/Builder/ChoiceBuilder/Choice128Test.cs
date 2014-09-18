namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for bet type Half Time / Full Time Odd/Even.
    /// </summary>
    [TestFixture]
    public class Choice128Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice128();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_BetTeamIsOO_SetBetTeamOddOdd()
        {
            // Arrange
            _ticket.BetTeam = "oo";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(CoreBetList.oo, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsOE_SetBetTeamOddEven()
        {
            // Arrange
            _ticket.BetTeam = "oe";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(CoreBetList.oe, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsEO_SetBetTeamEvenOdd()
        {
            // Arrange
            _ticket.BetTeam = "eo";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(CoreBetList.eo, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsEE_SetBetTeamEvenEven()
        {
            // Arrange
            _ticket.BetTeam = "ee";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(CoreBetList.ee, _choice.Template.betTeam);
        }
    }
}