namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using Core.App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice2807Tests : MatchCodeTests
    {
        private const int TestingBetTypeId = 2807;

        [SetUp]
        public override void Setup()
        {
            base.Setup();

            Choice = new Choice2807();
            Ticket.BetTypeId = TestingBetTypeId;

            TicketHelper.GetBetTypeNameById(Arg.Is(TestingBetTypeId), Arg.Any<object>()).Returns("Winning Margin 6 Way");
        }

        [TestCase("h1-5")]
        [TestCase("H1-5")]
        public void BuildTeamName_TicketBetTeamIsH1To5_RenderedBetTeamIsHomeToWinBy1To5Points(string betTeam)
        {
            // Arrange.
            const string BetTeamPattern = "{0} to Win by 1 - 5 Points";

            Ticket.BetTeam = betTeam;

            TicketHelper.GetResourceData(Choice2807.BasketballRefId, "1-5").Returns(BetTeamPattern);

            // Act.
            var builtChoice = Choice.Render(Ticket, TicketHelper, null, false);

            // Assert.
            string expectBetTeam = string.Format(BetTeamPattern, CoreBetList.Home);

            Assert.AreEqual(expectBetTeam, builtChoice.betTeam);
        }

        [TestCase("h6-10")]
        [TestCase("H6-10")]
        public void BuildTeamName_TicketBetTeamIsH6To10_RenderedBetTeamIsHomeToWinBy6To10Points(string betTeam)
        {
            // Arrange.
            const string BetTeamPattern = "{0} to Win by 6 - 10 Points";

            Ticket.BetTeam = betTeam;

            TicketHelper.GetResourceData(Choice2807.BasketballRefId, "6-10").Returns(BetTeamPattern);

            // Act.
            var builtChoice = Choice.Render(Ticket, TicketHelper, null, false);

            // Assert.
            string expectBetTeam = string.Format(BetTeamPattern, CoreBetList.Home);

            Assert.AreEqual(expectBetTeam, builtChoice.betTeam);
        }

        [TestCase("h11+")]
        [TestCase("H11+")]
        public void BuildTeamName_TicketBetTeamIsH11Plus_RenderedBetTeamIsHomeToWinBy11PlusPoints(string betTeam)
        {
            // Arrange.
            const string BetTeamPattern = "{0} to Win by 11+ Points";

            Ticket.BetTeam = betTeam;

            TicketHelper.GetResourceData(Choice2807.BasketballRefId, "11+").Returns(BetTeamPattern);

            // Act.
            var builtChoice = Choice.Render(Ticket, TicketHelper, null, false);

            // Assert.
            string expectBetTeam = string.Format(BetTeamPattern, CoreBetList.Home);

            Assert.AreEqual(expectBetTeam, builtChoice.betTeam);
        }

        [TestCase("a1-5")]
        [TestCase("A1-5")]
        public void BuildTeamName_TicketBetTeamIsA1To5_RenderedBetTeamIsAwayToWinBy1To5Points(string betTeam)
        {
            // Arrange.
            const string BetTeamPattern = "{0} to Win by 1 - 5 Points";

            Ticket.BetTeam = betTeam;

            TicketHelper.GetResourceData(Choice2807.BasketballRefId, "1-5").Returns(BetTeamPattern);

            // Act.
            var builtChoice = Choice.Render(Ticket, TicketHelper, null, false);

            // Assert.
            string expectBetTeam = string.Format(BetTeamPattern, CoreBetList.Away);

            Assert.AreEqual(expectBetTeam, builtChoice.betTeam);
        }

        [TestCase("a6-10")]
        [TestCase("A6-10")]
        public void BuildTeamName_TicketBetTeamIsA6To10_RenderedBetTeamIsAwayToWinBy6To10Points(string betTeam)
        {
            // Arrange.
            const string BetTeamPattern = "{0} to Win by 6 - 10 Points";

            Ticket.BetTeam = betTeam;

            TicketHelper.GetResourceData(Choice2807.BasketballRefId, "6-10").Returns(BetTeamPattern);

            // Act.
            var builtChoice = Choice.Render(Ticket, TicketHelper, null, false);

            // Assert.
            string expectBetTeam = string.Format(BetTeamPattern, CoreBetList.Away);

            Assert.AreEqual(expectBetTeam, builtChoice.betTeam);
        }

        [TestCase("a11+")]
        [TestCase("A11+")]
        public void BuildTeamName_TicketBetTeamIsA11Plus_RenderedBetTeamIsAwayToWinBy11PlusPoints(string betTeam)
        {
            // Arrange.
            const string BetTeamPattern = "{0} to Win by 11+ Points";

            Ticket.BetTeam = betTeam;

            TicketHelper.GetResourceData(Choice2807.BasketballRefId, "11+").Returns(BetTeamPattern);

            // Act.
            var builtChoice = Choice.Render(Ticket, TicketHelper, null, false);

            // Assert.
            string expectBetTeam = string.Format(BetTeamPattern, CoreBetList.Away);

            Assert.AreEqual(expectBetTeam, builtChoice.betTeam);
        }
    }
}