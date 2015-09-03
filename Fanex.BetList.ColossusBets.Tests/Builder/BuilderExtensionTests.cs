namespace Fanex.BetList.ColossusBets.Tests.Builder
{
    using System;
    using Fanex.BetList.ColossusBets.Builder;
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class BuilderExtensionTests
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private BetListHTMLBuilder _htmlBuilder;

        [SetUp]
        public void Setup()
        {
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _htmlBuilder = new BetListHTMLBuilder();
        }

        [Test]
        public void BuildColossusBetsTransactionType_NoCashOut_ReturnBetTypeName()
        {
            //// Arrange
            _ticket.Handicap1 = 0;
            var betTypeName = "Colossus Bets";
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>()).Returns(betTypeName);

            //// Act
            var actualTransactionType = _htmlBuilder.BuildColossusBetsTransactionType(_ticket, _ticketHelper);

            //// Assert
            Assert.AreEqual(actualTransactionType, betTypeName);
        }

        [Test]
        public void BuildColossusBetsTransactionType_CashOutIsGreaterThanZero_ReturnBetTypeName()
        {
            //// Arrange
            _ticket.Handicap1 = 10;
            var betTypeName = "Colossus Bets";
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>()).Returns(betTypeName);

            //// Act
            var actualTransactionType = _htmlBuilder.BuildColossusBetsTransactionType(_ticket, _ticketHelper);

            //// Assert
            Assert.AreEqual(actualTransactionType, betTypeName + " 10%");
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
        public void BuildColossusBetsDetailStatus_ReturnStatusHtml(string rawStatus, string expectedStatus)
        {
            //// Arrange
            _ticket.Status = rawStatus;

            //// Act
            var actualStatus = _htmlBuilder.BuildColossusBetsDetailStatus(_ticket);

            //// Assert
            Assert.AreEqual(expectedStatus, actualStatus);
        }

        [Test]
        public void BuildColossusBetsDetailDate_TransDateIsNotNull_ReturnDateHtml()
        {
            //// Arrange
            _ticket.TransDate = new DateTime(2014, 01, 01, 12, 00, 00);

            //// Act
            var actualTransDateHtml = _htmlBuilder.BuildColossusBetsDetailDate(_ticket);

            //// Assert
            Assert.AreEqual("1/1/2014 12:00:00 PM", actualTransDateHtml);
        }
    }
}