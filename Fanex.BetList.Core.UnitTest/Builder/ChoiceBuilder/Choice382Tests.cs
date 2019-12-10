namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice382Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice382();
            _ticket = TestData.CreateTicket(BetTypes.HomePenaltyShootoutRoundX);
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("s")]
        [TestCase("S")]
        public void BuildBetTeam_BetTeamIsS_BetChoiceIsScore(string betToScore)
        {
            // Arrange.
            _ticket.BetTeam = betToScore;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(CoreBetList.Score, template.betTeam);
        }

        [TestCase("m")]
        [TestCase("M")]
        public void BuildBetTeam_BetTeamIsM_BetChoiceIsMiss(string betToMiss)
        {
            // Arrange.
            _ticket.BetTeam = betToMiss;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(CoreBetList.Miss, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsNotSOrM_BetChoiceIsEmpty()
        {
            // Arrange.
            const string invalidBetTeam = "A";

            _ticket.BetTeam = invalidBetTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsEmpty(template.betTeam);
        }
    }
}