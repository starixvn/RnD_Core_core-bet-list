namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common.Enums;
    using NSubstitute;
    using NUnit.Framework;
    using Fanex.BetList.Core.App_GlobalResources;

    /// <summary>
    /// Unit test for Choice73 class.
    /// </summary>
    [TestFixture]
    public class Choice73Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice73();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.Bingo;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.BetType.betTypeName is always null.
        /// </summary>
        [Test]
        public void BuildChoice_Ticket_BetTypeNameIsNull()
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
        public void BuildChoice_Ticket_MatchVSIsNull()
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
        public void BuildChoice_Ticket_SportTypeNameIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.League.sportTypeName);
        }

        /// <summary>
        /// The value of Template.Score is always null.
        /// </summary>
        [TestCase(true)]
        [TestCase(false)]
        public void BuildChoice_Ticket_ScoreBlockIsNull(bool isLive)
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
        public void BuildChoice_Ticket_HandicapIsNull()
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
        public void BuildChoice_Ticket_HomeFGLGIsNull()
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
        public void BuildChoice_Ticket_AwayTeamIsNull()
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
        public void BuildChoice_Ticket_AwayFGLGIsNull()
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
        public void BuildChoice_Ticket_LeagueNameIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.League.LeagueName.leagueName);
        }

        /// <summary>
        /// The value of Template.betTeam is always Bingo.
        /// </summary>
        [Test]
        public void BuildChoice_BetTeamOfTicketIsNull_BetTeamIsBingoResource()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.bingo, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is always favorite.
        /// </summary>
        [Test]
        public void BuildChoice_Ticket_BetTeamClassNameIsFavorite()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("favorite", _choice.Template.betTeamClassName);
        }
    }
}