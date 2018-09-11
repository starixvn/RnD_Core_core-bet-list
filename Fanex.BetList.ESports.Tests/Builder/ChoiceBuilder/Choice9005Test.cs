namespace Fanex.BetList.ESports.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice9005Test
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IChoice _choice;

        [SetUp]
        public void Setup()
        {
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket = Substitute.For<ITicket>();
            _choice = new Choice9005();
        }

        [TestCase("O")]
        [TestCase("o")]
        public void BuildBetTeam_TicketBetTeamIsO_ShowOddAsBetTeam(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string expectedBetTeam = "Odd";

            Assert.AreEqual(expectedBetTeam, template.betTeam);
        }

        [TestCase("E")]
        [TestCase("e")]
        public void BuildBetTeam_TicketBetTeamIsE_ShowEvenAsBetTeam(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string expectedBetTeam = "Even";

            Assert.AreEqual(expectedBetTeam, template.betTeam);
        }

        [TestCase("o")]
        [TestCase("O")]
        public void BuildBetTeamClassNameAndHandicap_TicketBetTeamIsO_BetTeamClassNameIsFavorite(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string expectedClass = "favorite";

            Assert.AreEqual(expectedClass, template.betTeamClassName);
        }

        [TestCase("E")]
        [TestCase("e")]
        public void BuildBetTeamClassNameAndHandicap_TicketBetTeamIsE_BetTeamClassNameIsUnderdog(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string expectedClass = "underdog";

            Assert.AreEqual(expectedClass, template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetNullToHandicap()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.Handicap.handicap);
        }
    }
}