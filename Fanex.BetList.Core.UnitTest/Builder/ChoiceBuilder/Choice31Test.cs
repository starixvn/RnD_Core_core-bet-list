namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice31Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice31();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.Win;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildMatch_Always_SetMatchVSBlockIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Match.VS);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildMatch_Always_SetHomeFGLGIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Match.home_firstGoal_lastGoal);
        }

        [Test]
        public void BuildMatch_Always_SetAwayTeamIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Match.awayTeam);
        }

        [Test]
        public void BuildMatch_Always_SetAwayFGLGIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Match.away_firstGoal_lastGoal);
        }

        [Test]
        public void BuildBetTeam_Always_SetBetTeamIsMatchCodeAndHorseTeamName()
        {
            // Arrange
            _ticket.MatchCode = "100";
            _ticket.HomeId = 1;
            _ticket.AwayId = 2;
            _ticketHelper.GetHorseTeamNameById(Arg.Any<long>()).Returns("Horse team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = string.Format("{0} - {1}", _ticket.MatchCode, _ticketHelper.GetHorseTeamNameById(_ticket.HomeId));
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_Always_SetBetTeamIsRaceResourceAndRace()
        {
            // Arrange
            _ticket.Race = "1";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = string.Format("{0}&nbsp;{1}", CoreBetList.race, _ticket.Race);
            Assert.AreEqual(expectedBetTeam, _choice.Template.Match.homeTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string CSS_CLASS_FAVORITE = "favorite";
            Assert.AreEqual(CSS_CLASS_FAVORITE, _choice.Template.betTeamClassName);
        }
    }
}