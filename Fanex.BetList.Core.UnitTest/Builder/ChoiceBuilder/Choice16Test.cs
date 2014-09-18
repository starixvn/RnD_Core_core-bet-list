namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice16 class.
    /// </summary>
    [TestFixture]
    public class Choice16Test
    {
        private static object[] expectedBetTeams =
        {
            new object[] { "0:0", "DD" },
            new object[] { "0:1", "DH" },
            new object[] { "0:2", "DA" },
            new object[] { "1:0", "HD" },
            new object[] { "1:1", "HH" },
            new object[] { "1:2", "HA" },
            new object[] { "2:0", "AD" },
            new object[] { "2:1", "AH" },
            new object[] { "2:2", "AA" }
        };

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice16();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.HT_FT;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.ticketStatus is always void when Ticket.Status = void.
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
        /// The value of Template.betTeam is always bet team name.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIsEmpty_SetBetTeamIsEmpty()
        {
            // Arrange
            _ticket.BetTeam = string.Empty;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsEmpty(_choice.Template.betTeam);
        }

        [Test, TestCaseSource("expectedBetTeams")]
        public void BuildBetTeam_Always_SetRightBetTeam(string betTeam, string expectedResult)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedResult, _choice.Template.betTeam);
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