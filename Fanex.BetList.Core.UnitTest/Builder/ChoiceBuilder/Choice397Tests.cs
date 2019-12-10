namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice397Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice397();
            _ticket = TestData.CreateTicket(BetTypes.ExtraTimeWinningMargin);
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_BetTeamIsNotInDefinedList_BetChoiceIsEmpty()
        {
            // Arrange.
            const string undefinedBetTeam = "xyz";

            _ticket.BetTeam = undefinedBetTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsEmpty(template.betTeam);
        }

        [TestCase("H1", "Home to Win by 1 Goal")]
        [TestCase("H2", "Home to Win by 2 Goals")]
        [TestCase("H3+", "Home to Win by 3+ Goals")]
        [TestCase("h1", "Home to Win by 1 Goal")]
        [TestCase("h2", "Home to Win by 2 Goals")]
        [TestCase("h3+", "Home to Win by 3+ Goals")]
        public void BuildBetTeam_BetTeamStartsWithH_BetChoiceStartsWithHome(string betToHome, string expected)
        {
            // Arrange.
            _ticket.BetTeam = betToHome;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(expected, template.betTeam);
        }

        [TestCase("A1", "Away to Win by 1 Goal")]
        [TestCase("A2", "Away to Win by 2 Goals")]
        [TestCase("A3+", "Away to Win by 3+ Goals")]
        [TestCase("a1", "Away to Win by 1 Goal")]
        [TestCase("a2", "Away to Win by 2 Goals")]
        [TestCase("a3+", "Away to Win by 3+ Goals")]
        public void BuildBetTeam_BetTeamStartsWithA_BetChoiceStartsWithAway(string betToAway, string expected)
        {
            // Arrange.
            _ticket.BetTeam = betToAway;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(expected, template.betTeam);
        }

        [TestCase("D")]
        [TestCase("d")]
        public void BuildBetTeam_BetTeamIsD_BetChoiceIsAnyScoreDraw(string betToScoreDraw)
        {
            // Arrange.
            _ticket.BetTeam = betToScoreDraw;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(CoreBetList.ScoreDraw, template.betTeam);
        }

        [TestCase("NG")]
        [TestCase("ng")]
        [TestCase("nG")]
        [TestCase("Ng")]
        public void BuildBetTeam_BetTeamIsNG_BetChoiceIsNoGoal(string betToNoGoal)
        {
            // Arrange.
            _ticket.BetTeam = betToNoGoal;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(CoreBetList.NoGoal408, template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_HandicapIsNull()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_BetTeamClassNameIsFavorite()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string favorite = "favorite";

            Assert.AreEqual(favorite, template.betTeamClassName);
        }
    }
}