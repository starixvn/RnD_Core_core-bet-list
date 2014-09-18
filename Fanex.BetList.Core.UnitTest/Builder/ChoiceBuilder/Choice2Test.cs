namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice2: Odd/Even.
    /// </summary>
    [TestFixture]
    public class Choice2Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice2();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.Odd_Even;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// If the value of ticket.BetTeam is 'h', the Template.betTeam will be 'Odd'.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIsCharacterH_BetTeamIsOdd()
        {
            // Arrange
            _ticket.BetTeam = "h";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = "Odd";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// If the value of ticket.BetTeam is 'a', the Template.betTeam will be 'Even'.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIsCharacterA_BetTeamIsEven()
        {
            // Arrange
            _ticket.BetTeam = "a";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = "Even";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Handicap is always null.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_HandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        /// <summary>
        /// The value of bet team class is 'favorite' when bet team is 'h'.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetTeamIsCharacterh_BetTeamClassNameIsFavorite()
        {
            // Arrange
            _ticket.BetTeam = "h";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeamClassName = "favorite";
            Assert.AreEqual(expectedBetTeamClassName, _choice.Template.betTeamClassName);
        }

        /// <summary>
        /// The value of bet team class is 'underdog' when bet team is 'a'.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetTeamIsCharactera_BetTeamClassNameIsUnderdog()
        {
            // Arrange
            _ticket.BetTeam = "a";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeamClassName = "underdog";
            Assert.AreEqual(expectedBetTeamClassName, _choice.Template.betTeamClassName);
        }
    }
}