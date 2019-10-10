namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice711Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice711();
            _ticket = new Ticket
            {
                BetTypeId = BetTypes.GameXWinningMargin,
                BetTeam = string.Empty
            };
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("")]
        [TestCase("a")]
        [TestCase("A")]
        [TestCase("H")]
        [TestCase("h")]
        [TestCase("a1")]
        [TestCase("A1")]
        [TestCase("H2")]
        [TestCase("h2")]
        [TestCase("1-3")]
        [TestCase("9+")]
        public void BuildBetTeam_TicketBetTeamIsIncorrectFormat_DisplayedBetTeamIsEmpty(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsEmpty(template.betTeam);
        }

        [TestCase("h1-2")]
        [TestCase("H1-2")]
        public void BuildBetTeam_BetToHome_DisplayedHomeToWinByXPoints(string betTeam)
        {
            // Arrange.
            const string winningMargin = "1-2";
            const string winningMarginPattern = "{0} To Win by 1 - 2 Points";

            _ticket.BetTeam = betTeam;
            _ticketHelper
                .GetResourceData(Choice711.BadmintonWinningMarginRefId, winningMargin)
                .Returns(winningMarginPattern);

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedBetTeam = string.Format(winningMarginPattern, CoreBetList.Home);

            Assert.AreEqual(expectedBetTeam, template.betTeam);
        }

        [TestCase("a3-5")]
        [TestCase("A3-5")]
        public void BuildBetTeam_BetToHome_DisplayedAwayToWinByXPoints(string betTeam)
        {
            // Arrange.
            const string winningMargin = "3-5";
            const string winningMarginPattern = "{0} To Win by 3 - 5 Points";

            _ticket.BetTeam = betTeam;
            _ticketHelper
                .GetResourceData(Choice711.BadmintonWinningMarginRefId, winningMargin)
                .Returns(winningMarginPattern);

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedBetTeam = string.Format(winningMarginPattern, CoreBetList.Away);

            Assert.AreEqual(expectedBetTeam, template.betTeam);
        }

        [Test]
        public void BuildBetType_Always_SendBetIdAndBetCheckToGetCorrectGameName()
        {
            // Arrange.
            const int betId = 803;
            const string betCheck = "803";
            const string expectedBetTypeName = "Game 3 Winning Margin";

            _ticket.BetId = betId;
            _ticket.BetCheck = betCheck;

            _ticketHelper
                .GetBetTypeNameById(_ticket.BetTypeId, Arg.Any<object>(), _ticket.BetId, _ticket.BetCheck)
                .Returns(expectedBetTypeName);

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(expectedBetTypeName, template.BetType.betTypeName);
        }
    }
}