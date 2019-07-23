namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice494Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice494();
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

        [Test]
        public void BuildBetTeam_TicketBetTeamIsNotAOS_DisplayedBetTeamIsTicketBetTeam()
        {
            // Arrange.
            const string betTeam = "1-0";

            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(betTeam, template.betTeam);
        }
    }
}