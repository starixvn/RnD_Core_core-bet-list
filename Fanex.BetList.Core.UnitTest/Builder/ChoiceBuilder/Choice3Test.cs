namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice3: Over/Under.
    /// </summary>
    [TestFixture]
    public class Choice3Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice3();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.Over_Under;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// If the value of ticket.BetTeam is 'h', the Template.betTeam will be 'Over'.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIsCharacterH_BetTeamIsOver()
        {
            // Arrange
            _ticket.BetTeam = "h";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = "Over";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// If the value of ticket.BetTeam is 'a', the Template.betTeam will be 'Under'.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIsCharacterA_BetTeamIsUnder()
        {
            // Arrange
            _ticket.BetTeam = "a";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = "Under";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Handicap is always the value of ticket.Handicap1.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_HandicapIsHandicap1()
        {
            // Arrange
            _ticket.Handicap1 = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(_ticket.Handicap1.ToString(), _choice.Template.Handicap.handicap);
        }
    }
}