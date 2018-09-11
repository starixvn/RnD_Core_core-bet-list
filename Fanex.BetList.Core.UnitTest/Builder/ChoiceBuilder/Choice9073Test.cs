namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Core.App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice9073Test
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IChoice _choice;

        [SetUp]
        public void Setup()
        {
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket = Substitute.For<ITicket>();
            _choice = new Choice9073();
        }

        [TestCase("Y")]
        [TestCase("y")]
        public void BuildBetTeam_TicketBetTeamIsY_ShowYesAsBetTeam(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(CoreBetList.lblYes, template.betTeam);
        }

        [TestCase("N")]
        [TestCase("n")]
        public void BuildBetTeam_TicketBetTeamIsN_ShowNoAsBetTeam(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(CoreBetList.lblNo, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_TicketBetTeamIsNotYAndNotN_ShowEmptyStringAsBetTeam()
        {
            // Arrange.
            _ticket.BetTeam = "any string";

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsEmpty(template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_BetTeamClassNameIsFavorite()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

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