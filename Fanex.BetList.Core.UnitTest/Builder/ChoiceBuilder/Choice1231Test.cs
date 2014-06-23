namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using System;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice1231 class.
    /// </summary>
    [TestFixture]
    public class Choice1231Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1231();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.VHR_Win;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.Match.VS is always null.
        /// </summary>
        [Test]
        public void BuildMatch_Always_SetMatchVSBlockIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Match.VS);
        }

        /// <summary>
        /// The value of Template.Match.home_firstGoal_lastGoal is always null.
        /// </summary>
        [Test]
        public void BuildMatch_Always_SetHomeFGLGIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Match.home_firstGoal_lastGoal);
        }

        /// <summary>
        /// The value of Template.Match.away_firstGoal_lastGoal is always null.
        /// </summary>
        [Test]
        public void BuildMatch_Always_SetAwayFGLGIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Match.away_firstGoal_lastGoal);
        }

        /// <summary>
        /// The value of Template.Match.awayTeam is always null.
        /// </summary>
        [Test]
        public void BuildMatch_Always_SetAwayTeamIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Match.awayTeam);
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
        /// The value of Template.League.LeagueName.leagueName is always league name get by identifier.
        /// </summary>
        [Test]
        public void BuildLeague_Always_SetLeagueNameIsLeagueName()
        {
            // Arrange
            const string LEAGUE_NAME = "League name";
            _ticketHelper.GetLeagueNameById(Arg.Any<int>()).Returns<string>(LEAGUE_NAME);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(LEAGUE_NAME, _choice.Template.League.LeagueName.leagueName);
        }

        /// <summary>
        /// The value of Template.betTeam is always Ticket.MatchCode - HorseTeamName.
        /// </summary>
        [Test]
        public void BuildBetTeam_Always_SetBetTeamAreMatchCodeAndHorseTeamName()
        {
            // Arrange
            _ticket.HomeId = 1;
            _ticket.AwayId = 2;
            _ticket.MatchCode = "100";
            _ticketHelper.GetHorseTeamNameById(Arg.Any<long>(), Arg.Any<long>()).Returns<string>("Horse Team Name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = string.Format("{0} - {1}", _ticket.MatchCode, _ticketHelper.GetHorseTeamNameById(_ticket.HomeId, _ticket.AwayId));
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
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
            const string CSS_CLASS_FAVORITE = "favorite";
            Assert.AreEqual(CSS_CLASS_FAVORITE, _choice.Template.betTeamClassName);
        }

        /// <summary>
        /// The value of Template.Match.homeTeam is always Ticket.ShowTime when show time is valid format.
        /// </summary>
        [Test]
        public void BuildMatch_ShowTimeIsValidFormat_HomeTeamIsShowTimeBeFormatted()
        {
            // Arrange
            _ticket.ShowTime = "01/01/2014 15:25";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expetedShowTime = DateTime.Parse(_ticket.ShowTime).ToString("M/d/yyyy hh:mm");
            Assert.AreEqual(expetedShowTime, _choice.Template.Match.homeTeam);
        }

        /// <summary>
        /// The value of Template.Match.homeTeam is always Ticket.ShowTime when show time is invalid format.
        /// </summary>
        [Test]
        public void BuildMatch_ShowTimeIsInValidFormat_HomeTeamIsShowTime()
        {
            // Arrange
            _ticket.ShowTime = string.Empty;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsEmpty(_choice.Template.Match.homeTeam);
        }
    }
}