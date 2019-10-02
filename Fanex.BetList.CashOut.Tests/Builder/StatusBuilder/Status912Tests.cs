namespace Fanex.BetList.CashOut.Tests.Builder.StatusBuilder
{
    using Core.App_GlobalResources;
    using Core.Builder.StatusBuilder;
    using Core.Constants;
    using Core.Entities;
    using Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status912Tests
    {
        private const string ResultHtmlFormat = "{0}<br/><div style='font-weight: normal;'>({1})</div>";

        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IStatus _status;

        [SetUp]
        public void Setup()
        {
            _ticket = TestData.CreateTicket(CashOutBetTypes.Bought);
            _ticketHelper = Substitute.For<ITicketHelper>();
            _status = new Status912();
        }

        [TestCase(BetStatus.Void, "Void")]
        [TestCase(BetStatus.Refund, "Refund")]
        public void BuildResult_StatusIsVoidOrRefund_ShowBoughtBelowStatus(string status, string originalDisplayedStatus)
        {
            // Arrange
            _ticket.Status = status;
            var expectedTemplate = string.Format(ResultHtmlFormat, originalDisplayedStatus, CoreBetList.Bought);

            // Act
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedTemplate, template.result);
        }

        [TestCase(BetStatus.Won)]
        [TestCase(BetStatus.Lose)]
        [TestCase(BetStatus.Draw)]
        [TestCase(BetStatus.Reject)]
        [TestCase(BetStatus.Waiting)]
        [TestCase(BetStatus.Running)]
        public void BuildResult_StatusIsNotVoidOrRefund_ShowBoughtInstead(string status)
        {
            // Arrange
            _ticket.Status = status;
            var expectedTemplate = CoreBetList.Bought;

            // Act
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedTemplate, template.result);
        }
    }
}