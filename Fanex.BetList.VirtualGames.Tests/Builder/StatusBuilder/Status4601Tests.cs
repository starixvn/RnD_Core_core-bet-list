namespace Fanex.BetList.VirtualGames.Tests.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    public class Status4601Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status4601();
            _ticket = TestData.CreateTicket(VirtualGamesBetTypes.Baccarat);
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildStatusResult_BetCheckIsEmpty_HideResultLink()
        {
            // Act.
            var statusTemplate = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsFalse(statusTemplate.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_BetCheckIsNotEmpty_ShowResultLink()
        {
            // Arrange.
            const string betCheck = "PB011912120001";

            _ticket.BetCheck = betCheck;

            // Act.
            var statusTemplate = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsTrue(statusTemplate.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_BetCheckIsNotEmpty_CorrectParaForResult()
        {
            // Arrange.
            const string betCheck = "PB012001010001";
            _ticket.CurrencyId = 20;
            _ticket.BetTeam = "betteam";
            _ticket.CustId = 123;
            _ticket.SiteId = "siteid";
            _ticket.BetCheck = betCheck;

            // Act.
            var statusTemplate = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedBetId = "20, 'betteam', 123, 'PB012001010001', 'siteid'";

            Assert.AreEqual(expectedBetId, statusTemplate.StatusResult.betId);
        }
    }
}