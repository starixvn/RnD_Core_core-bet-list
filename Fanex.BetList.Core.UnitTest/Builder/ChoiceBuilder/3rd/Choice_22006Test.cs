namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder._3rd
{
    using App_GlobalResources;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Core.Builder.ChoiceBuilder._3rd;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice_22006 class.
    /// </summary>
    [TestFixture]
    public class Choice_22006Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice_22006();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.PlayTechCasino;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.Score is always null.
        /// </summary>
        [Test]
        public void BuildScore_Always_SetScoreBlockIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Score);
        }

        /// <summary>
        /// The value of Template.League is always null.
        /// </summary>
        [Test]
        public void BuildLeague_Always_SetLeagueBlockIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.League);
        }

        /// <summary>
        /// The value of Template.betTeam is always play tech casino.
        /// </summary>
        [Test]
        public void BuildBetType_Always_SetBetTypeNameIsPlayTechCasioResource()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.playtechcasino, _choice.Template.BetType.betTypeName);
        }

        /// <summary>
        /// The value of Template.betTeam is always bet team.
        /// </summary>
        [Test]
        public void BuildBetTeam_Always_SetBetTeamIsBetTeam()
        {
            // Arrange
            _ticket.BetTeam = "bet team";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(_ticket.BetTeam, _choice.Template.betTeam);
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
        /// The value of Template.Handicap is always null.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapBlockIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Handicap);
        }

        /// <summary>
        /// The value of Template.Match is always null.
        /// </summary>
        [Test]
        public void BuildMatch_Always_SetMatchBlockIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Match);
        }
    }
}