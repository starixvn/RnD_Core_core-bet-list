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
        public void When_BetTeam_Is_1h_BetChoice_Should_Display_FirstHalf()
        {
            // Arrange
            _ticket.BetTeam = "1h";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreLang.FirstHalf, _choice.Template.betTeam);
        }

        [Test]
        public void When_BetTeam_Is_2h_BetChoice_Should_Display_SecondHalf()
        {
            // Arrange
            _ticket.BetTeam = "2h";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreLang.SecondHalf, _choice.Template.betTeam);
        }

        [Test]
        public void When_BetTeam_Is_tie_BetChoice_Display_Tie()
        {
            // Arrange
            _ticket.BetTeam = "tie";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreLang.Tie, _choice.Template.betTeam);
        }

        [Test]
        public void BuildChoice_BetTeamIsNotYAndNotN_ShowBetTeamIsEmpty()
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