namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice171Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice171();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.WinningMargin;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("H1", "Home to Win by 1 Goal")]
        [TestCase("H2", "Home to Win by 2 Goals")]
        [TestCase("H3", "Home to Win by 3+ Goals")]
        [TestCase("h1", "Home to Win by 1 Goal")]
        [TestCase("h2", "Home to Win by 2 Goals")]
        [TestCase("h3", "Home to Win by 3+ Goals")]
        public void BuildBetTeam_BetTeamStartsWithH_BetChoiceStartsWithHome(string homeBetTeam, string expectedBetChoice)
        {
            // Arrange.
            _ticket.BetTeam = homeBetTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(expectedBetChoice, _choice.Template.betTeam);
        }

        [TestCase("A1", "Away to Win by 1 Goal")]
        [TestCase("A2", "Away to Win by 2 Goals")]
        [TestCase("A3", "Away to Win by 3+ Goals")]
        [TestCase("a1", "Away to Win by 1 Goal")]
        [TestCase("a2", "Away to Win by 2 Goals")]
        [TestCase("a3", "Away to Win by 3+ Goals")]
        public void BuildBetTeam_BetTeamStartsWithA_BetChoiceStartsWithAway(string awayBetTeam, string expectedBetChoice)
        {
            // Arrange.
            _ticket.BetTeam = awayBetTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(expectedBetChoice, _choice.Template.betTeam);
        }

        [TestCase("d")]
        [TestCase("D")]
        public void BuildBetTeam_BetTeamStartsWithD_BetChoiceIsDraw(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expectedBetChoice = "Draw";

            Assert.AreEqual(expectedBetChoice, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsInvalid_BetChoiceIsEmpty()
        {
            // Arrange.
            _ticket.BetTeam = "abc";

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsEmpty(_choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_HandicapIsNull()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_BetTeamClassNameIsFavorite()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string favorite = "favorite";

            Assert.AreEqual(favorite, _choice.Template.betTeamClassName);
        }
    }
}