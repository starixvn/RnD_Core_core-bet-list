namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice401Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice401();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// If the value of ticket.BetTeam is 'O' or 'o', the Template.betTeam will be 'Over'.
        /// </summary>
        [TestCase("O")]
        [TestCase("o")]
        public void BuildBetTeam_BetTeamIsCharacterO_BetTeamIsOver(string betTeam)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = "Over";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// If the value of ticket.BetTeam is 'U' or 'u', the Template.betTeam will be 'Under'.
        /// </summary>
        [TestCase("U")]
        [TestCase("u")]
        public void BuildBetTeam_BetTeamIsCharacterU_BetTeamIsUnder(string betTeam)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

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

        /// <summary>
        /// The betTeamClassName is favorite when the bet team is 'o' or 'O'.
        /// </summary>
        [TestCase("o")]
        [TestCase("O")]
        public void BuildBetTeamClassNameAndHandicap_BetTeamisO_BetTeamClassNameIsFavorite(string betTeam)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedClass = "favorite";
            Assert.AreEqual(expectedClass, _choice.Template.betTeamClassName);
        }

        /// <summary>
        /// The betTeamClassName is underdog when the bet team is 'u' or 'U'.
        /// </summary>
        [TestCase("u")]
        [TestCase("U")]
        public void BuildBetTeamClassNameAndHandicap_BetTeamisU_BetTeamClassNameIsUnderDog(string betTeam)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedClass = "underdog";
            Assert.AreEqual(expectedClass, _choice.Template.betTeamClassName);
        }
    }
}