namespace Fanex.BetList.ColossusBets.Tests.Builder
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.ColossusBets.Builder;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Resources;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class BuilderExtensionTests
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private BetListHTMLBuilder _htmlBuilder;
        private IEnumerable<ITicket> _tickets;

        [SetUp]
        public void Setup()
        {
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _htmlBuilder = new BetListHTMLBuilder();
            _tickets = new List<ITicket>
            {
                _ticket
            };
        }

        [Test]
        public void BuildColossusBetDetails_NoCashOut_ReturnBetTypeName()
        {
            //// Arrange
            _ticket.Handicap1 = 0;
            var betTypeName = "Colossus Bets";
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>()).Returns(betTypeName);

            //// Act
            var betDetailsHtml = _htmlBuilder.BuildColossusBetDetails(_tickets, _ticketHelper);

            //// Assert
            Assert.IsTrue(betDetailsHtml.Contains(betTypeName));
        }

        [Test]
        public void BuildColossusBetDetails_CashOutIsGreaterThanZero_ReturnBetTypeNameWithCashoutPercent()
        {
            //// Arrange
            _ticket.BetTypeId = 18002;
            _ticket.Handicap1 = 0.1m;
            var betTypeName = "Colossus Bets";
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>()).Returns(betTypeName);

            //// Act
            var betDetailsHtml = _htmlBuilder.BuildColossusBetDetails(_tickets, _ticketHelper);

            //// Assert
            Assert.IsTrue(betDetailsHtml.Contains(betTypeName + " 10%"));
        }

        [Test]
        public void BuildColossusBetDetails_ReturnSummaryYourOwnPercent()
        {
            //// Arrange
            _ticket.BetTypeId = 18002;
            _ticket.Handicap1 = 0.1m;

            //// Act
            var betDetailsHtml = _htmlBuilder.BuildColossusBetDetails(_tickets, _ticketHelper);

            //// Assert
            Assert.IsTrue(betDetailsHtml.Contains(string.Format(ColosussBetsLabel.YouOwnPercent, "90")));
        }

        [Test]
        public void BuildColossusBetDetails_ReturnSummaryCashoutPercent()
        {
            //// Arrange
            _ticket.BetTypeId = 18002;
            _ticket.Handicap1 = 0.1m;
            _ticket.Winlost = 20;

            //// Act
            var betDetailsHtml = _htmlBuilder.BuildColossusBetDetails(_tickets, _ticketHelper);

            //// Assert
            Assert.IsTrue(betDetailsHtml.Contains(string.Format(ColosussBetsLabel.YouSoldPercent, "10", "<span class='positive-win-loss'>20.00</span>")));
        }

        [TestCase(BetStatus.Running, "Running")]
        [TestCase(BetStatus.Won, "Won")]
        [TestCase(BetStatus.Lose, "Lose")]
        [TestCase(BetStatus.Reject, "Reject")]
        [TestCase(BetStatus.Void, "Void")]
        [TestCase(BetStatus.Waiting, "Waiting")]
        [TestCase(BetStatus.Refund, "Refund")]
        [TestCase(BetStatus.Draw, "Draw")]
        [TestCase(null, "")]
        [TestCase(" ", "")]
        [TestCase("", "")]
        public void BuildColossusBetDetails_ReturnStatusHtml(string rawStatus, string expectedStatus)
        {
            //// Arrange
            _ticket.Status = rawStatus;

            //// Act
            var betDetailsHtml = _htmlBuilder.BuildColossusBetDetails(_tickets, _ticketHelper);

            //// Assert
            Assert.IsTrue(betDetailsHtml.Contains(expectedStatus));
        }

        [Test]
        public void BuildColossusBetDetails_TransDateIsNotNull_ReturnDateHtml()
        {
            //// Arrange
            _ticket.TransDate = new DateTime(2014, 01, 01, 12, 00, 00);

            //// Act
            var actualBetDetailsHtml = _htmlBuilder.BuildColossusBetDetails(_tickets, _ticketHelper);

            //// Assert
            Assert.IsTrue(actualBetDetailsHtml.Contains("1/1/2014 12:00:00 PM"));
        }

        [Test]
        public void BuildColossusBetDetails_WinLossIsNegative_ReturnNegativeFormat()
        {
            //// Arrange
            _ticket.Winlost = -10;

            //// Act
            var actualBetDetailsHtml = _htmlBuilder.BuildColossusBetDetails(_tickets, _ticketHelper);

            //// Assert
            Assert.IsTrue(actualBetDetailsHtml.Contains("<span class='negative-win-loss'>-10.00</span>"));
        }

        [Test]
        public void BuildColossusBetDetails_NoTicket_ReturnNoTicketInfo()
        {
            //// Act
            var actualBetDetailsHtml = _htmlBuilder.BuildColossusBetDetails(new List<ITicket>(), _ticketHelper);

            //// Assert
            Assert.IsTrue(actualBetDetailsHtml.Contains(CoreBetList.ThereIsNoTicketDetailAvailable));
        }
    }
}