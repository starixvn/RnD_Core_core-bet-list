namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using System;
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice1231Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1231();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildMatch_Always_SetMatchVSBlockIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(_choice.Template.Match.VS);
        }

        [Test]
        public void BuildMatch_Always_SetHomeFGLGIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(_choice.Template.Match.home_firstGoal_lastGoal);
        }

        [Test]
        public void BuildMatch_Always_SetAwayFGLGIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(_choice.Template.Match.away_firstGoal_lastGoal);
        }

        [Test]
        public void BuildMatch_Always_SetAwayTeamIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(_choice.Template.Match.awayTeam);
        }

        [Test]
        public void BuildMatch_ShowTimeIsValidFormat_HomeTeamIsShowTimeBeFormatted()
        {
            // Arrange
            _ticket.ShowTime = "Apr 15 2010 12:00AM";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expetedShowTime = DateTime.Parse(_ticket.ShowTime).ToString("M/d/yyyy hh:mm tt");
            Assert.AreEqual(expetedShowTime, _choice.Template.Match.homeTeam);
        }

        [Test]
        public void BuildMatch_ShowTimeIsInValidFormat_HomeTeamIsShowTime()
        {
            // Arrange
            _ticket.ShowTime = string.Empty;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsEmpty(_choice.Template.Match.homeTeam);
        }

        [Test]
        public void BuildBetTeam_Always_SetBetTeamAreMatchCodeAndHorseTeamName()
        {
            // Arrange
            var horseTeamName = "Horse team name";
            _ticket.HomeId = 1;
            _ticket.AwayId = 2;
            _ticket.MatchCode = "100";
            _ticketHelper.GetHorseTeamNameById(Arg.Any<object>()).Returns(horseTeamName);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeam = string.Format("{0} - {1}", _ticket.MatchCode, horseTeamName);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string CSS_CLASS_FAVORITE = "favorite";
            Assert.AreEqual(CSS_CLASS_FAVORITE, _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        [Test]
        public void CalculateFG_LG_Excel_Always_SetHomeIsEmpty()
        {
            // Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, false, new RTFHelper().Clone());

            // Assert
            Assert.IsEmpty(_choice.Template.Match.home_firstGoal_lastGoal);
        }

        [Test]
        public void CalculateFG_LG_Excel_Always_SetAwayIsEmpty()
        {
            // Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, false, new RTFHelper().Clone());

            // Assert
            Assert.IsEmpty(_choice.Template.Match.away_firstGoal_lastGoal);
        }
    }
}