namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

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
            _ticket.BetTypeId = BetTypes.CleanSheet;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_BetTeamContainsCharacterhAndNotContainsCharacterY_BetTeamAreHomeTeamNameAndLableNoRescoure()
        {
            // Arrange
            _ticket.BetTeam = "text h";
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns("Home team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeam = string.Format(BetTeamTemplate, _choice.Template.Match.homeTeam, CoreBetList.lblNo.ToUpperInvariant());
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamContainsCharacterNotCharacterhAndNotContainsCharacterY_BetTeamAreAwayTeamNameAndLableNoRescoure()
        {
            // Arrange
            _ticket.BetTeam = string.Empty;
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns("Away team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeam = string.Format(BetTeamTemplate, _ticketHelper.GetTeamNameById(_ticket.AwayId), CoreBetList.lblNo.ToUpperInvariant());
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamContainsCharacterhAndContainsCharacterY_BetTeamAreHomeTeamNameAndLableYesRescoure()
        {
            // Arrange
            _ticket.BetTeam = "text h y";
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns("Home team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeam = string.Format(BetTeamTemplate, _choice.Template.Match.homeTeam, CoreBetList.lblYes.ToUpperInvariant());
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamNotContainsCharacterNotCharacterhAndContainsCharacterY_BetTeamAreAwayTeamNameAndLableYesRescoure()
        {
            // Arrange
            _ticket.BetTeam = "text y";
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns("Away team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeam = string.Format(BetTeamTemplate, _ticketHelper.GetTeamNameById(_ticket.AwayId), CoreBetList.lblYes.ToUpperInvariant());
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }
    }
}