namespace Fanex.BetList.ESports.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice9087Test
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IChoice _choice;

        [SetUp]
        public void Setup()
        {
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket = Substitute.For<ITicket>();
            _choice = new Choice9087();
        }

        [TestCase("Y")]
        [TestCase("y")]
        public void BuildBetTeam_TicketBetTeamIsY_ShowYesAsBetTeam(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual("Yes", template.betTeam);
        }

        [TestCase("N")]
        [TestCase("n")]
        public void BuildBetTeam_TicketBetTeamIsN_ShowNoAsBetTeam(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual("No", template.betTeam);
        }

        [TestCase("Y")]
        [TestCase("y")]
        [TestCase("n")]
        [TestCase("N")]
        public void BuildBetTeamClassNameAndHandicap_Always_BetTeamClassNameIsFavorite(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expectedClass = "favorite";

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