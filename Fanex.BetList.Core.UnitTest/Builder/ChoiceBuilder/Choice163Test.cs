namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice163Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice163();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("HU")]
        [TestCase("hu")]
        [TestCase("hU")]
        public void BuildBetTeam_BetTeamIsHU_BetChoiceIsHomeSlashUnder(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expectation = "Home/Under";

            Assert.AreEqual(expectation, _choice.Template.betTeam);
        }

        [TestCase("HO")]
        [TestCase("ho")]
        [TestCase("hO")]
        public void BuildBetTeam_BetTeamIsHO_BetChoiceIsHomeSlashOver(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expectation = "Home/Over";

            Assert.AreEqual(expectation, _choice.Template.betTeam);
        }

        [TestCase("DU")]
        [TestCase("du")]
        [TestCase("dU")]
        public void BuildBetTeam_BetTeamIsDU_BetChoiceIsDrawSlashUnder(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expectation = "Draw/Under";

            Assert.AreEqual(expectation, _choice.Template.betTeam);
        }

        [TestCase("DO")]
        [TestCase("do")]
        [TestCase("dO")]
        public void BuildBetTeam_BetTeamIsDO_BetChoiceIsDrawSlashOver(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expectation = "Draw/Over";

            Assert.AreEqual(expectation, _choice.Template.betTeam);
        }

        [TestCase("AU")]
        [TestCase("au")]
        [TestCase("aU")]
        public void BuildBetTeam_BetTeamIsAU_BetChoiceIsAwaySlashUnder(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expectation = "Away/Under";

            Assert.AreEqual(expectation, _choice.Template.betTeam);
        }

        [TestCase("AO")]
        [TestCase("ao")]
        [TestCase("aO")]
        public void BuildBetTeam_BetTeamIsAO_BetChoiceIsAwaySlashOver(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expectation = "Away/Over";

            Assert.AreEqual(expectation, _choice.Template.betTeam);
        }
    }
}