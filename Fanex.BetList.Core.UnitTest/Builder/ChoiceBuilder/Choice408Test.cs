namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Common.Enums;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    ///  Unit test for Choice171.
    /// </summary>
    [TestFixture]
    public class Choice408Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice408();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.WinningMargin;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// Builds the bet team_ bet team_ template bet team.
        /// </summary>
        /// <param name="betTeam">The bet team.</param>
        /// <param name="expected">The expected.</param>
        [TestCase("H1", "Home team name to Win by 1 Goal")]
        [TestCase("H2", "Home team name to Win by 2 Goals")]
        [TestCase("H3", "Home team name to Win by 3up Goals")]
        [TestCase("D", "S.Draw")]
        [TestCase("A1", "Away team name to Win by 1 Goal")]
        [TestCase("A2", "Away team name to Win by 2 Goals")]
        [TestCase("A3", "Away team name to Win by 3up Goals")]
        [TestCase("NG", "No Goal")]
        [TestCase("h1", "Home team name to Win by 1 Goal")]
        [TestCase("h2", "Home team name to Win by 2 Goals")]
        [TestCase("h3", "Home team name to Win by 3up Goals")]
        [TestCase("d", "S.Draw")]
        [TestCase("a1", "Away team name to Win by 1 Goal")]
        [TestCase("a2", "Away team name to Win by 2 Goals")]
        [TestCase("a3", "Away team name to Win by 3up Goals")]
        [TestCase("ng", "No Goal")]
        [TestCase("abc", "")]
        public void BuildBetTeam_BetTeam_TemplateBetTeam(string betTeam, string expected)
        {
            // Arrange
            _ticket.HomeId = 123;
            _ticket.AwayId = 456;
            _ticket.BetTeam = betTeam;
            _ticketHelper.GetTeamNameById(_ticket.HomeId).Returns<string>("Home team name");
            _ticketHelper.GetTeamNameById(_ticket.AwayId).Returns<string>("Away team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(expected, _choice.Template.betTeam);
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
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string CSS_CLASS_FAVORITE = "favorite";
            Assert.AreEqual(CSS_CLASS_FAVORITE, _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetType_Always_ShowParentBetTypeName()
        {
            // Arrange.
            var parentBetTypeId = BetTypes._3rdExactHomeTeamGoals.ToString();
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