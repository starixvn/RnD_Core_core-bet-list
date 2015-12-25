namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice410Test
    {
        private const string BetTeamTemplate = " <span class=\"favorite\">{0}</span>&nbsp;{1} <span class=\"favorite\">&nbsp;{2}</span>";
        private ITicket _ticket;
        private IChoice _choice;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void SetUp()
        {
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes._1stHalfDoubleChance;
            _ticketHelper = Substitute.For<ITicketHelper>();
            _choice = new Choice410();
        }

        [Test]
        public void BuildBetTeam_BetTeamIs1x_ShowBetTeamIsHomeTeamNameOrDraw()
        {
            // Arrange
            var homeTeamName = "Home team name";
            _ticket.BetTeam = "1x";
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns(homeTeamName);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expected = string.Format(BetTeamTemplate, _ticketHelper.GetTeamNameById(_ticket.HomeId), CoreBetList.lblOr, CoreBetList.lblDraw);
            Assert.AreEqual(expected, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIs2x_ShowBetTeamIsAwayTeamNameOrDraw()
        {
            // Arrange
            var awayTeamName = "Away team name";
            _ticket.BetTeam = "2x";
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns(awayTeamName);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expected = string.Format(BetTeamTemplate, _ticketHelper.GetTeamNameById(_ticket.AwayId), CoreBetList.lblOr, CoreBetList.lblDraw);
            Assert.AreEqual(expected, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIs12_ShowBetTeamIsHomeTeamNameOrAwayTeamName()
        {
            // Arrange
            var teamName = "Team name";
            _ticket.BetTeam = "12";
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns(teamName);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string homeTeamName = _ticketHelper.GetTeamNameById(_ticket.HomeId);
            string awayTeamName = _ticketHelper.GetTeamNameById(_ticket.AwayId);
            string expected = string.Format(BetTeamTemplate, homeTeamName, CoreBetList.lblOr, awayTeamName);
            Assert.AreEqual(expected, _choice.Template.betTeam);
        }


        [Test]
        public void BuildBetType_Always_ShowParentBetTypeName()
        {
            // Arrange.
            var parentBetTypeId = BetTypes._3rd1stHalfDoubleChance.ToString();
            var parentBetTypeName = "Parent Bet Type";
            _ticketHelper.GetParentIdByBetTypeId(Arg.Any<object>()).Returns(parentBetTypeId);
            _ticketHelper.GetBetTypeNameById(parentBetTypeId).Returns(parentBetTypeName);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(parentBetTypeName, _choice.Template.BetType.betTypeName);
        }
    }
}