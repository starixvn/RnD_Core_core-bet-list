namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice1333 class.
    /// </summary>
    [TestFixture]
    public class Choice1333Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1333();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.SetXGameYAndGameYAdd1Winner;
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
        /// The value of Template.betTeam is always Over.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs1_BetTeamIsHomeTeamName()
        {
            // Arrange
            _ticket.BetTeam = "1";
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns("Home team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(_choice.Template.Match.homeTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always Over.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs2_BetTeamIsAwayTeamName()
        {
            // Arrange
            _ticket.BetTeam = "2";
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns("Away team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(_choice.Template.Match.awayTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always Between handicap 2 and handicap1.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamNotIs1AndNotIs2_BetTeamIsNeitherResource()
        {
            // Arrange
            _ticket.BetTeam = string.Empty;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.lblNeither, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.BetType.betTypeName is always bet type name.
        /// </summary>
        [Test]
        public void BuildBetTeam_Always_SetBetTypeNameIsBetTypeName()
        {
            // Arrange
            _ticket.BetTypeId = 1;
            _ticket.BetId = 1;
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>(), Arg.Any<long>()).Returns("Bet type name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTypeName = _ticketHelper.GetBetTypeNameById(_ticket.BetTypeId, _ticket.BetId);
            Assert.AreEqual(expectedBetTypeName, _choice.Template.BetType.betTypeName);
        }
    }
}