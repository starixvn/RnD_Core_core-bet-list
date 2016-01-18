namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Common.Enums;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    ///  Unit test for Choice159.
    /// </summary>
    [TestFixture]
    public class Choice159Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice159();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes._3rdExactTotalGoals;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("g0", "0 Goals")]
        [TestCase("G0", "0 Goals")]
        [TestCase("g1", "1 Goal")]
        [TestCase("G1", "1 Goal")]
        [TestCase("g2", "2 Goals")]
        [TestCase("G2", "2 Goals")]
        [TestCase("g3", "3 Goals")]
        [TestCase("G3", "3 Goals")]
        [TestCase("g4", "4 Goals")]
        [TestCase("G4", "4 Goals")]
        [TestCase("g5", "5 Goals")]
        [TestCase("G5", "5 Goals")]
        [TestCase("g6", "6&Over")]
        [TestCase("G6", "6&Over")]
        [TestCase("abc", "")]
        [TestCase("", "")]
        public void BuildBetTeam_BetTeam_TemplateBetTeam(string betTeam, string expected)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

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
    }
}