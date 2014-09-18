namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using System.Diagnostics.CodeAnalysis;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Utils;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for bet type 1H Home Team Odd/Even - 136.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "EX1003:MethodMustBeDocumented", Justification = "Reviewed.")]
    [TestFixture]
    public class Choice143Test
    {
        private const string TeamName = "Chelsea";
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice143();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_BetTeamIsHO_SetBetTeamIsHomeTeamNameAndOver()
        {
            // Arrange
            _ticket.BetTeam = "ho"; // <<Home>> & Over
            _ticketHelper.GetTeamNameById(Arg.Any<int>()).ReturnsForAnyArgs(TeamName);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(string.Format("{0} & {1}", TeamName, CoreBetList.over), _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsHU_SetBetTeamIsHomeTeamNameAndUnder()
        {
            // Arrange
            _ticket.BetTeam = "hu"; // <<Home>> & Over
            _ticketHelper.GetTeamNameById(Arg.Any<int>()).ReturnsForAnyArgs(TeamName);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(string.Format("{0} & {1}", TeamName, CoreBetList.under), _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsAO_SetBetTeamIsHomeTeamNameAndOver()
        {
            // Arrange
            _ticket.BetTeam = "ao"; // <<Home>> & Over
            _ticketHelper.GetTeamNameById(Arg.Any<int>()).ReturnsForAnyArgs(TeamName);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(string.Format("{0} & {1}", TeamName, CoreBetList.over), _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsAU_SetBetTeamIsHomeTeamNameAndUnder()
        {
            // Arrange
            _ticket.BetTeam = "au"; // <<Home>> & Over
            _ticketHelper.GetTeamNameById(Arg.Any<int>()).ReturnsForAnyArgs(TeamName);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(string.Format("{0} & {1}", TeamName, CoreBetList.under), _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsDO_SetBetTeamIsDrawAndOver()
        {
            // Arrange
            _ticket.BetTeam = "do"; // Draw & Over
            _ticketHelper.GetTeamNameById(Arg.Any<int>()).ReturnsForAnyArgs(TeamName);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(string.Format("{0} & {1}", CoreBetList.draw, CoreBetList.over), _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsDU_SetBetTeamIsDrawAndUnder()
        {
            // Arrange
            _ticket.BetTeam = "du"; // Draw & Under
            _ticketHelper.GetTeamNameById(Arg.Any<int>()).ReturnsForAnyArgs(TeamName);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(string.Format("{0} & {1}", CoreBetList.draw, CoreBetList.under), _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamLengthIsNot2_BetTeamIsNotSet()
        {
            // Arrange
            _ticket.BetTeam = "a bet team"; // set bet team name by a string with length > 2
            var betTeam = _choice.Template.betTeam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(betTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_Always_SetBetTeamClassNameIsFavorite()
        {
            // Arrange
            _ticket.BetTeam = "a bet team"; // set bet team name by a string with length > 2
            var betTeam = _choice.Template.betTeam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("favorite", _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeam_Always_SetHandicapValueIsHandicap1()
        {
            // Arrange
            _ticket.Handicap1 = 12;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(ConvertByBetType.Hdp(_ticket.Handicap1), _choice.Template.Handicap.handicap);
        }
    }
}