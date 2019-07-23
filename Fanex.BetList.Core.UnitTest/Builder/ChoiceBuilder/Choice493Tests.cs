namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice493Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice493();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("aos")]
        [TestCase("Aos")]
        [TestCase("AOS")]
        public void BuildBetTeam_TicketBetTeamIsAOS_DisplayedBetTeamIsAOS(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(CoreBetList.AOS, template.betTeam);
        }

        [TestCase("1/2", "1", "2")]
        [TestCase("2 /10+", "2", "10+")]
        [TestCase("3/ 6", "3", "6")]
        [TestCase(" 4 / 9 ", "4", "9")]
        public void BuildBetTeam_TicketBetTeamContainsTwoTotalConersValues_DisplayHalfTimeTotalCornersAndFullTimeTotalCornersAsBetTeam(
            string betTeam,
            string expectedHalfTimeTotalCorners,
            string expectedFullTimeTotalCorners)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedDisplayedBetTeam = $"{CoreBetList.HalfTime} {expectedHalfTimeTotalCorners} / {CoreBetList.FullTime} {expectedFullTimeTotalCorners}";

            Assert.AreEqual(expectedDisplayedBetTeam, template.betTeam);
        }

        [TestCase("1/2/3")]
        [TestCase("anyotherscore")]
        [TestCase("1/")]
        [TestCase("2")]
        [TestCase("/3")]
        public void BuildBetTeam_TicketBetTeamIsIncorrectFormat_DisplayedBetTeamIsEmpty(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsEmpty(template.betTeam);
        }
    }
}