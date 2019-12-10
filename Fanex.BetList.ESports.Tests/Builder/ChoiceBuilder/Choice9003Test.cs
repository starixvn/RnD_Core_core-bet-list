namespace Fanex.BetList.ESports.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using Core.Utils;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice9003Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice9003();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("O")]
        [TestCase("o")]
        public void BuildBetTeam_TicketBetTeamIsO_ShowOverAsBetTeam(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expectedBetTeam = "Over";

            Assert.AreEqual(expectedBetTeam, template.betTeam);
        }

        [TestCase("U")]
        [TestCase("u")]
        public void BuildBetTeam_TicketBetTeamIsU_ShowUnderAsBetTeam(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expectedBetTeam = "Under";

            Assert.AreEqual(expectedBetTeam, template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_HandicapIsHandicap1()
        {
            // Arrange.
            _ticket.Handicap1 = 1;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(ConvertByBetType.Hdp(_ticket.Handicap1), template.Handicap.handicap);
        }

        [TestCase("o")]
        [TestCase("O")]
        public void BuildBetTeamClassNameAndHandicap_TicketBetTeamIsO_BetTeamClassNameIsFavorite(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expectedClass = "favorite";

            Assert.AreEqual(expectedClass, template.betTeamClassName);
        }

        [TestCase("u")]
        [TestCase("U")]
        public void BuildBetTeamClassNameAndHandicap_TicketBetTeamIsU_BetTeamClassNameIsUnderdog(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expectedClass = "underdog";

            Assert.AreEqual(expectedClass, template.betTeamClassName);
        }
    }
}