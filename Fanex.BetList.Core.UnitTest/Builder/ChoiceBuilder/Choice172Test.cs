namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice172Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice172();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
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

        [TestCase("HH")]
        [TestCase("hh")]
        public void BuildBetTeam_BetTeamIsHH_BetChoiceIsHomeSlashHome(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expected = "Home/Home";

            Assert.AreEqual(expected, _choice.Template.betTeam);
        }

        [TestCase("HA")]
        [TestCase("ha")]
        [TestCase("Ha")]
        [TestCase("hA")]
        public void BuildBetTeam_BetTeamIsHA_BetChoiceIsHomeSlashAway(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expected = "Home/Away";

            Assert.AreEqual(expected, _choice.Template.betTeam);
        }

        [TestCase("DH")]
        [TestCase("dh")]
        [TestCase("dH")]
        [TestCase("Dh")]
        public void BuildBetTeam_BetTeamIsDH_BetChoiceIsDrawSlashHome(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expected = "Draw/Home";

            Assert.AreEqual(expected, _choice.Template.betTeam);
        }

        [TestCase("AA")]
        [TestCase("aa")]
        public void BuildBetTeam_BetTeamIsAA_BetChoiceIsAwaySlashAway(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expected = "Away/Away";

            Assert.AreEqual(expected, _choice.Template.betTeam);
        }

        [TestCase("AH")]
        [TestCase("ah")]
        [TestCase("aH")]
        [TestCase("Ah")]
        public void BuildBetTeam_BetTeamIsAH_BetChoiceIsAwaySlashHome(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expected = "Away/Home";

            Assert.AreEqual(expected, _choice.Template.betTeam);
        }

        [TestCase("DA")]
        [TestCase("da")]
        [TestCase("dA")]
        [TestCase("Da")]
        public void BuildBetTeam_BetTeamIsDA_BetChoiceIsDrawSlashAway(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expected = "Draw/Away";

            Assert.AreEqual(expected, _choice.Template.betTeam);
        }

        [TestCase("NO")]
        [TestCase("no")]
        [TestCase("nO")]
        [TestCase("No")]
        public void BuildBetTeam_BetTeamIsNO_BetChoiceIsNone(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expected = "None";

            Assert.AreEqual(expected, _choice.Template.betTeam);
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