namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice383Tests
    {
        private const int HomeTeamId = 1;
        private const int AwayTeamId = 2;

        private const string HomeTeamName = "Home";
        private const string AwayTeamName = "Away";

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice383();
            _ticket = TestData.CreateTicket(BetTypes.PenaltyShootoutWoodworkRoundX);
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.HomeId = HomeTeamId;
            _ticket.AwayId = AwayTeamId;

            _ticketHelper.GetTeamNameById(_ticket.HomeId).Returns(HomeTeamName);
            _ticketHelper.GetTeamNameById(_ticket.AwayId).Returns(AwayTeamName);
        }

        [TestCase("h")]
        [TestCase("H")]
        public void BuildBetTeam_BetTeamIsH_BetChoiceIsHomeTeamName(string betToHome)
        {
            // Arrange.
            _ticket.BetTeam = betToHome;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(HomeTeamName, template.betTeam);
        }

        [TestCase("a")]
        [TestCase("A")]
        public void BuildBetTeam_BetTeamIsA_BetChoiceIsAwayTeamName(string betToAway)
        {
            // Arrange.
            _ticket.BetTeam = betToAway;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(AwayTeamName, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsNotHOrA_BetChoiceIsEmpty()
        {
            // Arrange.
            const string invalidBetTeam = "1";

            _ticket.BetTeam = invalidBetTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsEmpty(template.betTeam);
        }
    }
}