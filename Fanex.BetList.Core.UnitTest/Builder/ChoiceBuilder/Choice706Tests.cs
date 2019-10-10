namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice706Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice706();
            _ticket = TestData.CreateTicket(BetTypes.MatchPointOddEven);
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("o")]
        [TestCase("O")]
        public void BuildBetTeam_TicketBetTeamIsO_DisplayedBetTeamIsOdd(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(CoreBetList.odd, template.betTeam);
        }

        [TestCase("e")]
        [TestCase("E")]
        public void BuildBetTeam_TicketBetTeamIsE_DisplayedBetTeamIsEven(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(CoreBetList.even, template.betTeam);
        }
    }
}