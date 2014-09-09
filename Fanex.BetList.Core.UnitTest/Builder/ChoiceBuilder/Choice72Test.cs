namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice72 class.
    /// </summary>
    [TestFixture]
    public class Choice72Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice72();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.P2P_Games;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.BetType.betTypeName is always null.
        /// </summary>
        [Test]
        public void BuildBetType_Always_SetBetTypeNameIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.BetType.betTypeName);
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
        /// The value of Template.League.sportTypeName is always null.
        /// </summary>
        [Test]
        public void BuildSport_Always_SetSportTypeNameIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.League.sportTypeName);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void BuildScore_Always_SetScoreBlockIsNull(bool isLive)
        {
            // Arrange
            _ticket.IsLive = isLive;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Score);
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
        /// The value of Template.League.LeagueName is always null.
        /// </summary>
        [Test]
        public void BuildLeauge_Always_SetLeagueNameIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.League.LeagueName.leagueName);
        }

        /// <summary>
        /// The value of Template.betTeam is always Casino Progressive Bonus.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIsNullBetTeamIsCasinoProgressiveBonusResource()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.casinoProgressiveBonus, _choice.Template.betTeam);
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
    }
}