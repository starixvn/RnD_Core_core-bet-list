namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice426Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice426();
            _ticket = Substitute.For<ITicket>();
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

        [TestCase("H1")]
        [TestCase("h1")]
        public void BuildBetTeam_BetTeamIsH1_BetChoiceIsHomeToWinBy1Goal(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string expectation = "Home to Win by 1 Goal";

            Assert.AreEqual(expectation, _choice.Template.betTeam);
        }

        [TestCase("H2+")]
        [TestCase("h2+")]
        public void BuildBetTeam_BetTeamIsH2Plus_BetChoiceIsHomeToWinBy1Goal(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string expectation = "Home to Win by 2+ Goals";

            Assert.AreEqual(expectation, _choice.Template.betTeam);
        }

        [TestCase("d")]
        [TestCase("D")]
        public void BuildBetTeam_BetTeamIsD_BetChoiceIsAnyScoreDraw(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string expectation = "Any Score Draw (Exclude 0-0)";

            Assert.AreEqual(expectation, _choice.Template.betTeam);
        }

        [TestCase("A1")]
        [TestCase("a1")]
        public void BuildBetTeam_BetTeamIsA1_BetChoiceIsAwayToWinBy1Goal(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string expectation = "Away to Win by 1 Goal";

            Assert.AreEqual(expectation, _choice.Template.betTeam);
        }

        [TestCase("A2+")]
        [TestCase("a2+")]
        public void BuildBetTeam_BetTeamIsA2Plus_BetChoiceIsHomeToWinBy1Goal(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string expectation = "Away to Win by 2+ Goals";

            Assert.AreEqual(expectation, _choice.Template.betTeam);
        }

        [TestCase("NG")]
        [TestCase("ng")]
        [TestCase("nG")]
        [TestCase("Ng")]
        public void BuildBetTeam_BetTeamIsNG_BetChoiceIsNoGoal(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(CoreBetList.NoGoal408, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string favorite = "favorite";

            Assert.AreEqual(favorite, _choice.Template.betTeamClassName);
        }
    }
}