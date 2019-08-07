namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice479Tests
    {
        private const decimal PredefinedMinute = 10;

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice479();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.Handicap1 = PredefinedMinute;
        }

        [TestCase("y")]
        [TestCase("Y")]
        public void BuildBetTeam_TicketBetTeamIsY_DisplayedBetTeamIsCornerBeforePredefinedMinute(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedBetOption = string.Format(CoreBetList.CornerBefore, PredefinedMinute);

            Assert.AreEqual(expectedBetOption, template.betTeam);
        }

        [TestCase("n")]
        [TestCase("N")]
        [TestCase("a")]
        [TestCase("H")]
        public void BuildBetTeam_TicketBetTeamIsAnyOtherValue_DisplayedBetTeamIsNoCornerBeforePredefinedMinute(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedBetOption = string.Format(CoreBetList.NoCornerBefore, PredefinedMinute);

            Assert.AreEqual(expectedBetOption, template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_HideHandicap()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(_choice.Template.Handicap);
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