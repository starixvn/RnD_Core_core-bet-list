namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Choice140Test
    {
        private Choice140 _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice140();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.HighestScoringHalf;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_BetTeamIs1h_BetTeamIsFirstHalfResource()
        {
            // Arrange
            _ticket.BetTeam = "1h";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.FirstHalf, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIs2h_BetTeamIsSecondHalfResource()
        {
            // Arrange
            _ticket.BetTeam = "2h";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.SecondHalf, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIstie_BetTeamIsTieResource()
        {
            // Arrange
            _ticket.BetTeam = "tie";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.Tie, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsNotYAndNotN_BetTeamIsEmpty()
        {
            // Arrange
            _ticket.BetTeam = "any string";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsEmpty(_choice.Template.betTeam);
        }
    }
}