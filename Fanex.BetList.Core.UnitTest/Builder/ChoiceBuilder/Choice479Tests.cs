namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice479Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice479();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("y")]
        [TestCase("Y")]
        public void BuildBetTeam_TicketBetTeamIsY_DisplayedBetTeamIsCornerBefore(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(CoreBetList.CornerBefore, template.betTeam);
        }

        [TestCase("n")]
        [TestCase("N")]
        [TestCase("a")]
        [TestCase("H")]
        public void BuildBetTeam_TicketBetTeamIsAnyOtherValue_DisplayedBetTeamIsNoCornerBefore(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(CoreBetList.NoCornerBefore, template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_DisplayPredefinedMinuteAsHandicap()
        {
            // Arrange.
            const decimal predefinedMinute = 10;

            _ticket.Handicap1 = predefinedMinute;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedHandicap = $"{ConvertByBetType.Hdp(predefinedMinute)}:00";

            Assert.AreEqual(expectedHandicap, _choice.Template.Handicap.handicap);
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