namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice1302 class.
    /// </summary>
    [TestFixture]
    public class Choice1302Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1302();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.MatchCorrectScore;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.ticketStatus is always void when Ticket.Status = void.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        /// <summary>
        /// The value of Template.betTeam is empty when BetTeam.Length != 2.
        /// </summary>
        [Test]
        public void BuildBetTeam_LengthBetTeamNotEqual2_BetTeamIsEmpty()
        {
            // Arrange
            _ticket.BetTeam = "bet team";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("&nbsp;", _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always [BetTeam[0]:BetTeam[1]] when BetTeam.Length == 2.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIsAB_BetTeamIsAColonB()
        {
            // Arrange
            _ticket.BetTeam = "ab";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("&nbsp;a:b", _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is always favorite.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedTeamClassName = "favorite";
            Assert.AreEqual(expectedTeamClassName, _choice.Template.betTeamClassName);
        }

        /// <summary>
        /// The value of Template.Handicap.handicap is always NULL.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_HandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }
    }
}