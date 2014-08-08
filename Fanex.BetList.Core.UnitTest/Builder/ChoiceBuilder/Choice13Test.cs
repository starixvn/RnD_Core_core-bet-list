namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice13 class.
    /// </summary>
    [TestFixture]
    public class Choice13Test
    {
        private const string BetTeamTemplate = "{0} <span class=\"favorite\">{1}</span>";
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice13();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.Clean_Sheet;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.betTeam is always [HomeTeam YES] when bet team contains character h and not contains y.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamContainsCharacterhAndNotContainsCharacterY_BetTeamAreHomeTeamNameAndLableNoRescoure()
        {
            // Arrange
            _ticket.BetTeam = "text h";
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns("Home team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = string.Format(BetTeamTemplate, _choice.Template.Match.homeTeam, CoreBetList.lblNo.ToUpper());
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always [AwayTeam NO] when bet team not contains character h and not contains y.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamContainsCharacterNotCharacterhAndNotContainsCharacterY_BetTeamAreAwayTeamNameAndLableNoRescoure()
        {
            // Arrange
            _ticket.BetTeam = string.Empty;
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns("Away team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = string.Format(BetTeamTemplate, _ticketHelper.GetTeamNameById(_ticket.AwayId), CoreBetList.lblNo.ToUpper());
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always [HomeTeam YES] when bet team contains character h and contains y.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamContainsCharacterhAndContainsCharacterY_BetTeamAreHomeTeamNameAndLableYesRescoure()
        {
            // Arrange
            _ticket.BetTeam = "text h y";
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns("Home team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = string.Format(BetTeamTemplate, _choice.Template.Match.homeTeam, CoreBetList.lblYes.ToUpper());
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always [AwayTeam YES] when bet team contains not character h and contains y.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamNotContainsCharacterNotCharacterhAndContainsCharacterY_BetTeamAreAwayTeamNameAndLableYesRescoure()
        {
            // Arrange
            _ticket.BetTeam = "text y";
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns("Away team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = string.Format(BetTeamTemplate, _ticketHelper.GetTeamNameById(_ticket.AwayId), CoreBetList.lblYes.ToUpper());
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.Handicap.handicap is always null.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }
    }
}