namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

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
            _ticket.BetTypeId = BetTypes.WinningMarginOwn;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_BetTeamIsInvalid_BetChoiceIsEmpty()
        {
            // Arrange.
            _ticket.BetTeam = "abc";

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.IsEmpty(_choice.Template.betTeam);
        }

        [TestCase("H1", "Home to Win by 1 Goal")]
        [TestCase("H2", "Home to Win by 2 Goals")]
        [TestCase("H3", "Home to Win by 3+ Goals")]
        [TestCase("h1", "Home to Win by 1 Goal")]
        [TestCase("h2", "Home to Win by 2 Goals")]
        [TestCase("h3", "Home to Win by 3+ Goals")]
        public void BuildBetTeam_BetTeamStartsWithH_BetChoiceStartsWithHome(string betTeam, string expected)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(expected, _choice.Template.betTeam);
        }

        [TestCase("A1", "Away to Win by 1 Goal")]
        [TestCase("A2", "Away to Win by 2 Goals")]
        [TestCase("A3", "Away to Win by 3+ Goals")]
        [TestCase("a1", "Away to Win by 1 Goal")]
        [TestCase("a2", "Away to Win by 2 Goals")]
        [TestCase("a3", "Away to Win by 3+ Goals")]
        public void BuildBetTeam_BetTeamStartsWithA_BetChoiceStartsWithAway(string betTeam, string expected)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(expected, _choice.Template.betTeam);
        }

        [TestCase("D")]
        [TestCase("d")]
        public void BuildBetTeam_BetTeamStartsWithD_BetChoiceIsAnyScoreDraw(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string expectation = "Any Score Draw (Exclude 0-0)";

            Assert.AreEqual(expectation, _choice.Template.betTeam);
        }

        [TestCase("NG")]
        [TestCase("ng")]
        [TestCase("nG")]
        [TestCase("Ng")]
        public void BuildBetTeam_BetTeamStartsWithNG_BetChoiceIsNoGoal(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(CoreBetList.NoGoal408, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_HandicapIsNull()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_BetTeamClassNameIsFavorite()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string favorite = "favorite";

            Assert.AreEqual(favorite, _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetType_Always_ShowParentBetTypeName()
        {
            // Arrange.
            var parentBetTypeId = BetTypes.ExactHomeTeamGoals.ToString();
            var parentBetTypeName = "Parent Bet Type";

            _ticketHelper.GetParentIdByBetTypeId(Arg.Any<object>()).Returns(parentBetTypeId);
            _ticketHelper.GetBetTypeNameById(parentBetTypeId, Arg.Any<object>()).Returns(parentBetTypeName);

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(parentBetTypeName, _choice.Template.BetType.betTypeName);
        }
    }
}