namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice24 class.
    /// </summary>
    [TestFixture]
    public class Choice24Test
    {
        private const string BetTeamTemplate = " <span class=\"favorite\">{0}</span>&nbsp;{1} <span class=\"favorite\">&nbsp;{2}</span>";
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice24();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.Double_Change;
            _ticketHelper = Substitute.For<ITicketHelper>();
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

        /// <summary>
        /// The value of Template.betTeam is always Home or Draw.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs1x_BetTeamAreHomeTeamNameAndOrAndDrawResource()
        {
            // Arrange
            _ticket.BetTeam = "1x";
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns("Home team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = string.Format(BetTeamTemplate, _choice.Template.Match.homeTeam, CoreBetList.lblOr.ToLower(), CoreBetList.lblDraw);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always Away or Draw.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs2x_BetTeamAreAwayTeamNameAndOrAndDrawResource()
        {
            // Arrange
            _ticket.BetTeam = "2x";
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns("Away team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = string.Format(BetTeamTemplate, _choice.Template.Match.awayTeam, CoreBetList.lblOr.ToLower(), CoreBetList.lblDraw);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always Home or Away.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs12_BetTeamAreHomeTeamNameAndOrAndAwayTeamName()
        {
            // Arrange
            _ticket.BetTeam = "12";
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns("Team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = string.Format(BetTeamTemplate, _choice.Template.Match.homeTeam, CoreBetList.lblOr.ToLower(), _choice.Template.Match.awayTeam);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }
    }
}