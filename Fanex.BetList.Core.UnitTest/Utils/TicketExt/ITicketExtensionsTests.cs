namespace Fanex.BetList.Core.UnitTest.Utils.TicketExt
{
    using System.Globalization;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.TicketExt;
    using NSubstitute;
    using NUnit.Framework;

    public class ITicketExtensionsTests
    {
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _ticket = Substitute.For<ITicket>();
        }

        [TestCase(BetStatus.Won)]
        [TestCase(BetStatus.HalfWon)]
        [TestCase(BetStatus.Lose)]
        [TestCase(BetStatus.HalfLose)]
        [TestCase(BetStatus.Draw)]
        public void IsSettled_StatusInDefinedSettledStatuses_ReturnTrue(string settledStatus)
        {
            // Arrange.
            _ticket.Status = settledStatus;

            // Act.
            var isSettled = _ticket.IsSettled();

            // Assert.
            Assert.IsTrue(isSettled);
        }

        [TestCase(BetStatusId.Won)]
        [TestCase(BetStatusId.Lost)]
        [TestCase(BetStatusId.Draw)]
        public void IsSettled_StatusIdInDefinedSettledStatusIds_ReturnTrue(string settledStatusId)
        {
            // Arrange.
            _ticket.StatusId = int.Parse(settledStatusId, CultureInfo.InvariantCulture);

            // Act.
            var isSettled = _ticket.IsSettled();

            // Assert.
            Assert.IsTrue(isSettled);
        }

        [TestCase(BetStatus.Refund)]
        [TestCase(BetStatus.Running)]
        [TestCase(BetStatus.Waiting)]
        [TestCase(BetStatus.Void)]
        [TestCase(BetStatus.Reject)]
        public void IsSettled_StatusInDefinedUnsettledStatuses_ReturnFalse(string unsettledStatus)
        {
            // Arrange.
            _ticket.Status = unsettledStatus;

            // Act.
            var isSettled = _ticket.IsSettled();

            // Assert.
            Assert.IsFalse(isSettled);
        }

        [TestCase(BetStatusId.Refund)]
        [TestCase(BetStatusId.Running)]
        [TestCase(BetStatusId.Waiting)]
        [TestCase(BetStatusId.Void)]
        [TestCase(BetStatusId.Reject)]
        public void IsSettled_StatusIdInDefinedUnsettledStatusIds_ReturnFalse(string unsettledStatusId)
        {
            // Arrange.
            _ticket.StatusId = int.Parse(unsettledStatusId, CultureInfo.InvariantCulture);

            // Act.
            var isSettled = _ticket.IsSettled();

            // Assert.
            Assert.IsFalse(isSettled);
        }

        [TestCase(BetStatus.Refund)]
        [TestCase("Refund")]
        [TestCase("REFUND")]
        public void IsRefunded_StatusIsRefund_ReturnTrue(string unsettledStatus)
        {
            // Arrange.
            _ticket.Status = unsettledStatus;

            // Act.
            var isRefunded = _ticket.IsRefunded();

            // Assert.
            Assert.IsTrue(isRefunded);
        }

        [Test]
        public void IsRefunded_StatusIdIsRefund_ReturnTrue()
        {
            // Arrange.
            _ticket.StatusId = int.Parse(BetStatusId.Refund, CultureInfo.InvariantCulture);

            // Act.
            var isRefunded = _ticket.IsRefunded();

            // Assert.
            Assert.IsTrue(isRefunded);
        }

        [TestCase(1)]
        [TestCase(99)]
        [TestCase(154)]
        public void IsSportsbook_SportTypeIdIsInRange_ReturnTrue(int sportTypeId)
        {
            // Arrange.
            _ticket.SportTypeId = sportTypeId;

            // Act.
            var isSportsbook = _ticket.IsSportsbook();

            // Assert.
            Assert.IsTrue(isSportsbook);
        }

        [TestCase(0)]
        [TestCase(100)]
        public void IsSportsbook_SportTypeIdIsOutOfRange_ReturnFalse(int sportTypeId)
        {
            // Arrange.
            _ticket.SportTypeId = sportTypeId;

            // Act.
            var isSportsbook = _ticket.IsSportsbook();

            // Assert.
            Assert.IsFalse(isSportsbook);
        }
    }
}